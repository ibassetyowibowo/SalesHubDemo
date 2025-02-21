using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class M_Product : BaseEntity
	{
		public string? ProductCode { get; set; }
		public string? ProductName { get; set; }
		public string? Category { get; set; } 
		public decimal Price { get; set; } = 0; 
		public decimal MarginDefault { get; set; } = 0;
		public int Seq { get; set; } = 1;
	}
}
