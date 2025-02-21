using KSO_SalesHub.Models.Common;
using KSO_SalesHub.Models.Simulation;
using KSO_SalesHub.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;

namespace KSO_SalesHub.Services
{
	public class EmailServices
	{

		private readonly HttpClientAPIHandler _httpClientHandler;

		private string ApiUrl = GeneralSetting.AppSettingValue("Server", "APIMail");


		public EmailServices(HttpClientAPIHandler httpClientHandler)
		{

			_httpClientHandler = httpClientHandler;
		}


		public async Task<ApiResponse> PostNotifEmailSummaryCalculator(SimulationContainer container)
		{

			try
			{

				var SystemEnv = 1;
				var DtContentSubject = "[Mitra SPBU] Result Simulasi Calculator - SPBU " + container._station.NamaBadanUsaha?.ToUpper();
				var DtContentBody = TemplateConst.SummaryCalculatorMailTemplate();

				decimal profit = container.ResultCalculator.TotalProfitInOHCurency * ((decimal)container.ResultCalculator.PropSharingMarginMitra / 100);
				string formattedIDRProfit = "RP. " + profit.ToString("#,0", System.Globalization.CultureInfo.InvariantCulture);

				TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

				DtContentBody = DtContentBody.Replace("@Owner", textInfo.ToTitleCase(container._station.NamaLengkapOwner?.ToLower() ?? "") + " - " + container._station.NamaBadanUsaha?.ToUpper());
				DtContentBody = DtContentBody.Replace("@SpbuNo", container._calculator.NomorSPBU);
				DtContentBody = DtContentBody.Replace("@PropSharingMarginMitra", container.ResultCalculator.PropSharingMarginMitra + "%");
				DtContentBody = DtContentBody.Replace("@TotalProfitMitra", formattedIDRProfit);
				DtContentBody = DtContentBody.Replace("@StatusKelayakan", container.ResultCalculator.ResumeKelayakan);

				////Build Email Address
				var DtEmail = new Email();
				var LsMailAtt = new List<MailAttach>();
				var LsMailTo = new List<string>();

				if (SystemEnv == 0)
				{
					LsMailTo.Add("basuki.wibowo@indocyber.id");
				}
				else
				{
					LsMailTo.Add(container._station.EmailOwner ?? "");
				}

				DtEmail.to = LsMailTo.Where(w => w != null).ToList();
				DtEmail.subject = DtContentSubject;
				DtEmail.bodyEmailHTML = DtContentBody;

				var RsPost = await _httpClientHandler.PostDataAPIUrl<Object>(ApiUrl, "api/mail/PostMailWithHTML", JsonConvert.SerializeObject(DtEmail));
				return new ApiResponse
				{
					Code = "500",
					Messages = RsPost.Msg,
					Status = false,
				};

			}
			catch (Exception ex)
			{

				return new ApiResponse
				{
					Code = "500",
					Messages = ex.Message,
					Status = false,
				};
			}
		}


	}
}
