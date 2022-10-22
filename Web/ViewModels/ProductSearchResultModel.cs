using Data.Common;
using SharpCompress.Common;

namespace Web.ViewModels
{
   public class SearchResultModel
   {
      public ProductEntity Entity { get; init; }

      public string Name => Entity.Name;
      public string Category => Entity.Category;
      public decimal MinPrice => Entity.Prices.Min(info => info.Price);
      public int ShopCount => Entity.Prices.Count();

      public SearchResultModel(ProductEntity entity)
      {
         ArgumentNullException.ThrowIfNull(entity, nameof(entity));

         Entity = entity;
      }
   }

   public class ProductSearchResultModel
   {
      public string Category { get; init; }
      public List<SearchResultModel> Products { get; init; }

      public ProductSearchResultModel(string category, List<SearchResultModel> products)
      {
         ArgumentNullException.ThrowIfNull(category, nameof(category));
         ArgumentNullException.ThrowIfNull(products, nameof(products));

         Category = category;
         Products = products;
      }
   }
}