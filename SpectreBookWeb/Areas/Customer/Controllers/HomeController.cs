using Microsoft.AspNetCore.Mvc;
using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using SpectreBook.Models.ViewModels;
using System.Diagnostics;

namespace SpectreBookWeb.Areas.Customer.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {

        IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includes: "Category,CoverType");
        return View(productList);
    }

    public IActionResult Details(int id)
    {

        Product product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includes: "Category,CoverType");

        ShoppingCart cart = new()
        {
            product = product,
            Count = 1

        };
        return View(cart);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}