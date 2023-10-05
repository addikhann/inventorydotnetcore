using InventorySystemBusiness.Models;
using InventorySystemBusiness.Repository;
using InventorySystemServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemServices.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _product;
        public ProductService(IGenericRepository<Product> product)
        {
            _product = product;         

        }
        public async Task<int> AddProductAsync(Product product)
        {
            if (product == null)
                return 0;
            product.Created = DateTime.Now;
            product.Updated = DateTime.Now;
            _product.Insert(product);
            return product.ID;           
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _product.FindFirstByAsync(x => x.ID == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetProductListAsync()
        {
            return await _product.AllAsync().ConfigureAwait(false);
        }
        public async Task<int> UpdateProductAsync(Product product)
        {
            var productResult = await _product.FindFirstByAsync(x => x.ID == product.ID).ConfigureAwait(false);

            if (productResult == null)
                return 0;
            productResult.Name = product.Name;
            productResult.Description = product.Description;
            productResult.Price = product.Price;
            if(product.ImagePath != null)
                productResult.ImagePath = product.ImagePath;

            await _product.UpdateAsync(productResult).ConfigureAwait(false);
            return product.ID;
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            var productResult = await _product.FindFirstByAsync(x => x.ID == id).ConfigureAwait(false);

            if (productResult == null)
                return 0;
          
            await _product.DeleteAsync(productResult).ConfigureAwait(false);
            return productResult.ID;
        }




    }
}
