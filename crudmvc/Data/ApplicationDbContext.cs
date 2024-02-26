using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using crudmvc.Models;

namespace crudmvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<crudmvc.Models.Produk> Produk { get; set; } = default!;
        public DbSet<crudmvc.Models.Kategori> Kategori { get; set; } = default!;
    }
}
