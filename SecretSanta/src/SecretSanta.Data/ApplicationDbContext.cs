using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SecretSanta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Gift>? Gifts { get; set; }
        public DbSet<User>? Users { get; set; }
        DbSet<UserGroup>? UserGroup { get; set; }
        public IHttpContextAccessor? HttpContextAccessor { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public override int SaveChanges()
        {
            AddFingerPrinting();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            AddFingerPrinting();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddFingerPrinting()
        {
            //TODO
        }
    }
}
