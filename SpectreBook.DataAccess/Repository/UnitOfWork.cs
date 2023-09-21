using SpectreBook.DataAccess.Repository.IRepository;


namespace SpectreBook.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDBContext _db;
    public ICategoryRepository Category { get; private set; }
    public ICoverTypeRepository CoverType { get; private set; }
    public IProductRepository Product { get; private set; }
    public ICompanyRepository Company { get; private set; }
    public IShoppingCartRepository ShoppingCart { get; private set; }
    public IAppUserRepository AppUser { get; private set; }

    public UnitOfWork(AppDBContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        CoverType = new CoverTypeRepository(_db);
        Product = new ProductRepository(_db);
        Company = new CompanyRepository(_db);
        ShoppingCart = new ShoppingCartRepository(_db);
        AppUser = new AppUserRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
