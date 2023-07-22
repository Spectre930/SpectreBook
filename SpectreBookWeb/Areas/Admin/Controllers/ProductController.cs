using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpectreBook.DataAccess.Repository;
using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using SpectreBook.Models.ViewModels;

namespace SpectreBookWeb.Areas.Admin.Controllers;


public class ProductController : Controller
{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        _UnitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }
    public IActionResult Index()
    {
        IEnumerable<Product> ProductList = _UnitOfWork.Product.GetAll();
        return View(ProductList);
    }

    //Get
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _UnitOfWork.Category.GetAll().Select(u => new SelectListItem
            {

                Text = u.Name,
                Value = u.Id.ToString()
            }),
            CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(u => new SelectListItem
            {

                Text = u.Name,
                Value = u.Id.ToString()
            }),

        };


        if (id == null || id == 0)
        {
            //create product
            return View(productVM);
        }
        else
        {

        }



        return View(productVM);
    }

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile? file)
    {

        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
            }
            _UnitOfWork.Product.Add(obj.Product);
            _UnitOfWork.Save();
            TempData["success"] = "Product Created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //Get
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var ProductFromDb = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

        if (ProductFromDb == null)
        {
            return NotFound();
        }

        return View(ProductFromDb);
    }

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return NotFound();
        }

        _UnitOfWork.Product.Remove(obj);
        _UnitOfWork.Save();
        TempData["success"] = "Product Deleted Successfully";
        return RedirectToAction("Index");

    }


    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _UnitOfWork.Product.GetAll();
        return Json(new { data = productList });
    }
    #endregion


}


