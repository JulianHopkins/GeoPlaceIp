namespace GeoPlaceIp.Infras.Models
{
    public class OperationResult
    {
        public GeoItem Gi { get; set; }
        public GeoItem[] Items { get; set;}
        public string Error { get; set; }
    }
}
