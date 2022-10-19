using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
   public abstract class UserEntity
   {
      public string Name { get; set; }
      public string PasswordHash { get; set; }
      public string Email { get; set; }

      public UserRoleEntity UserRoleEntity { get; set; }
   }
}