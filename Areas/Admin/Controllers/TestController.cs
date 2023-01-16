using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Data;
using KS_Payroll.Areas.Admin.ViewModels;

namespace KS_Payroll.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestController : Controller
    {
        private readonly KS_PayrollContext _context;

        public TestController(KS_PayrollContext context)
        {
            _context = context;
        }

        // GET: Admin/Test
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comapny.ToListAsync());
        }

        // GET: Admin/Test/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comapny = await _context.Comapny
                .FirstOrDefaultAsync(m => m.Cid == id);
            if (comapny == null)
            {
                return NotFound();
            }

            return View(comapny);
        }

        // GET: Admin/Test/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpinfoVM comapny)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comapny);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comapny);
        }

        // GET: Admin/Test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comapny = await _context.Comapny.FindAsync(id);
            if (comapny == null)
            {
                return NotFound();
            }
            return View(comapny);
        }

        // POST: Admin/Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cid,CompanyName,CompanyDetails,UserId,ModifiedId,Created_By,Updated_By,Created_At,Updated_At")] Comapny comapny)
        {
            if (id != comapny.Cid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comapny);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComapnyExists(comapny.Cid))
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
            return View(comapny);
        }

        // GET: Admin/Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comapny = await _context.Comapny
                .FirstOrDefaultAsync(m => m.Cid == id);
            if (comapny == null)
            {
                return NotFound();
            }

            return View(comapny);
        }

        // POST: Admin/Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comapny = await _context.Comapny.FindAsync(id);
            _context.Comapny.Remove(comapny);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComapnyExists(int id)
        {
            return _context.Comapny.Any(e => e.Cid == id);
        }
    }
}
