using KSO_SalesHub.Models;
using KSO_SalesHub.Models.Simulation;
using KSO_SalesHub.Models.Simulation.Dto;
using KSO_SalesHub.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;
using System.Security.Claims;
using System.Diagnostics.CodeAnalysis;
using KSO_SalesHub.Models.Simulation.Entities;
using Rotativa.AspNetCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Newtonsoft.Json.Linq;

namespace KSO_SalesHub.Controllers
{
	public class HomeController : Controller
	{
		private SimulationServices _service;
		private readonly ILogger<HomeController> _logger;
		public HomeController(
			SimulationServices service,
			ILogger<HomeController> logger
			)
		{
			_service = service;
			_logger = logger;
		}


		[Route("login")]
		public IActionResult Login()
		{
			return View();
		}


		[Route("admin-panel")]
		public IActionResult DataSimulation()
		{
			var userId = GetUserId();
			var userIdentity = HttpContext.User.Identity;
			if (!userIdentity.IsAuthenticated)
			{
				return RedirectToAction("Login");
			}
			return View();
		}


		[Route("submit-login")]
		public async Task<IActionResult> submitLogin(UserLogin data)
		{
			var username = new[] { "ptpr.admin1", "ptpr.admin2" };
			var password = "user@default";

			if (username.Contains(data.Username) && data.Password == password)
			{
				var principal = new ClaimsPrincipal();
				principal = await Authenticate(data.Username);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
				{
					IsPersistent = false,
					ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
				});

				return RedirectToAction("DataSimulation");
			}
			else
			{
				var dtStation = await _service.GetDataSpbuAsync(data.Username ?? "");
				if(dtStation.Data != null)
				{ 
					return RedirectToAction("Calculator", "PublicWeb", dtStation.Data);
				}

				return RedirectToAction("Login");
			}
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Login", "Home");
		}


		[Route("calculator")]
		public async Task<IActionResult> SpbuSalesSimulation(StationDto data) 
		{
			string stationId = Request.Cookies["stationId"];

			data.StationNo = !string.IsNullOrEmpty(data.StationNo) ? data.StationNo : stationId;

			var container = new SimulationContainer();
			container._station = data;
			container._calculator.NomorSPBU = data?.StationNo ?? "";
			container._calculator.LokasiSPBU = data?.txt_alamat ?? "";
			container._calculator.UMK = data?.txt_umk ?? "";
			container = (await _service.InitAsync(container, "MITRA")).Data;

			Response.Cookies.Append("stationId", data?.StationNo ?? "");

			return View(container);
		}


		[Route("calculator-internal")]
		public async Task<IActionResult> SpbuSalesSimulationInternal()
		{
			var userId = GetUserId();
			var userIdentity = HttpContext.User.Identity;
			if (!userIdentity.IsAuthenticated)
			{
				return RedirectToAction("Login");
			}

			var container = new SimulationContainer();
			container = (await _service.InitAsync(container, "INTERNAL")).Data;

			return View(container);
		}


		[HttpGet]
		[Route("summary-calculator")]
		public async Task<IActionResult> CalculatorSummaryExisting(string SummaryId)
		{
			var container = (await _service.GetSummaryByIdAsync(SummaryId)).Data;
			return View(container);
		}

		[HttpGet]
		[Route("download-summary")]
		public async Task<IActionResult> DownloadCalculatorSummary(string SummaryID)
		{
			var container = (await _service.GetSummaryByIdAsync(SummaryID)).Data;

			return new ViewAsPdf("DownloadCalculatorSummary", container)
			{
				FileName = "Result Summary Mitra SPBU " + container._calculator.NomorSPBU + "_" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf",
				PageSize = Rotativa.AspNetCore.Options.Size.A4,
			};

			//return View(container);
		}

        [HttpGet]
        [Route("download-summary-internal")]
        public async Task<IActionResult> DownloadCalculatorSummaryInternal(string SummaryID)
        {
            var container = (await _service.GetSummaryByIdAsync(SummaryID)).Data;

			return new ViewAsPdf("DownloadCalculatorSummaryInternal", container)
			{
				FileName = "Result Summary Internal Mitra SPBU " + container._calculator.NomorSPBU + "_" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf",
				PageSize = Rotativa.AspNetCore.Options.Size.Legal,
				PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
			};

			//return View(container);
		}

        [HttpPost]
		[Route("summary")]
		public async Task<IActionResult> CalculatorSummary(SimulationContainer container)
		{
			container = (await _service.GetResultSimulationAsync(container, "MITRA")).Data;

			return View(container);
		}


		[HttpPost]
		[Route("summary-internal")]
		public async Task<IActionResult> CalculatorSummaryInternal(SimulationContainer container)
		{
			var userId = GetUserId();
			var userIdentity = HttpContext.User.Identity;
			if (!userIdentity.IsAuthenticated)
			{
				return RedirectToAction("Login");
			}

			container = (await _service.GetResultSimulationAsync(container, "INTERNAL")).Data;

			return View(container);
		}

        [Route("register")]
        public async Task<IActionResult> Register()
		{
			var dto = (await _service.InitRegisterAsync()).Data;
			return View(dto);
		}


		[HttpPost]
		[Route("submit-register")]
		public async Task<IActionResult> SubmitRegister(StationDto data)
		{
			var dtStation = (await _service.RegisterNewSpbuAsync(data)).Data;

			return RedirectToAction("SpbuSalesSimulation", dtStation);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		[ExcludeFromCodeCoverage]
		[HttpGet("GetUserId")]
		public string GetUserId()
		{
			try
			{
				string Rs = HttpContext.User.FindFirst(x => x.Type == ("UserId")).Value;

				return Rs;
			}
			catch (Exception)
			{
				return "N/A";
			}
		}


		public async Task<ClaimsPrincipal> Authenticate(string username)
		{
			try
			{
				var claims = new List<Claim>
						{
							new Claim("UserId", username),

						};
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				return principal;
			}
			catch (Exception ex)
			{
				throw new Exception("error : " + ex.ToString());
			}
		}
	}
}
