using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configuration
{
   public interface IConfiguration
   {
      public T Get<T>();
   }

   internal class Configuration : IConfiguration
   {
      private Dictionary<Type, object> objectDictionary = new Dictionary<Type, object>();

      public Configuration(Dictionary<Type, object> objectDictionary)
      {
         this.objectDictionary = objectDictionary;
      }

      public T Get<T>()
      {
         if (!objectDictionary.ContainsKey(typeof(T)))
            throw new ArgumentException(typeof(T).Name);

         return (T)objectDictionary[typeof(T)];
      }
   }
}
