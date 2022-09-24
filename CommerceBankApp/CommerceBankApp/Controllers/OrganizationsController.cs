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
    public class OrganizationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organizations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Organization.Include(o => o.DonationType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Organizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Organization == null)
            {
                return NotFound();
            }

            var organization = await _context.Organization
                .Include(o => o.DonationType)
                .FirstOrDefaultAsync(m => m.organizationID == id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // GET: Organizations/Create
        public IActionResult Create()
        {
            ViewData["donationTypeID"] = new SelectList(_context.DonationType, "donationTypeID", "donationTypeID");
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("organizationID,organizationName,donationGoal,currentDonated,organizationDescription,donationTypeID")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["donationTypeID"] = new SelectList(_context.DonationType, "donationTypeID", "donationTypeID", organization.donationTypeID);
            return View(organization);
        }

        // GET: Organizations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Organization == null)
            {
                return NotFound();
            }

            var organization = await _context.Organization.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            ViewData["donationTypeID"] = new SelectList(_context.DonationType, "donationTypeID", "donationTypeID", organization.donationTypeID);
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("organizationID,organizationName,donationGoal,currentDonated,organizationDescription,donationTypeID")] Organization organization)
        {
            if (id != organization.organizationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationExists(organization.organizationID))
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
            ViewData["donationTypeID"] = new SelectList(_context.DonationType, "donationTypeID", "donationTypeID", organization.donationTypeID);
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Organization == null)
            {
                return NotFound();
            }

            var organization = await _context.Organization
                .Include(o => o.DonationType)
                .FirstOrDefaultAsync(m => m.organizationID == id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Organization == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Organization'  is null.");
            }
            var organization = await _context.Organization.FindAsync(id);
            if (organization != null)
            {
                _context.Organization.Remove(organization);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationExists(int id)
        {
          return _context.Organization.Any(e => e.organizationID == id);
        }
    }
}
