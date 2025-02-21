using KSO_SalesHub.Models.Simulation.Dto;
using KSO_SalesHub.Models.Simulation;
using KSO_SalesHub.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KSO_SalesHub.Controllers
{
	public class PublicWebController : Controller
	{
		private SimulationServices _service;
		private readonly ILogger<HomeController> _logger;

		public PublicWebController(
			SimulationServices service,
			ILogger<HomeController> logger
			)
		{
			_service = service;
			_logger = logger;
		}


		[Route("")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("about")]
		public IActionResult About()
		{
			return View();
		}

		[Route("signup")]
		public async Task<IActionResult> Signup()
		{
			var dto = (await _service.InitRegisterAsync()).Data;
			return View(dto);
		}

		[HttpPost]
		[Route("public/submit-register")]
		public async Task<IActionResult> SubmitRegister(StationDto data)
		{
			var dtStation = (await _service.RegisterNewSpbuAsync(data)).Data;

			return RedirectToAction("Calculator", dtStation);
		}

		[Route("calculator-mitra")]
		public async Task<IActionResult> Calculator(StationDto data)
		{
			var _ssn_calculator = new CalculatorDto();
			var serialized_ssn_calculator = HttpContext.Session.GetString("_calculator");
			if (!string.IsNullOrEmpty(serialized_ssn_calculator))
			{
				_ssn_calculator = JsonConvert.DeserializeObject<CalculatorDto>(serialized_ssn_calculator);
			}

			string stationId = Request.Cookies["stationId"];

			data.StationNo = !string.IsNullOrEmpty(data.StationNo) ? data.StationNo : stationId;
			var container = new SimulationContainer();
			container._station = data;

			container._calculator = _ssn_calculator ?? new CalculatorDto();
			container._calculator.NomorSPBU = data?.StationNo ?? "";
			container._calculator.LokasiSPBU = data?.txt_alamat ?? "";
			container._calculator.UMK = data?.txt_umk ?? "";

			container = (await _service.InitAsync(container, "MITRA")).Data;
			Response.Cookies.Append("stationId", data?.StationNo ?? "");

			return View(container);
		}

		[HttpPost]
		[Route("public/summary")]
		public async Task<IActionResult> CalculatorSummary(SimulationContainer container)
		{
			container = (await _service.GetResultSimulationAsync(container, "MITRA")).Data;

			var _calculator = JsonConvert.SerializeObject(container._calculator);
			HttpContext.Session.SetString("_calculator", _calculator);
			return View(container);
		}

		public IActionResult Layanan()
		{
			return View();
		}

	}
}
