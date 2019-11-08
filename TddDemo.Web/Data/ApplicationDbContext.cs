using Microsoft.EntityFrameworkCore;

namespace TddDemo.Web.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetDetail> BudgetDetails { get; set; }

        public DbSet<Forecast> Forecasts { get; set; }
        public DbSet<ForecastDetail> ForecastDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forecast>()
                .Property(f => f.Date)
                .HasColumnType("date");

            modelBuilder.Entity<Budget>()
                .Property(b => b.Name)
                .HasMaxLength(300);

            base.OnModelCreating(modelBuilder);
        }
    }
}
