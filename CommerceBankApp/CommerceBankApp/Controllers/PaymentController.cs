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
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payment
                .Include(p => p.ApplicationUser)
                .Include(p => p.Organization)
                .Include(p => p.PaymentInfo)
                .Include(p => p.PaymentInfo2);
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
                .Include(p => p.PaymentInfo2)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.UserName = _userManager.GetUserName(HttpContext.User);
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            TempData.Keep("OrganizationName");
            TempData.Keep("OrganizationImage");
            ViewData["OrganizationID"] = new SelectList(_context.Organization, "OrganizationID", "ImageUrl");
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo
                                                        .Where(p => p.ApplicationUserId
                                                        .Equals(_userManager.GetUserId(HttpContext.User))),
                                                        "PaymentInfoId", "PaymentInfoName");
            ViewData["PaymentInfo2Id"] = new SelectList(_context.PaymentInfo2
                                                        .Where(p => p.ApplicationUserId
                                                        .Equals(_userManager.GetUserId(HttpContext.User))),
                                                        "PaymentInfo2Id", "PaymentInfo2Name");
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,DonatedAmount,DonatedDate,UserName,OrganizationID,PaymentInfoId,PaymentInfo2Id")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return Redirect("~/Organization/Details/" + payment.OrganizationID);
            }
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserName = _userManager.GetUserName(HttpContext.User);
            ViewData["OrganizationID"] = new SelectList(_context.Organization, "OrganizationID", "ImageUrl", payment.OrganizationID);
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo
                                                        .Where(p => p.ApplicationUserId
                                                        .Equals(_userManager.GetUserId(HttpContext.User))),
                                                        "PaymentInfoId", "PaymentInfoName", payment.PaymentInfoId);
            ViewData["PaymentInfo2Id"] = new SelectList(_context.PaymentInfo2
                                                        .Where(p => p.ApplicationUserId
                                                        .Equals(_userManager.GetUserId(HttpContext.User))),
                                                        "PaymentInfo2Id", "PaymentInfo2Name", payment.PaymentInfo2Id);
            string errors = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            ModelState.AddModelError("", errors);
            return View(payment);
        }

        // GET: Payment/Edit/5
        [Authorize]
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
            ViewData["OrganizationID"] = new SelectList(_context.Organization, "OrganizationID", "ImageUrl", payment.OrganizationID);
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "PaymentInfoId", "PaymentInfoName", payment.PaymentInfoId);
            ViewData["PaymentInfo2Id"] = new SelectList(_context.PaymentInfo2, "PaymentInfo2Id", "PaymentInfo2Name", payment.PaymentInfo2Id);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,DonatedAmount,DonatedDate,UserName,OrganizationID,PaymentInfoId,PaymentInfo2Id")] Payment payment)
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
                return Redirect("~/");
            }
            ViewData["OrganizationID"] = new SelectList(_context.Organization, "OrganizationID", "ImageUrl", payment.OrganizationID);
            ViewData["PaymentInfoId"] = new SelectList(_context.PaymentInfo, "PaymentInfoId", "PaymentInfoName", payment.PaymentInfoId);
            ViewData["PaymentInfo2Id"] = new SelectList(_context.PaymentInfo2, "PaymentInfo2Id", "PaymentInfo2Name", payment.PaymentInfo2Id);
            return View(payment);
        }

        // GET: Payment/Delete/5
        [Authorize]
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
                .Include(p => p.PaymentInfo2)
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
            var getID = payment?.OrganizationID;
            if (payment != null)
            {
                _context.Payment.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return Redirect("~/Organization/Details/" + getID);
        }

        private bool PaymentExists(int id)
        {
          return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
