using KSO_SalesHub.Models.Simulation.Entities;

namespace KSO_SalesHub.Models.Simulation.Dto
{
	public class ProductDto : M_Product
	{
        public int SalesPerMonth { get; set; } = 0; 
    }  
}
