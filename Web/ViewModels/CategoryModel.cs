namespace Web.ViewModels
{
   public class CategoryModel
   {
      public string Name { get; init; }

      public CategoryModel(string name)
      {
         Name = name;
      }
   }
}