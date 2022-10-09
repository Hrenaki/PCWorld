using Core.SearchZone;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HTTPAPI.Controllers
{
   [Route("/products")]
   public class ProductController : Controller
   {
      private readonly MainDbContext dbcontext;

      public ProductController(MainDbContext dbcontext)
      {
         this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
      }

      [HttpGet("list")]
      public string GetProducts()
      {
         var products = dbcontext.Products.Include(p => p.Category)
                                          .Select(p => new 
                                          {
                                             id = p.Id,
                                             name = p.Name,
                                             price = p.Price,
                                             category = new { id = p.CategoryId, name = p.Category.Name }
                                          });
         return JsonConvert.SerializeObject(products);
      }
   }
}