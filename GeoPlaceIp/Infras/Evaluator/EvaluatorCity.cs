using System.Collections.Concurrent;
using System.IO.MemoryMappedFiles;
using GeoPlaceIp.Infras.Converters;
using GeoPlaceIp.Infras.Load;

namespace GeoPlaceIp.Infras.Evaluator
{
    public class EvaluatorCity : EvaluatorBase
    {
        public EvaluatorCity(MemoryMappedViewAccessor mmva, DataHeader h) : base(mmva, h)
        {
        }

        protected override long IntToLong(int i)
        {
            return h.offset_cities + (4 * i);
        }
        public override int Evaluate<Object>(int i, Object value, out GeoItem gi)
        {
            gi = null;
            var k = IntToLong(i);
            var u = GetValue<uint>(k);
            sbyte[] _city = new sbyte[24];
            uint q = h.offset_locations + u + 32;
            mmva.ReadArray(q, _city, 0, 24);
            string MCity = _city.SbytesToStr();
            string City = value as string;
            int Z = String.CompareOrdinal(City, 4, MCity, 4, City.Length - 4);
            if (Z == 0)
            {
                gi = GetGeoItem(h.offset_locations + u, MCity);
            }
            return Z;

        }

        public GeoItem[] GetAll(int i, ConcurrentBag<GeoItem> items, string City)
        {

            Func<GeoItemLoader, int, int> cityFunc = (gild, l) =>
            {
                var u = GetValue<uint>(IntToLong(l));
                sbyte[] _city = new sbyte[24];
                uint q = h.offset_locations + u + 32;
                mmva.ReadArray(q, _city, 0, 24);
                string MCity = _city.SbytesToStr();
                int c=2;
                if ((c = String.CompareOrdinal(MCity, 4, City, 4, City.Length - 4)) == 0 )
                {
                    items.Add(gild.GetGeoItem(h.offset_locations + u, MCity));
                }
                return c;
            };
            Task[] tsks = new Task[]
{
                Task.Run(()=> {
                    var gild = new GeoItemLoader(mmva);
                    for(int l = i+1; l < h.records; l++)
                    {
                        if(cityFunc(gild, l) != 0) break;
                    }
                }),
                Task.Run(() => {
                    var gild = new GeoItemLoader(DataLoader.mmf.CreateViewAccessor());
                    for (int l = i-1; l >= 0; l--)
                    {
                        if(cityFunc(gild, l) != 0) break;
                    }
                })
};

            Task.WaitAll(tsks);
            return items.OrderBy(f => f.city).ToArray();
        }

    }
}
