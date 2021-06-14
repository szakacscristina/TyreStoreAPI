using System;
using System.Collections.Generic;

namespace TyreStoreAPI.Models
{
    public partial class VehicleManufacturers
    {
        public VehicleManufacturers()
        {
            VehicleModels = new HashSet<VehicleModels>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<VehicleModels> VehicleModels { get; set; }
    }
}
