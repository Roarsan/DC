using DC.DataAccess;
using DC.DataAccess.Repository;
using DC.DataAccess.Repository.IRepository;
using DC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DC.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController : Controller
{
    private readonly IUnitofWork _unitOfWork;

    public ProductController(IUnitofWork unitofWork)
    {
        _unitOfWork = unitofWork;
    }

    // GET: Product
    public IActionResult Index()
    {
        IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();
        return View(objProductList);
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
        Product product = new();
            
            if (id == null || id == 0)
        {
            //create product
            return View(product);
        }
        else
        {
            //update product
        
        }

       
        return View(product);
    }

    // POST: product/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //Post for edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
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


}
