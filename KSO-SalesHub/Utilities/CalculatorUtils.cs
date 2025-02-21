using KSO_SalesHub.Models.Simulation;
using KSO_SalesHub.Models.Simulation.Dto;
using KSO_SalesHub.Models.Simulation.Entities;

namespace KSO_SalesHub.Utilities
{
	public class CalculatorUtils
	{
		private readonly SimulationDbContext _context;

		public CalculatorUtils(
			SimulationDbContext context
			)
		{
			_context = context;

		}


		public bool getFlagAddingMarginUMP(decimal UMP)
		{
			bool AddingMarginUMP = false;

			if (UMP >= 3000000)
				AddingMarginUMP = true;

			return AddingMarginUMP; 
		}

		public decimal getinitPropSharingMarginPTPR(string TaxStatus)
		{
			decimal propSharing = 10;
			if (TaxStatus == "PKP")
				propSharing = 20;

			return propSharing;
		}


		public decimal getinitPropSharingMarginMitra(decimal PropSharingMarginPTPR)
		{
			return (decimal)100 - PropSharingMarginPTPR;
		}


		public int getAmountSalesPerDay(int SalesPerMonth, int totalDay = 30)
		{
			return SalesPerMonth / totalDay;
		}

		public decimal getMonthlySalesCurency(decimal ProductPrice, int SalesPerDay)
		{
			return ProductPrice * SalesPerDay;
		}


		public decimal getMarginDodoCodoNormalAdding5(decimal marginMatrixProduct, decimal addingAmount = 5)
		{
			return marginMatrixProduct + addingAmount;
		}


		public decimal getSelisihSingleMarginNormal(decimal AmountMarginDefault, decimal marginMatrixforSelisih)
		{
			return AmountMarginDefault - marginMatrixforSelisih;
		}


		public decimal getSelisihSingleMarginAdding5(decimal AmountMarginDefault, decimal marginMatrixforSelisih, decimal addingAmount = 5)
		{
			return (AmountMarginDefault - marginMatrixforSelisih) - addingAmount;
		}


		public decimal getPPH22Currency(decimal SalesPerMonth, decimal _basicPrice)
		{
			var PPH22Attribute = (decimal)(0.25 / 100);

			return SalesPerMonth * _basicPrice * PPH22Attribute;
		}


		public decimal getOverHeadHOCurrency(decimal SalesPerMonth)
		{
			var OverHeadHOAttribute = 20;

			return SalesPerMonth * OverHeadHOAttribute;
		}


		public decimal getGrossMarginMonthlySpbuCurrency(decimal SalesPerMonth, decimal marginForGrossMargin)
		{
			return SalesPerMonth * marginForGrossMargin;
		}


		public decimal getMarginSelisihSingleMarginCurrency(decimal SalesPerMonth, decimal marginForSelisihSingleMargin)
		{
			return SalesPerMonth * marginForSelisihSingleMargin;
		}


		public decimal getMarginSharingPTPRCurrency(decimal SalesPerMonth, decimal PropSharingMarginPTPR, decimal marginForGrossMargin)
		{
			return SalesPerMonth * PropSharingMarginPTPR * marginForGrossMargin;
		}


		public decimal getSharingMitraCurrency(int SalesPerMonth, decimal marginForGrossMargin, decimal PropSharingMarginMitra)
		{
			return (SalesPerMonth * marginForGrossMargin) * PropSharingMarginMitra;
		}


		public decimal getBasicPrice(decimal hargaJualProduk, decimal marginDefaultPTPR)
		{
			return hargaJualProduk - marginDefaultPTPR;
		}


		public decimal getMarginforGrossMargin(bool isAddingUMP, decimal marginMatrixProduct)
		{
			if (isAddingUMP)
				marginMatrixProduct = marginMatrixProduct + 5;

			return marginMatrixProduct;
		}


		public decimal getMarginforSelisihSingleMargin(bool isAddingUMP, decimal marginDefaultPTPR, decimal marginMatrixforSelisih)
		{
			var output = marginDefaultPTPR - marginMatrixforSelisih;
			if (isAddingUMP)
				output = output - 5;

			return output;
		}


