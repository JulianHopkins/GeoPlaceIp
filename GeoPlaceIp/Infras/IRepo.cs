using GeoPlaceIp.Infras.Models;

namespace GeoPlaceIp.Infras
{
    public interface IRepo
    {
        R Call<R>(Func<IRepo, R> func) where R : new();
        OperationResult GetGeoFromCity(string City);
        OperationResult GetGeoFromIp(string Ip);

    }
}
 