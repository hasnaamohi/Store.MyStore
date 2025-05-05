﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product:BaseEntity<int>
    {
        public string  Name{ get; set; }
        public string  Description{ get; set; }
        public string  PictureUrl{ get; set; }
        public decimal  Price{ get; set; }
        public int BrandId {  get; set; }//FK
        [ForeignKey ("BrandId")]
        public ProductBrand ProductBrand { get; set; } //Navigational Property
        public int TypeId { get; set; } //FK
        [ForeignKey("TypeId")]
        public ProductType ProductType { get; set; } //Navigational Property
        
    }
}
