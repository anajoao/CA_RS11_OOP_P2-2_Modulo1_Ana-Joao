using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RSGymClientManagment.Data;
using RSGymClientManagment.Models;

namespace RSGymClientManagment.Controllers
{
    public class LoyaltiesController : Controller
    {
        private readonly ClientManagmentContext _context;

        public LoyaltiesController(ClientManagmentContext context)
        {
            _context = context;
        }

        // GET: Loyalties
        public async Task<IActionResult> Index()
        {
              return _context.Loyalties != null ? 
                          View(await _context.Loyalties.ToListAsync()) :
                          Problem("Entity set 'ClientManagmentContext.Loyalties'  is null.");
        }

        // GET: Loyalties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loyalties == null)
            {
                return NotFound();
            }

            var loyalties = await _context.Loyalties
                .FirstOrDefaultAsync(m => m.LoyaltyId == id);
            if (loyalties == null)
            {
                return NotFound();
            }

            return View(loyalties);
        }

        // GET: Loyalties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loyalties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoyaltyId,LoyaltyProgram,Discount")] Loyalties loyalties)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loyalties);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loyalties);
        }

        // GET: Loyalties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loyalties == null)
            {
                return NotFound();
            }

            var loyalties = await _context.Loyalties.FindAsync(id);
            if (loyalties == null)
            {
                return NotFound();
            }
            return View(loyalties);
        }

        // POST: Loyalties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoyaltyId,LoyaltyProgram,Discount")] Loyalties loyalties)
        {
            if (id != loyalties.LoyaltyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loyalties);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoyaltiesExists(loyalties.LoyaltyId))
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
            return View(loyalties);
        }

        // GET: Loyalties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loyalties == null)
            {
                return NotFound();
            }

            var loyalties = await _context.Loyalties
                .FirstOrDefaultAsync(m => m.LoyaltyId == id);
            if (loyalties == null)
            {
                return NotFound();
            }

            return View(loyalties);
        }

        // POST: Loyalties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loyalties == null)
            {
                return Problem("Entity set 'ClientManagmentContext.Loyalties'  is null.");
            }
            var loyalties = await _context.Loyalties.FindAsync(id);
            if (loyalties != null)
            {
                _context.Loyalties.Remove(loyalties);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoyaltiesExists(int id)
        {
          return (_context.Loyalties?.Any(e => e.LoyaltyId == id)).GetValueOrDefault();
        }
    }
}
