using DC.DataAccess.Repository;
using DC.DataAccess.Repository.IRepository;
using DC.Models;
using DC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DC.Areas.Customer.Controllers;
[Area("Customer")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitofWork _unitofWork;

    public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
    {
        _logger = logger;
        _unitofWork = unitofWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productList = _unitofWork.Product.GetAll(includeProperties: "Category,CoverType");

        return View(productList);
    }
    public IActionResult Details(int id)
    {
        ShoppingCart cartObj = new()
        {
            Count = 1,
            Product = _unitofWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType"),
        };
        return View(cartObj);
    }
    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}