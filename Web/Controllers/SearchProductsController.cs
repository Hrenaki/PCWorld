using Core.SearchZone;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
   [Route("/products")]
   public class ProductsController : Controller
   {
      private IProductService productService;

      public ProductsController(IProductService productService)
      {
         ArgumentNullException.ThrowIfNull(productService, nameof(productService));

         this.productService = productService;
      }

      [HttpGet("list")]
      public IActionResult GetProducts()
      {
         ProductSearchResultModel rm = new ProductSearchResultModel("", 
            productService.GetProducts().Select(pe => new SearchResultModel(pe)).ToList());

         return PartialView("Products", rm);
      }
   }
}