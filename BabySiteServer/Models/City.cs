using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServer.Models
{
    public partial class City
    {
        public City()
        {
            Locations = new HashSet<Location>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int AreaId { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
