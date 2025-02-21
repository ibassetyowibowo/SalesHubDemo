using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class M_StationGrading : BaseEntity
	{
		public string? GradingCode { get; set; }
		public string? GradingName { get; set; }
		public int Seq { get; set; }
	}
}
 