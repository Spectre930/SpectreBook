using Microsoft.EntityFrameworkCore;
using SpectreBookWeb.Models;

namespace SpectreBookWeb.Data
{
    public class AppDBContext :DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {  
        }

        public DbSet<Category> Categories { get; set; }
    }
}
