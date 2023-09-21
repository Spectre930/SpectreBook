using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using System.Linq.Expressions;

namespace SpectreBook.DataAccess.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly AppDBContext _db;

    public ShoppingCartRepository(AppDBContext db) : base(db)
    {
        _db = db;
    }

    public int DecrementCount(ShoppingCart cart, int count)
    {
        cart.Count -= count;
        return cart.Count;
    }

    public int IncrementCount(ShoppingCart cart, int count)
    {
        cart.Count += count;
        return cart.Count;
    }
}
