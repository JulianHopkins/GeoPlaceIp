using GeoPlaceIp.Infras.Evaluator;
using System.IO.MemoryMappedFiles;

namespace GeoPlaceIp.Infras.Search
{
    public class Search
    {
        public Search(EvaluatorBase Evaluator)
        {
            this.Evaluator = Evaluator;
        }
        private EvaluatorBase Evaluator;


        public GeoItem BinarySearch<T>(T value, out int middle)
        {
            int left = 0;
            int right = Evaluator.h.records;
            //пока не сошлись границы массива
            while (left <= right)
            {
                //индекс среднего элемента
                middle = (left + right) / 2;
                int f = 0;
                if ((f = Evaluator.Evaluate<T>(middle, value, out GeoItem gi)) == 0)
                {
                    return gi;
                }
                else if (f < 0)
                {
                    //сужаем рабочую зону массива с правой стороны
                    right = middle - 1;
                }
                else
                {
                    //сужаем рабочую зону массива с левой стороны
                    left = middle + 1;
                }
            }
            //ничего не нашли
            middle = -1;
            return null;
        }

    }
}
