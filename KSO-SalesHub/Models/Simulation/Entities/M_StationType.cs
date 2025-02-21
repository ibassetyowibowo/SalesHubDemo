using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class M_StationType: BaseEntity
	{
		public string? TypeCode { get; set; }
		public string? TypeName { get; set; }
		public int Seq { get; set; }
	}
}
