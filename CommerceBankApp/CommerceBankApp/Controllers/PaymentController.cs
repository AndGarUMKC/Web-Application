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
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["OrganizationID"] = new SelectList(_context.Organization, "OrganizationID", "OrganizationID");
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "PaymentInfoId", "PaymentInfoName");
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,DonatedAmount,DonatedDate,ApplicationUserId,OrganizationID,PaymentInfoId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName", payment.ApplicationUserId);
            ViewData["OrganizationID"] = new SelectList(_context.Organization, "OrganizationID", "OrganizationName", payment.OrganizationID);
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "PaymentInfoId", "PaymentInfoName", payment.PaymentInfoId);

            string errors = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            ModelState.AddModelError("", errors);

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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName", payment.ApplicationUserId);
            ViewData["OrganizationID"] = new SelectList(_context.Organization, "OrganizationID", "OrganizationName", payment.OrganizationID);
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "PaymentInfoId", "PaymentInfoName", payment.PaymentInfoId);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,DonatedAmount,DonatedDate,ApplicationUserId,OrganizationID,PaymentInfoId")] Payment payment)
        {
            if (id != payment.PaymentId)
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
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["OrganizationID"] = new SelectList(_context.Organization, "OrganizationID", "OrganizationID", payment.OrganizationID);
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "PaymentInfoId", "PaymentInfoName", payment.PaymentInfoId);
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
                .FirstOrDefaultAsync(m => m.PaymentId == id);
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
          return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
