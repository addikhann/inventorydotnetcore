using AutoMapper;
using InventorySystemBusiness.Models;
using InventorySystemServices.Interfaces;
using InventorySystemServices.LibModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InventorySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _product;
        private IMapper _mapper;

        public ProductController(IProductService product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        //Show All Products List
        [HttpGet("ProductList")]
        public async Task<IActionResult> ProductListAsync()
        {
            var result = await _product.GetProductListAsync().ConfigureAwait(false);
            if (result == null)
                return NotFound();

            return Ok(new { success = true, response = _mapper.Map<ProductLib[]>(result) });
        }

        //Get Product Basis on Id
        [HttpGet("ProductById/{id}")]
        public async Task<IActionResult> ProductById(int id)
        {
            var result = await _product.GetProductAsync(id).ConfigureAwait(false);
            if (result == null)
                return NotFound(new { success = false, response = result });
            
            return Ok(new { success = true, response = result });
        }

        //Add New Product
        [HttpPost("AddProduct")]
        [AllowAnonymous]
        public async Task<IActionResult> AddProduct([FromForm] ProductLib product)
        {
            string folderName = string.Empty;
            string fileName = string.Empty;
            if (product.ImageFile.Length > 0)
            {
                var file = Request.Form.Files[0];
                folderName = Path.Combine("Images", "Products");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);


                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                product.ImagePath = folderName + "\\" + fileName;
                var addProduct = _mapper.Map<Product>(product);
                var result = await _product.AddProductAsync(addProduct).ConfigureAwait(false);
                if (result == 0)
                    return NotFound(new { success = false, response = result });

                return Ok(new { success = true, response = result });
            }
            else
                return Ok(new { success = false, response = "Image Not Found" });

          
        }

        //Update Product Details
        [HttpPut("UpdateProduct")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateProduct([FromForm]  ProductLib product)
        {
            string folderName = string.Empty;
            string fileName = string.Empty;
            var addProduct = _mapper.Map<Product>(product);
            if (product.ImageFile != null)
            {
                var file = Request.Form.Files[0];
                folderName = Path.Combine("Images", "Products");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);


                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                addProduct.ImagePath = folderName + "\\" + fileName;
                
                var result = await _product.UpdateProductAsync(addProduct).ConfigureAwait(false);
                if (result == 0)
                    return NotFound(new { success = false, response = result });

                return Ok(new { success = true, response = result });
            }
            else
            {
                var result = await _product.UpdateProductAsync(addProduct).ConfigureAwait(false);
                if (result == 0)
                    return NotFound(new { success = false, response = result });

                return Ok(new { success = true, response = result });
            }                
        }

        //Delete  Product Basis on Id
        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _product.DeleteProductAsync(id).ConfigureAwait(false);
            if (result == 0)
                return NotFound(new { success = false, response = result });

            return Ok(new { success = true, response = result });
        }
    }
}
