using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class M_MarginFixProductMatrix: BaseEntity
	{
		public string? TypeCode { get; set; }
		public string? GradingCode { get; set; }
		public string? ProductCode { get; set; }
		public decimal Sharing { get; set; }
	}
}
 