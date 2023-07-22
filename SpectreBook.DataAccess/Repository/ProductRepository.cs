using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectreBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        readonly AppDBContext _db;
        public ProductRepository(AppDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);

            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Author = obj.Author;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price = obj.Price;

            }
            _db.Products.Update(objFromDb);
        }
    }
}
