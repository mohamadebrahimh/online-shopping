using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Businessdevweb.Models
{
    public class ShopingCart
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ImageFile { get; set; }
        public int Qty { get; set; }
        public long? Price { get; set; }
        public long? Total => Qty * Price;

        public string ProductTitle { get;  set; }
    }
}