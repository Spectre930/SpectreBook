using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using SpectreBook.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

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

    public IActionResult Details(int productId)
    {

        Product product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includes: "Category,CoverType");

        ShoppingCart cart = new()
        {
            product = product,
            ProductId = productId,
            Count = 1

        };
        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {

        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        shoppingCart.AppUserId = claim.Value;

        ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.AppUserId == claim.Value && u.ProductId == shoppingCart.ProductId);

        if (cartFromDb == null)
        {
            _unitOfWork.ShoppingCart.Add(shoppingCart);

        }
        else
        {
            _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
        }

        _unitOfWork.Save();

        return RedirectToAction("Index");
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