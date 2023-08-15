using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class ObjectCoordinates : IPosition
    {
        public double? Altitude { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
