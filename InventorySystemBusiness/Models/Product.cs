using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventorySystemBusiness.Models
{
   public class Product
    {
        public Product()
        {

        }
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Double Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
  
    }
}