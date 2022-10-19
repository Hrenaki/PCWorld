using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
	internal class EntityWrongTypeException : Exception
	{
		public EntityWrongTypeException(string entityType, string requiredTypeName) :
			base($"{entityType} instance must be a type of {requiredTypeName}")
		{ }
	}
}