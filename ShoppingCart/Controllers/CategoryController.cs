
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoryController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return _context.Category != null ?
                        View(await _context.Category.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Category'  is null.");
        }

        //May use the below for future aplications 
        //// GET: Category/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Category == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.Category
        //        .FirstOrDefaultAsync(m => m.CategoryId == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                var categoryFound = _context.Category.Where(e => e.CategoryName == category.CategoryName).Count();
                if (categoryFound <= 0)
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //May use the below for future aplications 
        //// GET: Category/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Category == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.Category.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}

        //// POST: Category/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        //{
        //    if (id != category.CategoryId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(category);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CategoryExists(category.CategoryId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(category);
        //}

        //// GET: Category/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Category == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.Category
        //        .FirstOrDefaultAsync(m => m.CategoryId == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        //// POST: Category/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Category == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Category'  is null.");
        //    }
        //    var category = await _context.Category.FindAsync(id);
        //    if (category != null)
        //    {
        //        _context.Category.Remove(category);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CategoryExists(int id)
        //{
        //  return (_context.Category?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        //}
    }
}
