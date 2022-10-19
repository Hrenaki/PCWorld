using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework
{
   public class EfUserRoleEntity : UserRoleEntity
   {
      public int Id { get; set; }
   }

   public class EfUserEntity : UserEntity
   {
      public int Id { get; set; }

      public int UserRoleId { get; set; }
      [ForeignKey(nameof(UserRoleId))]
      public new UserRoleEntity UserRoleEntity { get; set; }
   }
}