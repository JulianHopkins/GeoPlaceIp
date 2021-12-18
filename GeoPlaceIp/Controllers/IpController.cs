using GeoPlaceIp.Infras;
using GeoPlaceIp.Infras.Models;
using Microsoft.AspNetCore.Mvc;
using GeoPlaceIp.Infras.Converters;

namespace GeoPlaceIp.Controllers
{
    [ApiController]
    [Route("[controller]/location")]
    public class IpController : Controller
    {
        private readonly IRepo repo;
        public IpController(IRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public OperationResult Get(string Ip) => repo.Call<OperationResult>(o=> o.GetGeoFromIp(Ip));

    }
}
