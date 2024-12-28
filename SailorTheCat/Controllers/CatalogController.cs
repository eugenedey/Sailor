using System.Collections.Generic;
using SailorTheCat.Models;
using SailorTheCat.Services;
using Microsoft.AspNetCore.Mvc;

namespace SailorTheCat.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        public CatalogController(JsonFileCatalogService catalogService)
        {
            CatalogService = catalogService;
        }

        public JsonFileCatalogService CatalogService { get; }

        [HttpGet]
        public IEnumerable<CatalogItems> Get()
        {
            return CatalogService.GetCatalogItems();
        }

        //[HttpPatch] "[FromBody]"
        [Route("Rate")]
        [HttpGet]
        public ActionResult Get([FromQuery] string catalogId, [FromQuery] int Rating)
        {
            CatalogService.AddRating(catalogId, Rating);
            return Ok();
        }

    }
}
