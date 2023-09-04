using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpectreBook.DataAccess.Repository;
using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using SpectreBook.Models.ViewModels;

namespace SpectreBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
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

        return View();
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
            productVM.Product = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            return View(productVM);
        }




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
                obj.Product.ImageUrl = file.FileName;
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);
                if (obj.Product.Id != 0)
                {
                    if (obj.Product.ImageUrl != null)
                    {

                        var oldPath = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == obj.Product.Id).ImageUrl;

                        var oldImage = Path.Combine(wwwRootPath, oldPath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }
                }


                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
            }
            if (obj.Product.Id == 0)
            {
                _UnitOfWork.Product.Add(obj.Product);
                TempData["success"] = "Product Created Successfully";
                _UnitOfWork.Save();

            }
            else
            {
                _UnitOfWork.Product.Update(obj.Product);
                TempData["success"] = "Product Updated Successfully";
                _UnitOfWork.Save();
            }
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    ////Get
    //public IActionResult Delete(int? id)
    //{
    //    if (id == null || id == 0)
    //    {
    //        return NotFound();
    //    }

    //    var ProductFromDb = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

    //    if (ProductFromDb == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(ProductFromDb);
    //}



    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _UnitOfWork.Product.GetAll(includes: "Category,CoverType");
        return Json(new { data = productList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return Json(new { success = false, message = "Error While Deleting" });

        }
        var oldPath = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == obj.Id).ImageUrl;
        var oldImage = Path.Combine(_hostEnvironment.WebRootPath, oldPath.TrimStart('\\'));
        if (System.IO.File.Exists(oldImage))
        {
            System.IO.File.Delete(oldImage);
        }

        _UnitOfWork.Product.Remove(obj);
        _UnitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });


    }
    #endregion


}


