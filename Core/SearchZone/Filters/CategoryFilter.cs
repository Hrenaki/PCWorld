﻿using Core.SearchZone.Filters.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters
{
   public class CategoryFilter : ICategoryFilter
   {
      public string CategoryName {get; set;}
   }
}