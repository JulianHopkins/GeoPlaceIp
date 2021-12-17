using System.IO.MemoryMappedFiles;
using GeoPlaceIp.Infras.Converters;
using GeoPlaceIp.Infras.Load;

namespace GeoPlaceIp.Infras.Evaluator
{
    public abstract class EvaluatorBase
    {
        protected MemoryMappedViewAccessor mmva;
        public DataHeader h;
        public EvaluatorBase(MemoryMappedViewAccessor mmva, DataHeader h)
        {
            this.mmva = mmva;
            this.h = h;
        }
        protected T GetValue<T>(long address) where T : struct
        {
            T value;
            mmva.Read(address, out value);
            return value;
        }
        protected GeoItem GetGeoItem(long address, string city = null)
        {
            var G = new GeoItemLoader(mmva);
            return G.GetGeoItem(address, city);
        }

        protected abstract long IntToLong(int i);
        public abstract int Evaluate<R>(int i, R value, out GeoItem gi);
    }
}
