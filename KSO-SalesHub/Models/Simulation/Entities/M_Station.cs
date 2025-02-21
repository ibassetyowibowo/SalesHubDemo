using KSO_SalesHub.Models.Simulation.Base;

namespace KSO_SalesHub.Models.Simulation.Entities
{
	public class M_Station : BaseEntity
	{
		public string? StationNo { get; set; }
		public string? StationName { get; set; }
		public string? StationType { get; set; }
		public string? RegionId { get; set; }
		public string? StationProvince { get; set; }
		public string? StationKabkot { get; set; }
		public string? StationAddress { get; set; }
		public string? Area { get; set; }
		public string? StationEmail { get; set; }
		public string? StationPhoneNo { get; set; }

		public string? NamaBadanUsaha { get; set; }
		public string? TlpOwner { get; set; }
		public string? NamaLengkapOwner { get; set; }
		public string? EmailOwner { get; set; }

		public string? Losses { get; set; }
		public string? Sarfas { get; set; }
		public string? KondisiLokasi { get; set; }
		public string? KomponenProduk { get; set; }
		public string? KendalaSPBU { get; set; }
	}
}
