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
    public class DonorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Donors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Donor.Include(d => d.Account).Include(d => d.DonorInfo).Include(d => d.Payment);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Donors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Donor == null)
            {
                return NotFound();
            }

            var donor = await _context.Donor
                .Include(d => d.Account)
                .Include(d => d.DonorInfo)
                .Include(d => d.Payment)
                .FirstOrDefaultAsync(m => m.donorID == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        // GET: Donors/Create
        public IActionResult Create()
        {
            ViewData["accountID"] = new SelectList(_context.Account, "accountID", "accountID");
            ViewData["donorInfoID"] = new SelectList(_context.Set<DonorInfo>(), "donorInfoID", "donorInfoID");
            ViewData["paymentID"] = new SelectList(_context.Set<Payment>(), "paymentID", "paymentID");
            return View();
        }

        // POST: Donors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("donorID,billingAddress,homeAddress,accountID,donorInfoID,paymentID")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["accountID"] = new SelectList(_context.Account, "accountID", "accountID", donor.accountID);
            ViewData["donorInfoID"] = new SelectList(_context.Set<DonorInfo>(), "donorInfoID", "donorInfoID", donor.donorInfoID);
            ViewData["paymentID"] = new SelectList(_context.Set<Payment>(), "paymentID", "paymentID", donor.paymentID);
            return View(donor);
        }

        // GET: Donors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Donor == null)
            {
                return NotFound();
            }

            var donor = await _context.Donor.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
            ViewData["accountID"] = new SelectList(_context.Account, "accountID", "accountID", donor.accountID);
            ViewData["donorInfoID"] = new SelectList(_context.Set<DonorInfo>(), "donorInfoID", "donorInfoID", donor.donorInfoID);
            ViewData["paymentID"] = new SelectList(_context.Set<Payment>(), "paymentID", "paymentID", donor.paymentID);
            return View(donor);
        }

        // POST: Donors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("donorID,billingAddress,homeAddress,accountID,donorInfoID,paymentID")] Donor donor)
        {
            if (id != donor.donorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonorExists(donor.donorID))
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
            ViewData["accountID"] = new SelectList(_context.Account, "accountID", "accountID", donor.accountID);
            ViewData["donorInfoID"] = new SelectList(_context.Set<DonorInfo>(), "donorInfoID", "donorInfoID", donor.donorInfoID);
            ViewData["paymentID"] = new SelectList(_context.Set<Payment>(), "paymentID", "paymentID", donor.paymentID);
            return View(donor);
        }

        // GET: Donors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Donor == null)
            {
                return NotFound();
            }

            var donor = await _context.Donor
                .Include(d => d.Account)
                .Include(d => d.DonorInfo)
                .Include(d => d.Payment)
                .FirstOrDefaultAsync(m => m.donorID == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        // POST: Donors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Donor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Donor'  is null.");
            }
            var donor = await _context.Donor.FindAsync(id);
            if (donor != null)
            {
                _context.Donor.Remove(donor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonorExists(int id)
        {
          return _context.Donor.Any(e => e.donorID == id);
        }
    }
}
