namespace GeoPlaceIp.Infras.Models
{
    public class OperationResult
    {
        public GeoItem gi { get; set; }
        public List<GeoItem> items { get; set;}
        public string Error { get; set; }
    }
}
