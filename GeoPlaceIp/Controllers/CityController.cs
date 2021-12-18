using GeoPlaceIp.Infras;
using GeoPlaceIp.Infras.Models;
using Microsoft.AspNetCore.Mvc;
using GeoPlaceIp.Infras.Converters;

namespace GeoPlaceIp.Controllers
{
    [ApiController]
    [Route("[controller]/locations")]
    public class CityController : Controller
    {
        private readonly IRepo repo;

        public CityController(IRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public OperationResult Get(string City) => repo.Call<OperationResult>(o => o.GetGeoFromCity(City));

    }

}
