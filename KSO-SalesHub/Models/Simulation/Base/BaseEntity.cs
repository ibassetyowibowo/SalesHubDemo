using System.ComponentModel.DataAnnotations.Schema;

namespace KSO_SalesHub.Models.Simulation.Base
{
	public class BaseEntity
	{
		[Column(Order = 0)]
		public int Id { get; set; }
		public string? Status { get; set; }
		public string? Notes { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; }
		public string? ModifiedBy { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public bool IsDeleted { get; set; }
		public string? DeletedBy { get; set; }
		public DateTime? DeletedAt { get; set; }
	}
}
