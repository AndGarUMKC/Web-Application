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
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payment.Include(p => p.ApplicationUser).Include(p => p.Organization).Include(p => p.PaymentInfo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.ApplicationUser)
                .Include(p => p.Organization)
                .Include(p => p.PaymentInfo)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OrganizationId"] = new SelectList(_context.Organization, "OrganizationID", "ImageUrl");
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "Id", "Id");
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,DonatedAmount,ApplicationUserId,OrganizationId,PaymentInfoId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", payment.ApplicationUserId);
            ViewData["OrganizationId"] = new SelectList(_context.Organization, "OrganizationID", "ImageUrl", payment.OrganizationId);
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "Id", "Id", payment.PaymentInfoId);
            return View(payment);
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", payment.ApplicationUserId);
            ViewData["OrganizationId"] = new SelectList(_context.Organization, "OrganizationID", "ImageUrl", payment.OrganizationId);
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "Id", "Id", payment.PaymentInfoId);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,DonatedAmount,ApplicationUserId,OrganizationId,PaymentInfoId")] Payment payment)
        {
            if (id != payment.PaymentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentID))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", payment.ApplicationUserId);
            ViewData["OrganizationId"] = new SelectList(_context.Organization, "OrganizationID", "ImageUrl", payment.OrganizationId);
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "Id", "Id", payment.PaymentInfoId);
            return View(payment);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.ApplicationUser)
                .Include(p => p.Organization)
                .Include(p => p.PaymentInfo)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Payment == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Payment'  is null.");
            }
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
          return _context.Payment.Any(e => e.PaymentID == id);
        }
    }
}
