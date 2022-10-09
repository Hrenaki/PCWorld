using Core.UserZone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class UserRoleEntity : Entity
	{
		public string Name { get; set; }
	}

	public class UserEntity : Entity
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }

		public int UserRoleId { get; set; }
		[ForeignKey(nameof(UserRoleId))]
		public UserRoleEntity UserRole { get; set; }
	}
}