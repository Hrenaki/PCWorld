using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Hardware.Components
{
	internal class RAMTechnology : ProductEntity
	{ }

	internal class RAM : ProductEntity
	{
		public Manufacturer Manufacturer { get; set; }
		public RAMTechnology RAMTechnology { get; set; }
		public uint Capacity{ get; set; }
		public ushort ModuleCount{ get; set; }
		public uint Speed{ get; set; }
	}
}
