using SpectreBook.Models;


namespace SpectreBook.DataAccess.Repository.IRepository;

public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company obj);
}
