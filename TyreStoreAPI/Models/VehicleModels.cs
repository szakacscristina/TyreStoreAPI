using System;
using System.Collections.Generic;

namespace TyreStoreAPI.Models
{
    public partial class VehicleModels
    {
        public VehicleModels()
        {
            TyresModels = new HashSet<TyresModels>();
        }

        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal? StartYear { get; set; }
        public decimal? EndYear { get; set; }

        public virtual VehicleManufacturers Manufacturer { get; set; }
        public virtual ICollection<TyresModels> TyresModels { get; set; }
    }
}