		public decimal getFeeStartUpCurency(List<M_ParameterLevel> data)
		{
			return data.Where(x => x.Paramkey3 == "startup").Select(x => Convert.ToDecimal(x.Paramvalue)).FirstOrDefault();
		}


		public decimal getFeePettyCashCurency(List<M_ParameterLevel> data)
		{
			return data.Where(x => x.Paramkey3 == "pettycash").Select(x => Convert.ToDecimal(x.Paramvalue)).FirstOrDefault();
		}


		public decimal getFeePickUpServiceCurency(List<M_ParameterLevel> data)
		{
			return data.Where(x => x.Paramkey3 == "pickup").Select(x => Convert.ToDecimal(x.Paramvalue)).FirstOrDefault();
		}


		public decimal getFeeSewaMtcIctCurency(List<M_ParameterLevel> data)
		{
			return data.Where(x => x.Paramkey3 == "sewa-mtc-ict").Select(x => Convert.ToDecimal(x.Paramvalue)).FirstOrDefault();
		}


		public decimal getSalaryAdminPengawasCurency(List<M_ParameterLevel> data)
		{
			return data.Where(x => x.Paramkey3 == "salary-admin-pengawas").Select(x => Convert.ToDecimal(x.Paramvalue)).FirstOrDefault();
		}


		public decimal getInsentiveCurency(List<M_ParameterLevel> data)
		{
			return data.Where(x => x.Paramkey3 == "insentive").Select(x => Convert.ToDecimal(x.Paramvalue)).FirstOrDefault();
		}


		public decimal getSalaryBurCurency(List<M_ParameterLevel> data)
		{
			return data.Where(x => x.Paramkey3 == "salary-bur").Select(x => Convert.ToDecimal(x.Paramvalue)).FirstOrDefault();
		}


		public decimal getMdrEdcCurency(decimal TotalMonthlySalesCurrency)
		{
			return (decimal)(0.5 / 100) * TotalMonthlySalesCurrency * ((decimal)20 / 100);
		}


		public decimal getTotalOpexExTaxCurency(
			decimal FeeStartUpCurency,
			decimal FeePettyCashCurency,
			decimal FeePickUpServiceCurency,
			decimal FeeSewaMtcIctCurency,
			decimal SalaryAdminPengawasCurency,
			decimal SalaryBurCurency,
			decimal InsentiveCurency,
			decimal MdrEdcCurency
			)
		{

			return FeeStartUpCurency + FeePettyCashCurency + FeePickUpServiceCurency + FeeSewaMtcIctCurency + SalaryAdminPengawasCurency + SalaryBurCurency + InsentiveCurency + MdrEdcCurency;
		}


		public decimal getTotalPPN11Curency(SummaryCalculatorDto data, string TaxStatus)
		{
			decimal output = 0;

			if (TaxStatus == "PKP")
				output = data.DetailSummary.Select(x => x.SharingMitraCurrency).Sum() * ((decimal)11 / 100);

			return output;
		}


		public decimal getTotalProfitExSSMCurency(
			decimal TotalMarginSharingPTPRCurrency,
			decimal TotalPPH22Currency,
			decimal TotalOpexExTaxCurency,
			decimal TotalPPN11Curency
			)
		{
			return TotalMarginSharingPTPRCurrency - (TotalPPH22Currency + TotalOpexExTaxCurency + TotalPPN11Curency);
		}


		public decimal getTotalProfitInOHCurency(
			decimal TotalMarginGrossMarginPTPRCurrency,
			decimal TotalOpexExTaxCurency,
			decimal TotalPPH22Currency,
			decimal TotalPPN11Curency,
			decimal TotalOverHeadHOCurrency
			)
		{
			return TotalMarginGrossMarginPTPRCurrency - (TotalOpexExTaxCurency + TotalPPH22Currency + TotalPPN11Curency + TotalOverHeadHOCurrency);
		}


		public decimal getTotalProfitExOHCurency(
			decimal TotalMarginGrossMarginPTPRCurrency,
			decimal TotalOpexExTaxCurency,
			decimal TotalPPH22Currency,
			decimal TotalPPN11Curency
			)
		{
			return TotalMarginGrossMarginPTPRCurrency - (TotalOpexExTaxCurency + TotalPPH22Currency + TotalPPN11Curency);
		}



