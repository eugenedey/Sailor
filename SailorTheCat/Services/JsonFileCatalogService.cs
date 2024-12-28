using Microsoft.AspNetCore.Hosting;
using SailorTheCat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SailorTheCat.Services
{
    public class JsonFileCatalogService
    {
        public JsonFileCatalogService(IWebHostEnvironment webHostEnvironment) 
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
         get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "Catalog.json"); }
        }
        public IEnumerable<CatalogItems> GetCatalogItems() 
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<CatalogItems[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        
        
        }

        public void AddRating(string catalogID, int rating)
        {
            var items = GetCatalogItems();
            var query = items.First(x => x.Id == catalogID);
            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<CatalogItems>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    items
                    );

            }

        }
        
    }
}
