using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Hardware.Components
{
	internal class GraphicsCardChipset : ProductEntity
	{ }

	internal class VRAMType : ProductEntity
	{ }

	internal class GraphicsCard : ProductEntity
	{
		public Manufacturer Manufacturer { get; set; }
		public GraphicsCardChipset Chipset { get; set; }
		public VRAMType VRAM { get; set; }
		public ushort VRAMVolume { get; set; }
		public uint BusWidth { get; set; }
	}
}
