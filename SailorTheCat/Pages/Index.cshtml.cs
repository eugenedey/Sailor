using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SailorTheCat.Models;
using SailorTheCat.Services;

namespace SailorTheCat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileCatalogService CatalogService;
        public IEnumerable<CatalogItems> Catalog { get; private set; } 

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileCatalogService catalogService)

        {
            _logger = logger;
            CatalogService = catalogService;
        }

        public void OnGet()
        {
            Catalog = CatalogService.GetCatalogItems();
        }
    }
}
