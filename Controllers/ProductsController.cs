using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentitycrudMVC.Data;
using IdentitycrudMVC.Models;

namespace IdentitycrudMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //  return _context.Product != null ? 
            //              View(await _context.Product.ToListAsync()) :
            //              Problem("Entity set 'ApplicationDbContext.Product'  is null.");

            var joinedData = from t1 in _context.categories
                             join t2 in _context.Product on t1.CId equals t2.CId 
                             select new Viewmodel
                             {
                                 CId = t1.CId,
                                 CategoryName = t1.CategoryName,
                                 PId = t2.PId,
                                 ProductName = t2.ProductName
                             } ;

            return View(joinedData.ToList());


       }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.PId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            List<Category> cl = new List<Category>();
            cl = (from c in _context.categories select c).ToList();
            cl.Insert(0, new Category { CId = 0, CategoryName = "--Select Category Name--" });
            ViewBag.message = cl;
            return View();
          
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PId,ProductName,CId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);

         
            if (product == null)
            {
                return NotFound();
            }
            List<Category> cl = new List<Category>();
            cl = (from c in _context.categories select c).ToList();
            cl.Insert(0, new Category { CId = 0, CategoryName = "--Select Country Name--" });
            ViewBag.message = cl;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PId,ProductName,CId")] Product product)
        {
            if (id != product.PId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.PId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.PId == id);

          

            if (product == null)
            {
                return NotFound();
            }
            List<Category> cl = new List<Category>();
            cl = (from c in _context.categories select c).ToList();
            cl.Insert(0, new Category { CId = 0, CategoryName = "--Select Country Name--" });
            ViewBag.message = cl;
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.PId == id)).GetValueOrDefault();
        }
    }
}