		public string getPartnershipSchemaRecommendation(
			int SalesTotalPerDay,
			string Grading,
			string Losses,
			string TaxStatus,
			string SarfasCondition,
			string LokasiCondition,
			string KomposisiProduk,
			string KendalaSPBU,
			decimal UMK
			)
		{
			var outputRecommendation = "";
			try
			{

				// premise I
				if (
					SalesTotalPerDay <= 10 && Grading == GradingConsts.BASIC && Losses == LossesConsts.less_than_05 && TaxStatus == TaxStatusConsts.NPKP && SarfasCondition == SarfasConsts.Terawat &&
					LokasiCondition == LokasiConditionConsts.Dekat_COCO_TBBM && KomposisiProduk == KomposisiProdukConsts.JBT && KendalaSPBU == KendalaSPBUConsts.Financial && UMK <= 4000000
					)
				{
					outputRecommendation = "TAC, Delta, Full Operate";
				}
				// premise II
				else if (
					SalesTotalPerDay >= 10 && Grading != GradingConsts.BASIC && Losses == LossesConsts.great_than_05 && TaxStatus == TaxStatusConsts.PKP && SarfasCondition == SarfasConsts.Tidak_Terawat &&
					LokasiCondition == LokasiConditionConsts.Tidak_Dekat_COCO_TBBM && KomposisiProduk == KomposisiProdukConsts.JBU && KendalaSPBU == KendalaSPBUConsts.Operasional && UMK >= 4000000
					)
				{
					outputRecommendation = "TAC,,";
				}
				// premise III
				else if (
					SalesTotalPerDay <= 10 && Grading != GradingConsts.BASIC && Losses == LossesConsts.great_than_05 && TaxStatus == TaxStatusConsts.PKP && SarfasCondition == SarfasConsts.Terawat &&
					LokasiCondition == LokasiConditionConsts.Dekat_COCO_TBBM && KomposisiProduk == KomposisiProdukConsts.JBU && KendalaSPBU == KendalaSPBUConsts.Tidak_Ada && UMK <= 4000000
					)
				{
					outputRecommendation = "TAC, Full Operate, Delta";
				}
				// premise IV
				else if (
					SalesTotalPerDay >= 10 && Grading != GradingConsts.BASIC && Losses == LossesConsts.great_than_05 && TaxStatus == TaxStatusConsts.PKP && SarfasCondition == SarfasConsts.Tidak_Terawat &&
					LokasiCondition == LokasiConditionConsts.Tidak_Dekat_COCO_TBBM && KomposisiProduk == KomposisiProdukConsts.JBU && KendalaSPBU == KendalaSPBUConsts.Financial && UMK >= 4000000
					)
				{
					outputRecommendation = "TAC,,";
				}
				// premise V
				else if (
					SalesTotalPerDay <= 10 && Grading == GradingConsts.BASIC && Losses == LossesConsts.less_than_05 && TaxStatus == TaxStatusConsts.NPKP && SarfasCondition == SarfasConsts.Tidak_Terawat &&
					LokasiCondition == LokasiConditionConsts.Tidak_Dekat_COCO_TBBM && KomposisiProduk == KomposisiProdukConsts.JBU && KendalaSPBU == KendalaSPBUConsts.Tidak_Ada && UMK >= 4000000
					)
				{
					outputRecommendation = "TAC, Delta,";
				}
				// premise VI
				else if (
					SalesTotalPerDay >= 10 && Grading != GradingConsts.BASIC && Losses == LossesConsts.great_than_05 && TaxStatus == TaxStatusConsts.PKP && SarfasCondition == SarfasConsts.Tidak_Terawat &&
					LokasiCondition == LokasiConditionConsts.Tidak_Dekat_COCO_TBBM && KomposisiProduk == KomposisiProdukConsts.JBU && KendalaSPBU == KendalaSPBUConsts.Tidak_Ada && UMK >= 4000000
					)
				{
					outputRecommendation = "TAC, Full Operate, Delta";
				}
				else
				{
					outputRecommendation = "Tidak Tersedia";
				}
			}
			catch (Exception ex)
			{

			}

			return outputRecommendation;
		}


