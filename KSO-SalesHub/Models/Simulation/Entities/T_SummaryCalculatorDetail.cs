using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class T_SummaryCalculatorDetail : BaseEntity
	{

		public string? SummaryNo { get; set; }
		public string? ProductCode { get; set; }
		public string? ProductName { get; set; }
		public int SalesMonthly { get; set; } = 0;
		public int SalesPerDay { get; set; } = 0;
		public decimal PriceCurrency { get; set; } = 0;
		public decimal MonthlySalesCurency { get; set; } = 0;
		public decimal MarginDodoCodoNormal { get; set; } = 0;
		public decimal MarginDodoCodoNormalAdding5 { get; set; } = 0;
		public decimal MarginPTPR { get; set; } = 0;
		public decimal SelisihSingleMarginNormal { get; set; } = 0;
		public decimal SelisihSingleMarginAdding5 { get; set; } = 0;
		public decimal BasicPrice { get; set; } = 0;
		public decimal PPH22Currency { get; set; } = 0;
		public decimal OverHeadHOCurrency { get; set; } = 0;
		public decimal GrossMarginMonthlySpbuCurrency { get; set; } = 0;
		public decimal MarginSelisihSingleMarginCurrency { get; set; } = 0;
		public decimal MarginSharingPTPRCurrency { get; set; } = 0;
		public decimal MarginGrossMarginPTPRCurrency { get; set; } = 0;
		public decimal SharingMitraCurrency { get; set; } = 0;
	}
}
