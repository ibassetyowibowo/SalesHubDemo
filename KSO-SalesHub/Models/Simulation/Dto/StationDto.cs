using KSO_SalesHub.Models.Simulation.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KSO_SalesHub.Models.Simulation.Dto
{
	public class StationDto : M_Station
	{
        public StationDto()
        {
			_lossesListItem = new List<SelectListItem>();
			_sarfasListItem = new List<SelectListItem>();
			_komponenProdukListItem = new List<SelectListItem>();
			_kondisiLokasiListItem = new List<SelectListItem>();
			_kendalaSPBUListItem = new List<SelectListItem>(); 

			_LokasiSPBUListItem = new List<SelectListItem>();
			_SPBUTypeListItem = new List<SelectListItem>();
			_SPBUGradingListItem = new List<SelectListItem>();
		}


		public string txt_alamat { get; set; }  
		public string txt_umk { get; set; }   

		public List<SelectListItem> _lossesListItem { get; set; }
		public List<SelectListItem> _sarfasListItem { get; set; }
		public List<SelectListItem> _komponenProdukListItem { get; set; } 
		public List<SelectListItem> _kondisiLokasiListItem { get; set; } 
		public List<SelectListItem> _kendalaSPBUListItem { get; set; } 

		public List<SelectListItem> _SPBUTypeListItem { get; set; }
		public List<SelectListItem> _SPBUGradingListItem { get; set; }
		public List<SelectListItem> _LokasiSPBUListItem { get; set; }
	}
}
