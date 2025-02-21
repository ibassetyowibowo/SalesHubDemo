using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class M_ParameterLevel : BaseEntity
	{
		public string? Paramkey1 { get; set; }
		public string? Paramkey2 { get; set; }
		public string? Paramkey3 { get; set; }
		public string? Paramkey4 { get; set; }
		public string? Paramvalue { get; set; }
	}
}
