using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using System.Linq.Expressions;

namespace SpectreBook.DataAccess.Repository;

public class AppUserRepository : Repository<AppUser>, IAppUserRepository
{
   private readonly AppDBContext _db;

    public AppUserRepository(AppDBContext db) : base(db)
    {
        _db = db;
    }

}
