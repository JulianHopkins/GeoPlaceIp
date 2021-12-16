using System.IO.MemoryMappedFiles;

namespace GeoPlaceIp.Infras.Evaluator
{
    public class EvaluatorIp : EvaluatorBase
    {
        public EvaluatorIp(MemoryMappedViewAccessor mmva, DataHeader h) : base(mmva, h)
        {
            this.mmva = mmva;
            this.h = h;
        }

        protected override long IntToLong(int i)
        {
            return h.offset_ranges + (i * 12);
        }
        public override int Evaluate<UInt32>(int i, UInt32 value, out GeoItem gi)
        {
            gi = null;
            var range = GetValue<IpRange>(IntToLong(i));
            if (0 < range.ip_from.CompareTo(value)) return -1;
            if (0 > range.ip_to.CompareTo(value)) return 1;
            gi = GetGeoItem(h.offset_locations + range.location_index);
            return 0;
        }
    }
}
