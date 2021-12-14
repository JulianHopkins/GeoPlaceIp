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
            int r = range.ip_from.CompareTo(value);
            if (r != 0) return r;

            gi = GetGeoItem(h.offset_locations + range.location_index);
            return 0;
        }
    }
}
