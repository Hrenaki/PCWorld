using Core.SearchZone;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
   [Route("/categories")]
   public class CategoryController : Controller
   {
      private readonly ICategoryService categoryService;

      public CategoryController(ICategoryService categoryService)
      {
         ArgumentNullException.ThrowIfNull(categoryService, nameof(categoryService));
         this.categoryService = categoryService;
      }

      [HttpGet("list")]
      public IActionResult GetCategories()
      {
         var categories = categoryService.GetCategories();
         return PartialView("Categories", new CategorySearchModel(categories.Select(c => new CategoryModel(c.Name)).ToList()));
      }
   }
}