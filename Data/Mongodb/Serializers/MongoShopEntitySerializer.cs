using Data.Common;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mongodb.Serializers
{
   internal class MongoShopEntitySerializer : SerializerBase<ShopEntity>
   {
      public override ShopEntity Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
      {
         var serializer = BsonSerializer.LookupSerializer<MongoShopEntity>();
         return serializer.Deserialize(context);
      }

      public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ShopEntity value)
      {
         var mongoShopEntity = (value as MongoShopEntity) ?? throw new ArgumentException($"ShopEntity instance must be a type of '{nameof(MongoShopEntity)}'");
         base.Serialize(context, args, mongoShopEntity);
      }
   }
}