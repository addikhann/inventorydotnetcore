using InventorySystemBusiness.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystemBusiness.EntityContaxt
{
    public static class CoreDbSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            IList<Models.Product> productDetails = new List<Product>();
            productDetails.Add(new Product()
            {
                ID = 1,
                Name = "Teddy Toy",
                Description = "Teddy Toy Full Size",
                Price = 2.36,
                ImagePath = "Images=\\Products\\222.PNG",
                Created = DateTime.Now,
                Updated = DateTime.Now,
            });    
            foreach (Product product in productDetails)
                modelBuilder.Entity<Product>().HasData(product);
        }
    }
}
