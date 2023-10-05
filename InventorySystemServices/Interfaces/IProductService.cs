using InventorySystemBusiness.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemServices.Interfaces
{
    public interface IProductService
    {
        Task<int> AddProductAsync(Product product);
        Task<Product> GetProductAsync(int id);
        Task<IEnumerable<Product>> GetProductListAsync();
        Task<int> UpdateProductAsync(Product product);
        Task<int> DeleteProductAsync(int id);
    }
}
