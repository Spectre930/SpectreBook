using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpectreBook.DataAccess.Repository;
using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using SpectreBook.Models.ViewModels;

namespace SpectreBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _UnitOfWork;


    public CompanyController(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {

        return View();
    }

    //Get
    public IActionResult Upsert(int? id)
    {
        Company company = new();



        if (id == null || id == 0)
        {
            //create product
            return View(company);
        }
        else
        {
            company = _UnitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
            return View(company);
        }




    }

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company obj)
    {
        if (ModelState.IsValid)
        {
            if (obj.Id == 0)
            {
                _UnitOfWork.Company.Add(obj);
                TempData["success"] = "Company Created Successfully";
                _UnitOfWork.Save();

            }
            else
            {
                _UnitOfWork.Company.Update(obj);
                TempData["success"] = "Company Updated Successfully";
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
        var companies = _UnitOfWork.Company.GetAll();
        return Json(new { data = companies });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _UnitOfWork.Company.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return Json(new { success = false, message = "Error While Deleting" });

        }

        _UnitOfWork.Company.Remove(obj);
        _UnitOfWork.Save();
        return Json(new { success = true, message = "Delete Successfull" });


    }
    #endregion


}


