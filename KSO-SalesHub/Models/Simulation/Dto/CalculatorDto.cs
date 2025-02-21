using KSO_SalesHub.Models.Simulation.Entities;

namespace KSO_SalesHub.Models.Simulation.Dto
{
	public class CalculatorDto
	{
        public CalculatorDto()
        {
            _productDtoLs = new List<ProductDto>();
		}


        public bool isAddingUMP { get; set; } = false;

		public string NomorSPBU { get; set; }
		public string LokasiSPBU { get; set; }
		public string TaxStatus { get; set; }
        public string SPBUName { get; set; }
        public string SPBUType { get; set; }
        public string SPBUGrading { get; set; }
        public string UMK { get; set; } 

		public List<ProductDto> _productDtoLs { get; set; } 



	}
}
