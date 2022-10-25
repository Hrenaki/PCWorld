namespace Web.ViewModels
{
   public class CategorySearchModel
   {
      public IReadOnlyList<CategoryModel> Categories { get; init; }

      public CategorySearchModel(List<CategoryModel> categories)
      {
         Categories = categories.AsReadOnly();
      }
   }
}