using Microsoft.EntityFrameworkCore;

namespace SignalR_CRUD.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }
        public virtual DbSet<Product> Product { get; set; }
    }
}
