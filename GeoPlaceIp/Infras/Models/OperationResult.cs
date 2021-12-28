namespace GeoPlaceIp.Infras.Models
{
    public class OperationResult
    {
        public GeoItem Gi { get; set; }
        public GeoItem[] Items { get; set;}
        public LastExc Error { get; set; }
    }
}
