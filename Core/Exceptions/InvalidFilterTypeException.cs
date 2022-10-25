﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
   internal class InvalidFilterTypeException : Exception
   {
      public InvalidFilterTypeException(string baseType, string currentType) :
         base($"{baseType} must be a type of {currentType}")
      {}
   }
}