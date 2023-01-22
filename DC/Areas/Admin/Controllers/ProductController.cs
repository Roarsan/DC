using DC.DataAccess;
using DC.DataAccess.Repository;
using DC.DataAccess.Repository.IRepository;
using DC.Models;
using DC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DC.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController : Controller
{
    private readonly IUnitofWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductController(IUnitofWork unitofWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitofWork;
        _hostEnvironment = hostEnvironment;
    }

    // GET: Product
    public IActionResult Index()
    {

        return View();
    }

    // GET: Product/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || id == null)
        {
            return NotFound();
        }

        var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }


    // GET: Product/Upsert/5
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };

        if (id == null || id == 0)
        {
            //create product
            return View(productVM);
        }
        else
        {
            //update product

        }


        return View(productVM);
    }

    // POST: product/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //Post for edit
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
                obj.Product.ImageURL = @"\images\products\" + fileName + extension;

            }
            _unitOfWork.Product.Add(obj.Product);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }



    // GET: product/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
        }
        var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
        if (product != null)
        {
            _unitOfWork.Product.Remove(product);
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _unitOfWork.Product.GetAll(includePropeties:"Category,CoverType");
        return Json(new { data = productList });
    }

    #endregion

}