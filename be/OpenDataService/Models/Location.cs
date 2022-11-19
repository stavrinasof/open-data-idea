namespace OpenDataService.Models
{
    public class Location
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string Area { get; set; }
        public List<string> StreetNames { get; set; }
        public Location(int lat, int longt, string area, List<string> streetNames)
        {
            Latitude = lat;
            Longitude = longt;
            Area = area;    
            StreetNames = streetNames;
        }
    }

    
}
