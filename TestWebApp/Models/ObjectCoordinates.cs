using GeoJSON.Net.Geometry;


namespace Domain.Models
{
    internal class ObjectCoordinates : IPosition
    {
        public double? Altitude { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
