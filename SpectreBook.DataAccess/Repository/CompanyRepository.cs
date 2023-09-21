using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using System.Linq.Expressions;

namespace SpectreBook.DataAccess.Repository;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
   private readonly AppDBContext _db;

    public CompanyRepository(AppDBContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Company obj)
    {
        _db.Companies.Update(obj);
    }
}
