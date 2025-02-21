using KSO_SalesHub.Models.Simulation.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ardalis.EFCore.Extensions;
using KSO_SalesHub.Models.Simulation.Entities;

namespace KSO_SalesHub.Models.Simulation
{
	public class SimulationDbContext : DbContext
	{
		IHttpContextAccessor _contextAccessor;
		public SimulationDbContext(DbContextOptions<SimulationDbContext> options, IHttpContextAccessor contextAccessor) : base(options)
		{
			_contextAccessor = contextAccessor;
		}


		public DbSet<M_Product> M_Product { get; set; } 
		public DbSet<M_Station> M_Station { get; set; }
		public DbSet<M_StationType> M_StationType { get; set; } 
		public DbSet<M_KabupatenKota> M_KabupatenKota { get; set; }
		public DbSet<M_ParameterLevel> M_ParameterLevel { get; set; }
		public DbSet<M_StationGrading> M_StationGrading { get; set; }
		public DbSet<M_MarginFixProduct> M_MarginFixProduct { get; set; }
		public DbSet<M_MarginFixProductMatrix> M_MarginFixProductMatrix { get; set; }
		public DbSet<M_MarginSimulationProduct> M_MarginSimulationProduct { get; set; } 

		public DbSet<V_SharingMarginFixProduct> V_SharingMarginFixProduct { get; set; }

		public DbSet<T_SummaryCalculator> T_SummaryCalculator { get; set; }
		public DbSet<T_SummaryCalculatorDetail> T_SummaryCalculatorDetail { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<V_SharingMarginFixProduct>().HasNoKey();
			modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

			var propertyMethodInfo = typeof(EF).GetMethod("Property", new[] { typeof(object), typeof(string) });

			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
				{
					var parameter = Expression.Parameter(entityType.ClrType);

					// EF.Property<bool>(entity, "IsDeleted")
					var isDeletedProperty = Expression.Call(propertyMethodInfo.MakeGenericMethod(typeof(bool)), parameter, Expression.Constant("IsDeleted"));

					// EF.Property<bool>(entity, "IsDeleted") == false
					var compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

					// entity => EF.Property<bool>(entity, "IsDeleted") == false
					var lambda = Expression.Lambda(compareExpression, parameter);

					modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
				}
			}
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			HandleSoftDelete();

			int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

			var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
				.Select(x => x.Entity)
				.ToArray();

			return result;
		}

		public override int SaveChanges()
		{
			HandleSoftDelete();

			int result = base.SaveChanges();

			var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
				.Select(e => e.Entity)
				.ToArray();

			return result;
		}

		private void HandleSoftDelete()
		{
			var currentUsername = "System";

			if (_contextAccessor != null && _contextAccessor.HttpContext != null && _contextAccessor.HttpContext.User.Claims.FirstOrDefault() != null)
			{
				currentUsername = !string.IsNullOrEmpty(_contextAccessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == "UserId").Value) ?
					_contextAccessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == "UserId").Value : "N/A";
			}

			DateTime dtNow = DateTime.Now;
			foreach (var entry in ChangeTracker.Entries())
			{
				if (entry.Entity is BaseEntity baseEntity)
				{
					switch (entry.State)
					{
						case EntityState.Added:
							baseEntity.IsDeleted = false;
							baseEntity.CreatedAt = dtNow;
							baseEntity.CreatedBy = currentUsername;
							baseEntity.ModifiedAt = dtNow;
							baseEntity.ModifiedBy = currentUsername;
							break;
						case EntityState.Modified:
							baseEntity.IsDeleted = baseEntity.IsDeleted;
							baseEntity.ModifiedAt = dtNow;
							baseEntity.ModifiedBy = currentUsername;
							break;
						case EntityState.Deleted:
							entry.State = EntityState.Modified;
							baseEntity.IsDeleted = true;
							baseEntity.DeletedAt = dtNow;
							baseEntity.DeletedBy = currentUsername;
							break;
					}
				}
			}
		}



	}
}
