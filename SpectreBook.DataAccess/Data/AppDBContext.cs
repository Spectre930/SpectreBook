using SpectreBook.Models;
using Microsoft.EntityFrameworkCore;

namespace SpectreBook.DataAccess;

public class AppDBContext :DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {  
    }

    public DbSet<Category> Categories { get; set; }
}
