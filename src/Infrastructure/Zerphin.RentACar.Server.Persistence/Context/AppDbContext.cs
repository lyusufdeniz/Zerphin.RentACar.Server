using Microsoft.EntityFrameworkCore;
using Zerphin.RentACar.Server.Persistence.Interceptors;

namespace Zerphin.RentACar.Server.Persistence.Context
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
            => SaveChangesInterceptor.SaveChangesAsync(this, cancellationToken);
        public override int SaveChanges()=> SaveChangesInterceptor.SaveChanges(this);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
        
