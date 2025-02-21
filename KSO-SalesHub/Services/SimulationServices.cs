using AutoMapper;
using KSO_SalesHub.Models.Simulation;
using KSO_SalesHub.Models.Simulation.Dto;
using KSO_SalesHub.Models.Simulation.Entities;
using KSO_SalesHub.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Globalization;

namespace KSO_SalesHub.Services
{
	public class SimulationServices
	{
		private readonly IMapper _mapper;
		private readonly CalculatorUtils _calculator;
		private readonly SimulationDbContext _context;

		private readonly EmailServices _email;

		public SimulationServices(
			SimulationDbContext context,
			CalculatorUtils calculator,
			IMapper mapper,
			EmailServices email
			)

		{
			_mapper = mapper;
			_context = context;
			_calculator = calculator;
			_email = email;
		}


		public async Task<(bool Status, SimulationContainer Data, string Message)> InitAsync(SimulationContainer data, string UserType)
		{
			var status = false;
			var message = string.Empty;

			try
			{
				var defaultItem = new SelectListItem
				{
					Value = "",
					Text = " -- Pilih --",
					Selected = true
				};

				data._taxStatusListItem = _context.M_ParameterLevel
					.Where(x => x.Paramkey1 == "tax-status")
					.Select(x => new SelectListItem
					{
						Value = x.Paramvalue,
						Text = "<p style='font-size:11px;' class='mt-0 mb-0'>" + x.Paramvalue + "</p>"
					}).ToList() ?? new List<SelectListItem>();

				data._SPBUTypeListItem = _context.M_StationType
					.Select(x => new SelectListItem
					{
						Value = x.TypeCode,
						Text = "<p style='font-size:11px;' class='mt-0 mb-0'>" + x.TypeName + "</p>"
					}).ToList() ?? new List<SelectListItem>();

				data._SPBUGradingListItem = _context.M_StationGrading
					.Select(x => new SelectListItem
					{
						Value = x.GradingCode,
						Text = "<p style='font-size:11px;' class='mt-0 mb-0'>" + x.GradingName + "</p>"
					}).ToList() ?? new List<SelectListItem>();


				data._LokasiSPBUListItem = _context.M_KabupatenKota
					.Select(x => new SelectListItem
					{
						Value = x.UMP.ToString() + "___" + x.KabupatenCode + "___" + x.ProvinceCode,
						Text = "<p style='font-size:13px;' class='mt-0 mb-0'>" + x.KabupatenName + " - " + x.ProvinceName + "" +
							   "<span style='font-size:10px; color:gray;'> <i>UMP terakhir: " +
							   x.UMP.ToString("C0", new CultureInfo("id-ID")) +
							   "</span></i></p>"
					}).ToList() ?? new List<SelectListItem>();

				if (data._calculator._productDtoLs.Count == 0)
				{
					data._calculator._productDtoLs = _context.M_Product
						.Select(x => new ProductDto
						{
							ProductCode = x.ProductCode,
							ProductName = x.ProductName,
							Price = x.Price,
							Category = x.Category,
							MarginDefault = x.MarginDefault,
						}).ToList() ?? new List<ProductDto>();
				}

				if (UserType == "MITRA")
				{
					if (string.IsNullOrEmpty(data._station.EmailOwner) && string.IsNullOrEmpty(data._station.NamaBadanUsaha) && string.IsNullOrEmpty(data._station.StationNo))
					{
						data._NoSPBUListItem = new List<SelectListItem> { new SelectListItem { Value = "LAINNYA", Text = "LAINNYA" } };
					}
					else
					{
						data._NoSPBUListItem = _context.M_Station.Where(x =>
							(!string.IsNullOrEmpty(data._station.EmailOwner) && x.EmailOwner == data._station.EmailOwner) ||
							(!string.IsNullOrEmpty(data._station.NamaBadanUsaha) && x.NamaBadanUsaha == data._station.NamaBadanUsaha) ||
							(!string.IsNullOrEmpty(data._station.StationNo) && x.StationNo == data._station.StationNo)
							).OrderByDescending(x => x.CreatedAt).Take(1000)
						.Select(x => new SelectListItem
						{
							Value = x.StationNo,
							Text = "<p style='font-size:11px;'><b>" + x.StationNo + "</b> - " + (!string.IsNullOrEmpty(x.StationName) ? x.StationName : x.StationKabkot + " / " + x.StationProvince) + "</p><span style='font-size:10px; color:gray;'><i>" + x.StationAddress + "...</i></span>"
						}).ToList() ?? new List<SelectListItem>();
					}
				}
				else
				{
					data._NoSPBUListItem = _context.M_Station.OrderByDescending(x => x.CreatedAt).Take(1000)
						.Select(x => new SelectListItem
						{
							Value = x.StationNo,
							Text = "<p style='font-size:11px;'><b>" + x.StationNo + "</b> - " + (!string.IsNullOrEmpty(x.StationName) ? x.StationName : x.StationKabkot + " / " + x.StationProvince) + "</p><span style='font-size:10px; color:gray;'><i>" + x.StationAddress + "...</i></span>"
						}).ToList() ?? new List<SelectListItem>();
				}

				data._taxStatusListItem.Insert(0, defaultItem);
				data._SPBUTypeListItem.Insert(0, defaultItem);
				data._SPBUGradingListItem.Insert(0, defaultItem);
				data._LokasiSPBUListItem.Insert(0, defaultItem);
				data._NoSPBUListItem.Insert(0, defaultItem);

				status = true;
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
			finally
			{
				// logic here
			}

			return (status, data, message);
		}

		public async Task<(bool Status, StationDto Data, string Message)> InitRegisterAsync()
		{
			var status = false;
			var message = string.Empty;
			var data = new StationDto();
			try
			{
				var defaultItem = new SelectListItem
				{
					Value = "",
					Text = " -- Pilih --",
					Selected = true
				};

				data._lossesListItem = new List<SelectListItem> {
					new SelectListItem { Value ="less_than_05", Text=" ≤ 0.5% " },
					new SelectListItem { Value ="great_than_05", Text=" ≥ 0.5% " }
				};

				data._sarfasListItem = new List<SelectListItem> {
					new SelectListItem { Value ="Terawat", Text=" Terawat " },
					new SelectListItem { Value ="Tidak Terawat", Text=" Tidak Terawat " }
				};

				data._komponenProdukListItem = new List<SelectListItem> {
					new SelectListItem { Value ="JBT", Text=" JBT (Biosolar) " },
					new SelectListItem { Value ="JBU", Text=" JBU (Pertamax Series dan Dex Series)" }
				};

				data._kondisiLokasiListItem = new List<SelectListItem> {
					new SelectListItem { Value ="Dekat COCO & TBBM", Text=" Dekat COCO & TBBM " },
					new SelectListItem { Value ="Tidak Dekat COCO & TBBM", Text=" Tidak Dekat COCO & TBBM" }
				};

				data._kendalaSPBUListItem = new List<SelectListItem> {
					new SelectListItem { Value ="Masalah Financial", Text=" Masalah Financial " },
					new SelectListItem { Value ="Masalah Operasional", Text=" Masalah Operasional" },
					new SelectListItem { Value ="Tidak Ada Kendala", Text=" Tidak Ada Kendala" }
				};


				data._SPBUGradingListItem = _context.M_StationGrading
					.Select(x => new SelectListItem
					{
						Value = x.GradingCode,
						Text = "<p style='font-size:11px;'>" + x.GradingName + "</p>"
					}).ToList() ?? new List<SelectListItem>();

				data._LokasiSPBUListItem = _context.M_KabupatenKota
					.Select(x => new SelectListItem
					{
						Value = x.UMP.ToString() + "___" + x.KabupatenCode + "___" + x.ProvinceCode,
						Text = "<p style='font-size:14px;' class='mt-0 mb-0'>" + x.KabupatenName + " - " + x.ProvinceName + "" +
							   "<span style='font-size:13px; color:gray;'> <i>UMP terakhir: " +
							   x.UMP.ToString("C0", new CultureInfo("id-ID")) +
							   "</span></i></p>"
					}).ToList() ?? new List<SelectListItem>();

				data._lossesListItem.Insert(0, defaultItem);
				data._sarfasListItem.Insert(0, defaultItem);
				data._komponenProdukListItem.Insert(0, defaultItem);
				data._kondisiLokasiListItem.Insert(0, defaultItem);
				data._kendalaSPBUListItem.Insert(0, defaultItem);

				data._SPBUTypeListItem.Insert(0, defaultItem);
				data._SPBUGradingListItem.Insert(0, defaultItem);
				data._LokasiSPBUListItem.Insert(0, defaultItem);

				status = true;
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
			finally
			{
				// logic here
			}

			return (status, data, message);
		}


		public async Task<(bool Status, SimulationContainer Data, string Message)> GetSummaryByIdAsync(string SummaryId)
		{
			var status = false;
			var message = string.Empty;
			var data = new SimulationContainer();

			try
			{
				var SummaryCalculator = _context.T_SummaryCalculator.Where(x => x.SummaryNo == SummaryId).FirstOrDefault();
				if (SummaryCalculator != null)
				{
					var dtCalculator = _mapper.Map<CalculatorDto>(SummaryCalculator);
					var dtSummary = _mapper.Map<SummaryCalculatorDto>(SummaryCalculator);

					data._calculator = dtCalculator;
					data.ResultCalculator = dtSummary;
				}
				var SummaryCalculatorDetail = _context.T_SummaryCalculatorDetail.Where(x => x.SummaryNo == SummaryId).ToList();
				if (SummaryCalculatorDetail != null)
				{
					var dtSummaryDetail = _mapper.Map<List<SummaryCalculatorDetailDto>>(SummaryCalculatorDetail);
					data.ResultCalculator.DetailSummary = dtSummaryDetail;
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
			finally
			{
				// logic here
			}

			return (status, data, message);
		}


		public async Task<(bool Status, SummaryCalculatorDto Data, string Message)> GetSummaryAsync(
			SimulationContainer data,
			string UserType,
			string SummaryNo,
			decimal PropSharingMarginPTPR,
			decimal PropSharingMarginMitra,
			List<int> salesMonthlyforBBM,
			List<SummaryCalculatorDetailDto> salesMonthly,
			int salesMonthlyTotal,
			double BBMPrecentage,
			List<M_ParameterLevel> CalculatorFee
			)
		{
			var status = false;
			var message = string.Empty;
			var outputData = new SummaryCalculatorDto();
			try
			{
				foreach (var item in data._calculator._productDtoLs)
				{
					var AmountMarginDefault = item.MarginDefault;
					var marginMatrixProduct = _calculator.marginMatrixProductView(data, item.ProductCode ?? "");
					var marginMatrixforSelisih = _calculator.getMarginMatrixProductforSelisihView(data, item.ProductCode ?? "");
					var _basicPrice = _calculator.getBasicPrice(item.Price, AmountMarginDefault);

					var marginForGrossMargin = _calculator.getMarginforGrossMargin(data._calculator.isAddingUMP, marginMatrixProduct);
					var marginForSelisihSingleMargin = _calculator.getMarginforSelisihSingleMargin(data._calculator.isAddingUMP, AmountMarginDefault, marginMatrixforSelisih);

					var productInput = new SummaryCalculatorDetailDto()
					{
						SummaryNo = SummaryNo,
						ProductCode = item.ProductCode,
						ProductName = item.ProductName,
						SalesPerDay = _calculator.getAmountSalesPerDay(item.SalesPerMonth),
						SalesMonthly = item.SalesPerMonth,
						PriceCurrency = item.Price,
						MonthlySalesCurency = _calculator.getMonthlySalesCurency(item.Price, item.SalesPerMonth),
						MarginDodoCodoNormal = marginMatrixProduct,
						MarginDodoCodoNormalAdding5 = _calculator.getMarginDodoCodoNormalAdding5(marginMatrixProduct),
						MarginPTPR = AmountMarginDefault,
						SelisihSingleMarginNormal = _calculator.getSelisihSingleMarginNormal(AmountMarginDefault, marginMatrixforSelisih),
						SelisihSingleMarginAdding5 = _calculator.getSelisihSingleMarginAdding5(AmountMarginDefault, marginMatrixforSelisih),
						BasicPrice = _basicPrice,
						PPH22Currency = _calculator.getPPH22Currency(item.SalesPerMonth, _basicPrice),
						OverHeadHOCurrency = _calculator.getOverHeadHOCurrency(item.SalesPerMonth),
						GrossMarginMonthlySpbuCurrency = _calculator.getGrossMarginMonthlySpbuCurrency(item.SalesPerMonth, marginForGrossMargin),
						MarginSelisihSingleMarginCurrency = _calculator.getMarginSelisihSingleMarginCurrency(item.SalesPerMonth, marginForSelisihSingleMargin),
						MarginSharingPTPRCurrency = _calculator.getMarginSharingPTPRCurrency(item.SalesPerMonth, PropSharingMarginPTPR, marginForGrossMargin),
						SharingMitraCurrency = _calculator.getSharingMitraCurrency(item.SalesPerMonth, marginForGrossMargin, PropSharingMarginMitra)
					};

					outputData.DetailSummary.Add(productInput);
				}

				outputData.SummaryNo = SummaryNo;
				outputData.PropSharingMarginPTPR = Math.Round((double)PropSharingMarginPTPR * 100, 2);
				outputData.PropSharingMarginMitra = Math.Round((double)PropSharingMarginMitra * 100, 2);

				outputData.ProductBBMPercentage = BBMPrecentage;
				outputData.ProductBBKPercentage = 100 - BBMPrecentage;
				outputData.TotalMonthlySalesCurency = _calculator.getTotalMonthlySalesCurency(outputData.DetailSummary);
				outputData.FeeStartUpCurency = _calculator.getFeeStartUpCurency(CalculatorFee);
				outputData.FeePettyCashCurency = _calculator.getFeePettyCashCurency(CalculatorFee);
				outputData.FeePickUpServiceCurency = _calculator.getFeePickUpServiceCurency(CalculatorFee);
				outputData.FeeSewaMtcIctCurency = _calculator.getFeeSewaMtcIctCurency(CalculatorFee);
				outputData.SalaryAdminPengawasCurency = _calculator.getSalaryAdminPengawasCurency(CalculatorFee);
				outputData.SalaryBurCurency = _calculator.getSalaryBurCurency(CalculatorFee);
				outputData.InsentiveCurency = _calculator.getInsentiveCurency(CalculatorFee);
				outputData.MdrEdcCurency = _calculator.getMdrEdcCurency(outputData.TotalMonthlySalesCurency);

				outputData.TotalOpexExTaxCurency = _calculator.getTotalOpexExTaxCurency(
					outputData.FeeStartUpCurency,
					outputData.FeePettyCashCurency,
					outputData.FeePickUpServiceCurency,
					outputData.FeeSewaMtcIctCurency,
					outputData.SalaryAdminPengawasCurency,
					outputData.SalaryBurCurency,
					outputData.InsentiveCurency,
					outputData.MdrEdcCurency);

				outputData.TotalPPN11Curency = _calculator.getTotalPPN11Curency(outputData, data._calculator.TaxStatus);

				outputData.TotalProfitExSSMCurency = _calculator.getTotalProfitExSSMCurency(
					outputData.DetailSummary.Select(x => x.MarginSharingPTPRCurrency).Sum(),
					outputData.DetailSummary.Select(x => x.PPH22Currency).Sum(),
					outputData.TotalOpexExTaxCurency,
					outputData.TotalPPN11Curency);

				outputData.TotalProfitInOHCurency = _calculator.getTotalProfitInOHCurency(
					outputData.DetailSummary.Select(x => x.MarginGrossMarginPTPRCurrency).Sum(),
					outputData.TotalOpexExTaxCurency,
					outputData.DetailSummary.Select(x => x.PPH22Currency).Sum(),
					outputData.TotalPPN11Curency,
					outputData.DetailSummary.Select(x => x.OverHeadHOCurrency).Sum());

				outputData.TotalProfitExOHCurency = _calculator.getTotalProfitExOHCurency(
					outputData.DetailSummary.Select(x => x.MarginGrossMarginPTPRCurrency).Sum(),
					outputData.TotalOpexExTaxCurency,
					outputData.DetailSummary.Select(x => x.PPH22Currency).Sum(),
					outputData.TotalPPN11Curency);

				outputData.ResumeKelayakan = _calculator.getResumeKelayakan(outputData.TotalProfitInOHCurency);

				status = true;
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
			finally
			{
				// logic here
			}

			return (status, outputData, message);
		}


		public async Task<(bool Status, SimulationContainer Data, string Message)> GetResultSimulationAsync(SimulationContainer container, string UserType)
		{
			var status = false;
			var message = string.Empty;

			try
			{
				var dtStation = _context.M_Station.Where(x => x.StationNo == container._calculator.NomorSPBU).FirstOrDefault() ?? new M_Station();
				var dataLokasi = _context.M_KabupatenKota.Where(x => x.ProvinceCode == dtStation.StationProvince && x.KabupatenCode == dtStation.StationKabkot).FirstOrDefault() ?? new M_KabupatenKota();

				container._calculator.isAddingUMP = _calculator.getFlagAddingMarginUMP(dataLokasi.UMP);

				var SummaryNo = (await GenerateId("CALC")).TicketId;
				var PropSharingMarginPTPR = _calculator.getinitPropSharingMarginPTPR(container._calculator.TaxStatus);
				var PropSharingMarginMitra = _calculator.getinitPropSharingMarginMitra(PropSharingMarginPTPR);
				var salesMonthlyforBBM = _calculator.getSalesMonthlyforBBM(container);
				var salesMonthly = _calculator.getSalesMonthlyPerProduct(container);
				var salesMonthlyTotal = _calculator.getTotalSalesMonthlyProduct(salesMonthly);

				var BBMPrecentage = Math.Ceiling((Convert.ToDouble(salesMonthlyforBBM.Sum()) / Convert.ToDouble(salesMonthlyTotal)) * 100);
				var CalculatorFee = _calculator.getParameterFeeCalculator();
				container = _calculator.marginMatrixProductList(container);

				for (var i = PropSharingMarginPTPR; i <= 100; i++)
				{
					var output = await GetSummaryAsync(
						container,
						UserType,
						SummaryNo,
						(decimal)PropSharingMarginPTPR / 100,
						(decimal)PropSharingMarginMitra / 100,
						salesMonthlyforBBM,
						salesMonthly,
						salesMonthlyTotal,
						BBMPrecentage,
						CalculatorFee
						);

					if (output.Data.ResumeKelayakan == "LAYAK")
					{
						container.ResultCalculator = output.Data;
						break;
					}

					if (i == 100)
					{
						container.ResultCalculator = output.Data;
					}

					PropSharingMarginPTPR += 1;
					PropSharingMarginMitra -= 1;
				}

				if (container.ResultCalculator.ResumeKelayakan == "LAYAK")
				{
					container.ResultCalculator.SchemaRecommendation = (await GetRecomendation(container)).Recomendation;
				}

				if (!string.IsNullOrEmpty(container._calculator.LokasiSPBU))
				{
					string[] parts = container._calculator.LokasiSPBU.Split(new string[] { "___" }, StringSplitOptions.None);
					if (parts.Length >= 3)
					{
						string alamat = parts[1] + " - " + parts[2];
						container._calculator.LokasiSPBU = alamat;
					}
				}

				if (UserType == "MITRA")
				{
					container._calculator.UMK = Convert.ToString(dataLokasi.UMP);
					container._calculator.LokasiSPBU = dataLokasi.KabupatenName + " - " + dataLokasi.ProvinceName;

					await InsertNewSimulationAsync(container.ResultCalculator, container._calculator);

					container._station = _context.M_Station.Where(x => x.StationNo == container._calculator.NomorSPBU).FirstOrDefault() ?? new M_Station();
					await _email.PostNotifEmailSummaryCalculator(container);
				}
			}
			catch (Exception ex)
			{

			}

			return (status, container, message);
		}


		public async Task<(bool Status, string Recomendation, string Msg)> GetRecomendation(SimulationContainer container)
		{
			var outputRecomendation = "";
			try
			{

				var SalesTotalPerDay = container.ResultCalculator.DetailSummary.Select(x => x.SalesPerDay).Sum() / 1000;
				var station = _context.M_Station.Where(x => x.StationNo == container._calculator.NomorSPBU).FirstOrDefault() ?? new M_Station();
				var dataLokasi = _context.M_KabupatenKota.Where(x => x.ProvinceCode == station.StationProvince && x.KabupatenCode == station.StationKabkot).FirstOrDefault() ?? new M_KabupatenKota();
				outputRecomendation = _calculator.getPartnershipSchemaRecommendation(
					SalesTotalPerDay,
					container._calculator.SPBUGrading,
					station?.Losses,
					container._calculator.TaxStatus,
					station.Sarfas,
					station.KondisiLokasi,
					station.KomponenProduk,
					station.KendalaSPBU,
					dataLokasi.UMP
					);

				return (true, outputRecomendation, string.Empty);
			}
			catch (Exception ex)
			{
				return (false, string.Empty, ex.Message);
			}
		}


		public async Task<(bool Status, M_Station Data, string Message)> GetDataSpbuAsync(string StationKey)
		{
			var status = false;
			var message = string.Empty;
			var dtStation = new M_Station();

			try
			{
				dtStation = _context.M_Station.Where(x => (x.StationNo == StationKey || x.EmailOwner == StationKey) && x.IsDeleted == false).FirstOrDefault();

				status = true;
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}

			return (status, dtStation, message);
		}

		public async Task<(bool Status, string Data, string Message)> InsertNewSimulationAsync(SummaryCalculatorDto summary, CalculatorDto calcualtor)
		{
			var status = false;
			var message = string.Empty;

			try
			{

				var dtSummary = _mapper.Map<T_SummaryCalculator>(calcualtor);
				_mapper.Map(summary, dtSummary);

				var dtSummaryDetail = _mapper.Map<List<T_SummaryCalculatorDetail>>(summary.DetailSummary);

				var executionStrategy = _context.Database.CreateExecutionStrategy();
				await executionStrategy.Execute(
					   async () =>
					   {
						   using (var _trans = await _context.Database.BeginTransactionAsync())
						   {
							   try
							   {
								   _context.T_SummaryCalculator.Add(dtSummary);
								   _context.T_SummaryCalculatorDetail.AddRange(dtSummaryDetail);

								   await _context.SaveChangesAsync();
								   await _trans.CommitAsync();

								   status = true;
							   }
							   catch (Exception ex)
							   {
								   await _trans.RollbackAsync();
							   }
						   }
					   });

				status = true;
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
			finally
			{
				// logic here
			}

			return (status, "", message);
		}


		public async Task<(bool Status, StationDto Data, string Message)> RegisterNewSpbuAsync(StationDto data)
		{
			var status = false;
			var message = string.Empty;
			var dtStation = new M_Station();

			try
			{
				data.CreatedBy = data?.EmailOwner ?? "reg";
				data.StationName = data.NamaBadanUsaha;

				if (!string.IsNullOrEmpty(data.txt_alamat))
				{
					string[] parts = data.txt_alamat.Split(new string[] { "___" }, StringSplitOptions.None);
					if (parts.Length >= 3)
					{
						string alamat = parts[1] + " - " + parts[2];
						data.StationProvince = parts[2];
						data.StationKabkot = parts[1];
						data.txt_umk = parts[0];
					}
				}

				M_Station dtTempStation = _mapper.Map<M_Station>(data);
				dtStation = _context.M_Station.Where(x => x.StationNo == data.StationNo && x.IsDeleted == false).FirstOrDefault();
				if (dtStation != null)
				{
					dtStation.StationNo = data.StationNo;
					dtStation.NamaBadanUsaha = data.NamaBadanUsaha;
					dtStation.StationAddress = data.StationAddress;
					dtStation.NamaLengkapOwner = data.NamaLengkapOwner;
					dtStation.TlpOwner = data.TlpOwner;
					dtStation.EmailOwner = data.EmailOwner;

					dtStation.Losses = data.Losses;
					dtStation.Sarfas = data.Sarfas;
					dtStation.KomponenProduk = data.KomponenProduk;
					dtStation.KendalaSPBU = data.KendalaSPBU;
					dtStation.KondisiLokasi = data.KondisiLokasi;
				}
				else
				{
					dtStation = dtTempStation;
				}

				var executionStrategy = _context.Database.CreateExecutionStrategy();
				await executionStrategy.Execute(
					   async () =>
					   {
						   using (var _trans = await _context.Database.BeginTransactionAsync())
						   {
							   try
							   {
								   if (dtStation.Id != 0)
									   _context.M_Station.Update(dtStation);
								   else
									   _context.M_Station.Add(dtStation);

								   await _context.SaveChangesAsync();
								   await _trans.CommitAsync();

								   status = true;
							   }
							   catch (Exception ex)
							   {
								   await _trans.RollbackAsync();
							   }
						   }
					   });

				status = true;
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
			finally
			{
				// logic here
			}

			return (status, data, message);
		}


		public async Task<(bool Status, string TicketId, string Msg)> GenerateId(string Tipe)
		{
			try
			{
				var Tahun = DateTime.Now.Year.ToString();
				string TicketId = "REQ/" + Tahun + "/" + Tipe.ToUpper() + "/000001";
				long NoUrut = 1;
				string GenNoUrut = string.Empty;

				var Data = _context.T_SummaryCalculator
					.Where(w => w.CreatedAt.Year == DateTime.Now.Year && !string.IsNullOrEmpty(w.SummaryNo))
					.OrderBy(o => o.Id)
					.Select(s => new { SummaryNo = s.SummaryNo })
					.LastOrDefault();

				if (Data == null)
				{
					return (true, TicketId, string.Empty);
				}
				else
				{
					NoUrut = Convert.ToInt64(Convert.ToInt64(Data.SummaryNo.Split("/")[3]));
					GenNoUrut = Data.SummaryNo.Substring(0, Data.SummaryNo.Length - 6) + addZero(NoUrut + 1, 6);
				}

				return (true, GenNoUrut, string.Empty);
			}
			catch (Exception ex)
			{
				return (false, string.Empty, ex.Message);
			}
		}

		public string addZero(long input, int expectedLength)
		{
			string returnValue = string.Empty;

			for (int i = 0; i < expectedLength - input.ToString().Length; i++)
			{
				returnValue += "0";

			}

			returnValue = returnValue + input;
			return returnValue;
		}


	}
}
