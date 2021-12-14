using System.IO.MemoryMappedFiles;

namespace GeoPlaceIp.Infras.Evaluator
{
    public class EvaluatorCity : EvaluatorBase
    {
        public EvaluatorCity(MemoryMappedViewAccessor mmva, DataHeader h) : base(mmva, h)
        {
            this.mmva = mmva;
            this.h = h;
        }

        protected override long IntToLong(int i)
        {
            return 60 + (h.offset_cities) + (4 * i);
        }
        public override int Evaluate<sbyte[]>(int i, sbyte[] value, out GeoItem gi)
        {
            uint location_index = GetValue<uint>(IntToLong(i));
            gi = GetGeoItem(h.offset_locations + location_index);
           /* int x = (value.Length < gi.city.Length)
            if (value < range.ip_from) return -1;
            if (value > range.ip_to) return 1;*/
            return 0;
        }
    }
}
