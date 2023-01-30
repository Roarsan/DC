﻿using DC.DataAccess.Repository;
using DC.DataAccess.Repository.IRepository;
using DC.Models;
using DC.Models.ViewModels;
using DC.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
    public IActionResult Details(int productid)
    {
        ShoppingCart cartObj = new()
        {
            Count = 1,
            Product = _unitofWork.Product.GetFirstOrDefault(u => u.Id == productid, includeProperties: "Category,CoverType"),
        };
        return View(cartObj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        shoppingCart.ApplicationUserId = claim.Value;

        ShoppingCart cartFromDb = _unitofWork.ShoppingCart.GetFirstOrDefault(
            u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);


        if (cartFromDb == null)
        {

            _unitofWork.ShoppingCart.Add(shoppingCart);

        }
        else
        {
            _unitofWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
  
        }

        _unitofWork.Save();
        return RedirectToAction(nameof(Index));
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