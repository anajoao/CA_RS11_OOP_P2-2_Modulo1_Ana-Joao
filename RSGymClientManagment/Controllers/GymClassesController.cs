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
    public class GymClassesController : Controller
    {
        private readonly ClientManagmentContext _context;

        public GymClassesController(ClientManagmentContext context)
        {
            _context = context;
        }

        // GET: GymClasses
        public async Task<IActionResult> Index()
        {
              return _context.GymClasses != null ? 
                          View(await _context.GymClasses.ToListAsync()) :
                          Problem("Entity set 'ClientManagmentContext.GymClasses'  is null.");
        }

        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }

            var gymClasses = await _context.GymClasses
                .FirstOrDefaultAsync(m => m.GymClassId == id);
            if (gymClasses == null)
            {
                return NotFound();
            }

            return View(gymClasses);
        }

        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymClassId,ClassName,ClassPrice")] GymClasses gymClasses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClasses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClasses);
        }

        // GET: GymClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }

            var gymClasses = await _context.GymClasses.FindAsync(id);
            if (gymClasses == null)
            {
                return NotFound();
            }
            return View(gymClasses);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymClassId,ClassName,ClassPrice")] GymClasses gymClasses)
        {
            if (id != gymClasses.GymClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClasses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassesExists(gymClasses.GymClassId))
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
            return View(gymClasses);
        }

        // GET: GymClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }

            var gymClasses = await _context.GymClasses
                .FirstOrDefaultAsync(m => m.GymClassId == id);
            if (gymClasses == null)
            {
                return NotFound();
            }

            return View(gymClasses);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GymClasses == null)
            {
                return Problem("Entity set 'ClientManagmentContext.GymClasses'  is null.");
            }
            var gymClasses = await _context.GymClasses.FindAsync(id);
            if (gymClasses != null)
            {
                _context.GymClasses.Remove(gymClasses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassesExists(int id)
        {
          return (_context.GymClasses?.Any(e => e.GymClassId == id)).GetValueOrDefault();
        }
    }
}
