using System.IO.MemoryMappedFiles;

namespace GeoPlaceIp.Infras.Models
{
    public class IpRanges
    {
        public SortedSet<IpRange> ipRanges;
        public IpRanges(MemoryMappedViewAccessor mmva, DataHeader h)
        {
            ipRanges = new SortedSet<IpRange>();
            
            for (int i = 0; i < h.records; i++)
            {
                IpRange range;
                mmva.Read<IpRange>(60 + i * 12, out range);
                ipRanges.Add(range);
            }
        }
    }

}
