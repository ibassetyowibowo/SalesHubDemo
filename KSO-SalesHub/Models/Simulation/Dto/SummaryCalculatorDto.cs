namespace KSO_SalesHub.Models.Simulation.Dto
{
	public class SummaryCalculatorDto
	{
        public SummaryCalculatorDto()
        {
			DetailSummary = new List<SummaryCalculatorDetailDto>();

		}

		public string? SummaryNo { get; set; }
		public double PropSharingMarginPTPR { get; set; }
		public double PropSharingMarginMitra { get; set; }
        public double ProductBBMPercentage { get; set; }
        public double ProductBBKPercentage { get; set; }


        public int TotalSalesMonthly { get; set; }
        public int TotalSalesPerDay { get; set; } 
        public decimal TotalMonthlySalesCurency { get; set; }
        
        public decimal TotalOpexExTaxCurency { get; set; }
        public decimal TotalPPN11Curency { get; set; }
        public decimal TotalProfitExSSMCurency { get; set; }
        public decimal TotalProfitInOHCurency { get; set; }
        public decimal TotalProfitExOHCurency { get; set; }

        public string ResumeKelayakan { get; set; }
        public string SchemaRecommendation { get; set; } 


		public decimal FeeStartUpCurency { get; set; }
		public decimal FeePettyCashCurency { get; set; }
		public decimal FeePickUpServiceCurency { get; set; } 
		public decimal FeeSewaMtcIctCurency { get; set; }  
		public decimal MdrEdcCurency { get; set; } 
		public decimal SalaryAdminPengawasCurency { get; set; } 
		public decimal SalaryBurCurency { get; set; } 
		public decimal InsentiveCurency { get; set; }

		public List<SummaryCalculatorDetailDto> DetailSummary { get; set; }

	}

	public class SummaryCalculatorDetailDto
	{
        public SummaryCalculatorDetailDto()
        {
                
        }

		public string? SummaryNo { get; set; }

		public string? ProductCode { get; set; }
		public string? ProductName { get; set; }
        public int SalesMonthly { get; set; }
        public int SalesPerDay { get; set; }

        public decimal PriceCurrency { get; set; } 
        public decimal MonthlySalesCurency { get; set; }

        public decimal MarginDodoCodoNormal { get; set; }
        public decimal MarginDodoCodoNormalAdding5 { get; set; }
        public decimal MarginPTPR { get; set; }

        public decimal SelisihSingleMarginNormal { get; set; }
        public decimal SelisihSingleMarginAdding5 { get; set; }
        public decimal BasicPrice { get; set; }


		public decimal PPH22Currency { get; set; }
		public decimal OverHeadHOCurrency { get; set; } 
		public decimal GrossMarginMonthlySpbuCurrency { get; set; }  
		public decimal MarginSelisihSingleMarginCurrency { get; set; }   
		public decimal MarginSharingPTPRCurrency { get; set; }   
		public decimal MarginGrossMarginPTPRCurrency
		{
			get
			{
				return MarginSharingPTPRCurrency + MarginSelisihSingleMarginCurrency;
			}
		}

		public decimal SharingMitraCurrency { get; set; }
	}
}
