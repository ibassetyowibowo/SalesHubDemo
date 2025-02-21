using KSO_SalesHub.Models.Simulation.Dto;
using KSO_SalesHub.Models.Simulation.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KSO_SalesHub.Models.Simulation
{
	public class SimulationContainer
	{
        public SimulationContainer()
        {

			_station = new M_Station();
			_calculator = new CalculatorDto();
			_productLs = new List<M_Product>();
			_taxStatusListItem = new List<SelectListItem>();
			_SPBUTypeListItem = new List<SelectListItem>();
			_SPBUGradingListItem = new List<SelectListItem>();
			_LokasiSPBUListItem = new List<SelectListItem>();
			_NoSPBUListItem = new List<SelectListItem>();
			ResultCalculator = new SummaryCalculatorDto();
			_V_SharingMarginFixProductList = new List<V_SharingMarginFixProduct>();
		}


		public M_Station _station { get; set; }
		public CalculatorDto _calculator { get; set; } 

        public List<M_Product> _productLs { get; set; }
		public List<SelectListItem> _taxStatusListItem { get; set; } 
		public List<SelectListItem> _SPBUTypeListItem { get; set; } 
		public List<SelectListItem> _SPBUGradingListItem { get; set; }  		 
		public List<SelectListItem> _LokasiSPBUListItem { get; set; }  		 
		public List<SelectListItem> _NoSPBUListItem { get; set; }
		public List<V_SharingMarginFixProduct> _V_SharingMarginFixProductList { get; set; } 

        public SummaryCalculatorDto ResultCalculator { get; set; }  
    }
}
