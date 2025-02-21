using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class M_MarginSimulationProduct : BaseEntity
	{
		public string? GradingCode { get; set; }
		public string? ProductCode { get; set; }
		public decimal Margin { get; set; }
	}
}
