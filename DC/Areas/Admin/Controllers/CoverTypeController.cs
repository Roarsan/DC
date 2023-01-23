using DC.DataAccess;
using DC.DataAccess.Repository;
using DC.DataAccess.Repository.IRepository;
using DC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DC.Areas.Admin.Controllers;
[Area("Admin")]

public class CoverTypeController : Controller
{
    private readonly IUnitofWork _unitOfWork;

    public CoverTypeController(IUnitofWork unitofWork)
    {
        _unitOfWork = unitofWork;
    }

    // GET: CoverType
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }



    // GET: CoverType/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CoverType/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,DisplayOrder,CreatedDate")] CoverType coverType)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(coverType);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        return View(coverType);
    }

    // GET: CoverType/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var coverType = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        if (coverType == null)
        {
            return NotFound();
        }
        return View(coverType);
    }

    // POST: CoverType/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //Post for edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(obj);
    }



    // GET: CoverType/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var category = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: CoverType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return Problem("Entity set 'ApplicationDbContext.CoverType'  is null.");
        }
        var category = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        if (category != null)
        {
            _unitOfWork.CoverType.Remove(category);
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }


}
