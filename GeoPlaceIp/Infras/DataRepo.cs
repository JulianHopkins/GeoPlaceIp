using GeoPlaceIp.Infras;
using GeoPlaceIp.Infras.Models;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using GeoPlaceIp.Infras.Converters;
using GeoPlaceIp.Infras.Evaluator;
using GeoPlaceIp.Infras.Search;
using System.Text;
using System.Reflection;

public class DataRepo : IRepo
{
    private DataHeader h;
	public DataRepo()
	{
        h = new DataHeader(DataLoader.mmf.CreateViewAccessor());
    }
    public R Call<R>(Func<IRepo, R> func) where R : new()
    {
        try
        {

            return func(this);

        }
        catch (Exception ex)
        {
            R answer = new R();
            PropertyInfo pi = typeof(R).GetProperties().FirstOrDefault(s => s.Name == "Error");
            StringBuilder errbody = new StringBuilder(3000);
            var i = 0;
            while (ex != null)
            {

                errbody.AppendLine(string.Format("\r\n{0} type: {1}, Message: {2}", ++i > 1 ? "Inner exception" : "Exception", ex.GetType(), ex.Message));
                ex = ex.InnerException;
            }
            if (pi != null) pi.SetValue(answer, errbody.ToString(), null);
            return answer;
        }
    }
    public OperationResult GetGeoFromCity(string City)
    {
        EvaluatorBase Eval = new EvaluatorCity(DataLoader.mmf.CreateViewAccessor(), h);
        var s = new Search(Eval);
        int i;
        var gi = s.BinarySearch(City, out i);
        return new OperationResult { Items = ((EvaluatorCity)Eval).GetAll(i, gi) };
    }
    public OperationResult GetGeoFromIp(string Ip)
    {
        EvaluatorBase Eval = new EvaluatorIp(DataLoader.mmf.CreateViewAccessor(), h);
        var s = new Search(Eval);
        int i;

        return new OperationResult { Gi = s.BinarySearch(Ip.IpStringToUint(), out i) };
    }




}
