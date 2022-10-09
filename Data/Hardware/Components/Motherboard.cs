using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Hardware.Components
{
	internal class MotherboardChipset
	{ }

	internal class Motherboard : ProductEntity
	{
		public Manufacturer Manufacturer { get; set; }
		public ProcessorSocket Socket{ get; set; }
		public MotherboardChipset Chipset { get; set; }
		public ushort RAMSocketCount{ get; set; }
		public string RAMTechnology{ get; set; }
		public uint MaxRAM{ get; set; }
	}
}
