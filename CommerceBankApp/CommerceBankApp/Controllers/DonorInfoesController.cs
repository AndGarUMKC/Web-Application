using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommerceBankApp.Data;
using CommerceBankApp.Models;

namespace CommerceBankApp.Controllers
{
    public class DonorInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonorInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DonorInfoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DonorInfo.Include(d => d.Donor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DonorInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DonorInfo == null)
            {
                return NotFound();
            }

            var donorInfo = await _context.DonorInfo
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(m => m.donorInfoID == id);
            if (donorInfo == null)
            {
                return NotFound();
            }

            return View(donorInfo);
        }

        // GET: DonorInfoes/Create
        public IActionResult Create()
        {
            ViewData["donorID"] = new SelectList(_context.Donor, "donorID", "donorID");
            return View();
        }

        // POST: DonorInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("donorInfoID,cardNumber,cardExpiration,cvcNumber,bankRoutingNumber,bankAccountNumber,donorID")] DonorInfo donorInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donorInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["donorID"] = new SelectList(_context.Donor, "donorID", "donorID", donorInfo.donorID);
            return View(donorInfo);
        }

        // GET: DonorInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DonorInfo == null)
            {
                return NotFound();
            }

            var donorInfo = await _context.DonorInfo.FindAsync(id);
            if (donorInfo == null)
            {
                return NotFound();
            }
            ViewData["donorID"] = new SelectList(_context.Donor, "donorID", "donorID", donorInfo.donorID);
            return View(donorInfo);
        }

        // POST: DonorInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("donorInfoID,cardNumber,cardExpiration,cvcNumber,bankRoutingNumber,bankAccountNumber,donorID")] DonorInfo donorInfo)
        {
            if (id != donorInfo.donorInfoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donorInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonorInfoExists(donorInfo.donorInfoID))
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
            ViewData["donorID"] = new SelectList(_context.Donor, "donorID", "donorID", donorInfo.donorID);
            return View(donorInfo);
        }

        // GET: DonorInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DonorInfo == null)
            {
                return NotFound();
            }

            var donorInfo = await _context.DonorInfo
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(m => m.donorInfoID == id);
            if (donorInfo == null)
            {
                return NotFound();
            }

            return View(donorInfo);
        }

        // POST: DonorInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DonorInfo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DonorInfo'  is null.");
            }
            var donorInfo = await _context.DonorInfo.FindAsync(id);
            if (donorInfo != null)
            {
                _context.DonorInfo.Remove(donorInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonorInfoExists(int id)
        {
          return _context.DonorInfo.Any(e => e.donorInfoID == id);
        }
    }
}
