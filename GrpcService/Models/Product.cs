using System;
using System.Collections.Generic;

#nullable disable

namespace GrpcService.Models
{
    public partial class Product
    {
        public int Productrowid { get; set; }
        public string Productid { get; set; }
        public string Productname { get; set; }
        public string Categoryname { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
    }

}
