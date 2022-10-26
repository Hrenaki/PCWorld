using Core.SearchZone;
using Core.SearchZone.Filters;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
   [Route("/products")]
   public class CatalogController : Controller
   {
      private IProductService productService;

      public CatalogController(IProductService productService)
      {
         ArgumentNullException.ThrowIfNull(productService, nameof(productService));

         this.productService = productService;
      }

      [HttpGet("list")]
      public IActionResult GetProducts()
      {
         ProductSearchResultModel rm = new ProductSearchResultModel("",
            productService.GetProducts().Select(pe => new SearchResultModel(pe)).ToList());

         return View("Catalog", rm);
      }

      [HttpGet("category/{name:maxlength(20)}")]
      public IActionResult GetProductsByCategory(string name)
      {
         var pipeLine = new FilterPipeline<IProductFilter>(new ProductCategoryFilter() { CategoryName = name });
         ProductSearchResultModel rm = new ProductSearchResultModel("",
            productService.GetProducts(pipeLine).Select(pe => new SearchResultModel(pe)).ToList());
         return View("Catalog", rm);
      }
   }
}