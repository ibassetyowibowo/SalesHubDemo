using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class M_KabupatenKota : BaseEntity
	{
		public string? ProvinceCode { get; set; }  
		public string? ProvinceName { get; set; }   
		public string? KabupatenCode { get; set; }  
		public string? KabupatenName { get; set; }  
		public decimal UMP { get; set; }  
		public decimal UMK { get; set; }
	}
}
