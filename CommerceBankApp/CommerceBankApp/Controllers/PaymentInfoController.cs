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
    public class PaymentInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentInfo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PaymentInfo.Include(p => p.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PaymentInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PaymentInfo == null)
            {
                return NotFound();
            }

            var paymentInfo = await _context.PaymentInfo
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentInfo == null)
            {
                return NotFound();
            }

            return View(paymentInfo);
        }

        // GET: PaymentInfo/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PaymentInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,cardNumber,cvcNumber,cardExpiration,ApplicationUserId")] PaymentInfo paymentInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", paymentInfo.ApplicationUserId);
            return View(paymentInfo);
        }

        // GET: PaymentInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PaymentInfo == null)
            {
                return NotFound();
            }

            var paymentInfo = await _context.PaymentInfo.FindAsync(id);
            if (paymentInfo == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", paymentInfo.ApplicationUserId);
            return View(paymentInfo);
        }

        // POST: PaymentInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,cardNumber,cvcNumber,cardExpiration,ApplicationUserId")] PaymentInfo paymentInfo)
        {
            if (id != paymentInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentInfoExists(paymentInfo.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", paymentInfo.ApplicationUserId);
            return View(paymentInfo);
        }

        // GET: PaymentInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PaymentInfo == null)
            {
                return NotFound();
            }

            var paymentInfo = await _context.PaymentInfo
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentInfo == null)
            {
                return NotFound();
            }

            return View(paymentInfo);
        }

        // POST: PaymentInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PaymentInfo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PaymentInfo'  is null.");
            }
            var paymentInfo = await _context.PaymentInfo.FindAsync(id);
            if (paymentInfo != null)
            {
                _context.PaymentInfo.Remove(paymentInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentInfoExists(int id)
        {
          return _context.PaymentInfo.Any(e => e.Id == id);
        }
    }
}
