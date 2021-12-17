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
        private readonly ILogger<CityController> _logger;
        public CityController(IRepo repo, ILogger<CityController> logger)
        {
            this.repo = repo;
            _logger = logger;
        }
        [HttpGet]
        public GeoItem[] Get(string City)
        {
            var OpR = repo.Call<OperationResult>(o => o.GetGeoFromCity(City));
            if (!string.IsNullOrEmpty(OpR.Error))
            {
                _logger.Log(LogLevel.Error, OpR.Error);
                return null;
            }
            return OpR.Items;
        }
    }

}
