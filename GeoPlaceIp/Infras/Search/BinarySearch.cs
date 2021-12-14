using System.IO.MemoryMappedFiles;

namespace GeoPlaceIp.Infras.Search
{
    public class Search
    {
        MemoryMappedViewAccessor
        DataHeader
        public Search()
        {

        }
        public GeoItem BinarySearch<T>(Func<MemoryMappedViewAccessor, DataHeader, long, T, int> evaluate,  T value, long left, long right)
        {

        }

    }
}
