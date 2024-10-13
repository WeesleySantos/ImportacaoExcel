using Asteria_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Asteria_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<DataModel> Planilha { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
