using System.IO.MemoryMappedFiles;

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
        protected GeoItem GetGeoItem(long address)
        {
            var gi = new GeoItem();
            mmva.ReadArray(address, gi.country, 0, 8);
            mmva.ReadArray(address + 8, gi.region, 0, 12);
            mmva.ReadArray(address + 20, gi.postal, 0, 12);
            mmva.ReadArray(address + 32, gi.city, 0, 24);
            mmva.ReadArray(address + 56, gi.organization, 0, 32);
            gi.latitude = mmva.ReadSingle(address + 88);
            gi.longitude = mmva.ReadSingle(address + 92);
            return gi;
        }

        protected abstract long IntToLong(int i);
        public abstract int Evaluate<R>(int i, R value, out GeoItem gi);
    }
}
