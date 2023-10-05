using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystemServices.LibModel
{
    public class ProductLib
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
