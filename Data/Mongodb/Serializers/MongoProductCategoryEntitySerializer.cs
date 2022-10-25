using Data.Common;
using Data.Mongodb.Products;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mongodb.Serializers
{
   public class MongoProductCategoryEntitySerializer : SerializerBase<ProductCategoryEntity>
   {
      public override ProductCategoryEntity Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
      {
         var serializer = BsonSerializer.LookupSerializer<MongoProductCategoryEntity>();
         return serializer.Deserialize(context);
      }

      public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ProductCategoryEntity value)
      {
         var mongoProductCategoryEntity = (value as MongoProductCategoryEntity) ?? throw new ArgumentException($"ProductCategoryEntity instance must be a type of '{nameof(MongoProductCategoryEntity)}'");
         base.Serialize(context, args, mongoProductCategoryEntity);
      }
   }
}