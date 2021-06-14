using System;
using System.Collections.Generic;

namespace TyreStoreAPI.Models
{
    public partial class Tyres
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Season { get; set; }
        public string Part { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        public int? TyreId { get; set; }

        public virtual TyresSizes Tyre { get; set; }
    }
}
