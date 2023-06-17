using Microsoft.AspNetCore.Mvc;
using SpectreBookWeb.Data;
using SpectreBookWeb.Models;
namespace SpectreBookWeb.Controllers
{
   
    public class CategoryController : Controller
    {
        private readonly AppDBContext _db;

        public CategoryController(AppDBContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList= _db.Categories;
            return View(objCategoryList);
        }

        //Get
        public IActionResult Create()
        {

            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
