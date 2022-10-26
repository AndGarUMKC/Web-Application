using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommerceBankApp.Data;
using CommerceBankApp.Models;
using CommerceBankApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CommerceBankApp.Controllers
{
    public class PaymentInfo2Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentInfo2Controller(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PaymentInfo2
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PaymentInfo2.Include(p => p.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PaymentInfo2/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PaymentInfo2 == null)
            {
                return NotFound();
            }

            var paymentInfo2 = await _context.PaymentInfo2
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.PaymentInfo2Id == id);
            if (paymentInfo2 == null)
            {
                return NotFound();
            }

            return View(paymentInfo2);
        }

        // GET: PaymentInfo2/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PaymentInfo2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentInfo2Id,PaymentInfo2Name,BankAccount,Routing,Address,City,State,ZipCode,ApplicationUserId")] PaymentInfo2 paymentInfo2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentInfo2);
                await _context.SaveChangesAsync();
                return Redirect("~/");
                //return RedirectToAction(nameof(Index));
            }
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", paymentInfo2.ApplicationUserId);
            return View(paymentInfo2);
        }

        // GET: PaymentInfo2/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PaymentInfo2 == null)
            {
                return NotFound();
            }

            var paymentInfo2 = await _context.PaymentInfo2.FindAsync(id);
            if (paymentInfo2 == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", paymentInfo2.ApplicationUserId);
            return View(paymentInfo2);
        }

        // POST: PaymentInfo2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentInfo2Id,PaymentInfo2Name,BankAccount,Routing,Address,City,State,ZipCode,ApplicationUserId")] PaymentInfo2 paymentInfo2)
        {
            if (id != paymentInfo2.PaymentInfo2Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentInfo2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentInfo2Exists(paymentInfo2.PaymentInfo2Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", paymentInfo2.ApplicationUserId);
            return View(paymentInfo2);
        }

        // GET: PaymentInfo2/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PaymentInfo2 == null)
            {
                return NotFound();
            }

            var paymentInfo2 = await _context.PaymentInfo2
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.PaymentInfo2Id == id);
            if (paymentInfo2 == null)
            {
                return NotFound();
            }

            return View(paymentInfo2);
        }

        // POST: PaymentInfo2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PaymentInfo2 == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PaymentInfo2'  is null.");
            }
            var paymentInfo2 = await _context.PaymentInfo2.FindAsync(id);
            if (paymentInfo2 != null)
            {
                _context.PaymentInfo2.Remove(paymentInfo2);
            }
            
            await _context.SaveChangesAsync();
            return Redirect("~/");
            //return RedirectToAction(nameof(Index));
        }

        private bool PaymentInfo2Exists(int id)
        {
          return _context.PaymentInfo2.Any(e => e.PaymentInfo2Id == id);
        }
    }
}
