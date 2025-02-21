using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
    public class T_SummaryCalculator : BaseEntity
    {
        public string? SummaryNo { get; set; }
        public bool IsAddingUMP { get; set; } = false;
        public string? NomorSPBU { get; set; }
        public string? LokasiSPBU { get; set; }
        public string? TaxStatus { get; set; }
        public string? SPBUName { get; set; }
        public string? SPBUType { get; set; }
        public string? SPBUGrading { get; set; }
        public string? UMK { get; set; }

        // Changed float to double for more precision
        public double PropSharingMarginPTPR { get; set; } = 0;
        public double PropSharingMarginMitra { get; set; } = 0;
        public double ProductBBMPercentage { get; set; } = 0;
        public double ProductBBKPercentage { get; set; } = 0;

        public int TotalSalesMonthly { get; set; } = 0;
        public int TotalSalesPerDay { get; set; } = 0;

        public decimal TotalMonthlySalesCurency { get; set; } = 0;
        public decimal TotalOpexExTaxCurency { get; set; } = 0;
        public decimal TotalPPN11Curency { get; set; } = 0;
        public decimal TotalProfitExSSMCurency { get; set; } = 0;
        public decimal TotalProfitInOHCurency { get; set; } = 0;
        public decimal TotalProfitExOHCurency { get; set; } = 0;

        public string? ResumeKelayakan { get; set; }

        public decimal FeeStartUpCurency { get; set; } = 0;
        public decimal FeePettyCashCurency { get; set; } = 0;
        public decimal FeePickUpServiceCurency { get; set; } = 0;
        public decimal FeeSewaMtcIctCurency { get; set; } = 0;
        public decimal MdrEdcCurency { get; set; } = 0;
        public decimal SalaryAdminPengawasCurency { get; set; } = 0;
        public decimal SalaryBurCurency { get; set; } = 0;
        public decimal InsentiveCurency { get; set; } = 0;
        public string? SchemaRecommendation { get; set; } 
    }

}
