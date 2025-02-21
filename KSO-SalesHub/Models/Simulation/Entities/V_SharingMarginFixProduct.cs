namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class V_SharingMarginFixProduct
	{
		public string? TypeCode { get; set; }
		public string? GradingCode { get; set; }
		public string? ProductCode { get; set; }
		public decimal Margin { get; set; }
		public decimal Sharing { get; set; }
		public decimal MaS { get; set; } 
	}
}
