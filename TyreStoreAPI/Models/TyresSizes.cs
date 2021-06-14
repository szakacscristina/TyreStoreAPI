using System;
using System.Collections.Generic;

namespace TyreStoreAPI.Models
{
    public partial class TyresSizes
    {
        public TyresSizes()
        {
            Tyres = new HashSet<Tyres>();
            TyresModels = new HashSet<TyresModels>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? TyreWidth { get; set; }
        public decimal? AspectRatio { get; set; }
        public decimal? RimDiameter { get; set; }

        public virtual ICollection<Tyres> Tyres { get; set; }
        public virtual ICollection<TyresModels> TyresModels { get; set; }
    }
}
