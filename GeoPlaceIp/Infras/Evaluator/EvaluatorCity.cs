using System.Collections.Concurrent;
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
        public override int Evaluate<Object>(int i, Object value, out GeoItem gi)
        {
            gi = GetGeoItem(h.offset_locations + GetValue<uint>(IntToLong(i)));
            var City = value as sbyte[];
            if (City == null) throw new ArgumentNullException();
            return CompareArr(City, gi.city);

        }

        private int CompareArr(sbyte[] arr1, sbyte[] arr2)
        {
            int x = (arr1.Length < arr2.Length) ? arr1.Length : arr2.Length;
            for (int j = 0; j < x; j++)
            {
                var y = arr1[j].CompareTo(arr2[j]);
                if (y == 0) continue;
                return y;
            }
            return 0;
        }
        public List<GeoItem> GetAll(int i, GeoItem gi)
        {
        ConcurrentBag<GeoItem> items = new ConcurrentBag<GeoItem>();
            items.Add(gi);
            Task[] tsks = new Task[]
{
                Task.Run(()=> {
                    for(int l = i+1; l < h.records; l++)
                    {
                       var _gi = GetGeoItem(h.offset_locations + GetValue<uint>(IntToLong(l)));
                        if(0 == CompareArr(_gi.city, gi.city)) items.Add(_gi);
                        else break;
                    }
                }),
                Task.Run(() => {
                    for (int l = i-1; l >= 0; l--)
                    {
                        var _gi = GetGeoItem(h.offset_locations + GetValue<uint>(IntToLong(l)));
                        if(0 == CompareArr(_gi.city, gi.city)) items.Add(_gi);
                        else break;
                    }
                })
};

            Task.WaitAll(tsks);
            return items.OrderBy(f=> f.city).ToList();
        }
    }
}