		public string getResumeKelayakan(decimal TotalProfitInOHCurency)
		{
			string ResumeKelayakan = "TIDAK LAYAK";

			if (TotalProfitInOHCurency > 0)
				ResumeKelayakan = "LAYAK";

			return ResumeKelayakan;
		}

		public decimal getTotalMonthlySalesCurency(List<SummaryCalculatorDetailDto> data)
		{
			return data.Select(x => x.MonthlySalesCurency).Sum();
		}

		public int getTotalSalesMonthlyProduct(List<SummaryCalculatorDetailDto> data)
		{
			return data.Select(x => x.SalesMonthly).Sum();
		}


		public List<int> getSalesMonthlyforBBM(SimulationContainer container)
		{
			var data = container._calculator._productDtoLs.Where(x => x.Category == "BBM").Select(x => x.SalesPerMonth).ToList();
			return data;
		}


		public List<SummaryCalculatorDetailDto> getSalesMonthlyPerProduct(SimulationContainer container)
		{
			return container._calculator._productDtoLs.Select(
					x => new SummaryCalculatorDetailDto
					{
						ProductCode = x.ProductCode,
						SalesMonthly = x.SalesPerMonth,
					}).ToList();
		}


		public List<M_ParameterLevel> getParameterFeeCalculator()
		{
			return _context.M_ParameterLevel.Where(x => x.Paramkey1 == "calculator" && x.Paramkey2 == "fee").ToList()
					?? new List<Models.Simulation.Entities.M_ParameterLevel>();
		}


		public SimulationContainer marginMatrixProductList(SimulationContainer container)
		{
			container._V_SharingMarginFixProductList = _context.V_SharingMarginFixProduct.Where(x =>
						(x.TypeCode == container._calculator.SPBUType || x.TypeCode == "DODO") &&
						x.GradingCode == container._calculator.SPBUGrading
						).ToList() ?? new List<V_SharingMarginFixProduct>();

			return container;
		}

		public decimal marginMatrixProduct(SimulationContainer container, string ProductCode)
		{
			return _context.V_SharingMarginFixProduct.Where(x =>
						x.TypeCode == container._calculator.SPBUType &&
						x.GradingCode == container._calculator.SPBUGrading &&
						x.ProductCode == ProductCode
						).Select(x => x.MaS).FirstOrDefault();

			//return _context.M_MarginSimulationProduct.Where(x =>
			//x.GradingCode == container._calculator.SPBUGrading &&
			//x.ProductCode == ProductCode
			//).Select(x => x.Margin).FirstOrDefault();
		}

		public decimal marginMatrixProductView(SimulationContainer container, string ProductCode)
		{
			return container._V_SharingMarginFixProductList.Where(x =>
						x.TypeCode == container._calculator.SPBUType &&
						x.GradingCode == container._calculator.SPBUGrading &&
						x.ProductCode == ProductCode
						).Select(x => x.MaS).FirstOrDefault();
		}

		public decimal getMarginMatrixProductforSelisih(SimulationContainer container, string ProductCode)
		{
			return _context.V_SharingMarginFixProduct.Where(x =>
						x.TypeCode == "DODO" &&
						x.GradingCode == container._calculator.SPBUGrading &&
						x.ProductCode == ProductCode
						).Select(x => x.MaS).FirstOrDefault();

			//return _context.M_MarginSimulationProduct.Where(x =>
			//x.GradingCode == container._calculator.SPBUGrading &&
			//x.ProductCode == ProductCode
			//).Select(x => x.Margin).FirstOrDefault();
		}

		public decimal getMarginMatrixProductforSelisihView(SimulationContainer container, string ProductCode)
		{
			return container._V_SharingMarginFixProductList.Where(x =>
						x.TypeCode == "DODO" &&
						x.GradingCode == container._calculator.SPBUGrading &&
						x.ProductCode == ProductCode
						).Select(x => x.MaS).FirstOrDefault();
		}



	}
}
