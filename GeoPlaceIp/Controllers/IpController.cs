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
        private readonly ILogger<IpController> _logger;
        public IpController(IRepo repo, ILogger<IpController> logger)
        {
            this.repo = repo;
            _logger = logger;
        }
        [HttpGet]
        public GeoItemOut Get(string Ip)
        {
            var OpR = repo.Call<OperationResult>(o=> o.GetGeoFromIp(Ip));
            if (!string.IsNullOrEmpty(OpR.Error))
            {
                _logger.Log(LogLevel.Error, OpR.Error);
                return null;
            }
            return OpR.gi.Convert();

        }

    }
}
