using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RSGymClientManagment.Data;
using RSGymClientManagment.Models;
using static RSGymClientManagment.Enums.Enums;

namespace RSGymClientManagment.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ClientManagmentContext _context;

        public PaymentsController(ClientManagmentContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var clientManagmentContext = _context.Payments.Include(p => p.Contract);
            return View(await clientManagmentContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments
                .Include(p => p.Contract)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payments == null)
            {
                return NotFound();
            }

            return View(payments);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId");
            ViewData["PaymentType"] = new SelectList(Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().Select(v => new { Id = (int)v, Name = v.ToString() }), "Id", "Name");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,ContractId,PaymentDate,PaymentType,PaymentValue")] Payments payments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", payments.ContractId);
            ViewData["PaymentType"] = new SelectList(Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().Select(v => new { Id = (int)v, Name = v.ToString() }), "Id", "Name", payments.PaymentType);
            return View(payments);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", payments.ContractId);
            ViewData["PaymentType"] = new SelectList(Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().Select(v => new { Id = (int)v, Name = v.ToString() }), "Id", "Name", payments.PaymentType);
            return View(payments);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,ContractId,PaymentDate,PaymentType,PaymentValue")] Payments payments)
        {
            if (id != payments.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentsExists(payments.PaymentId))
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
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", payments.ContractId);
            ViewData["PaymentType"] = new SelectList(Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().Select(v => new { Id = (int)v, Name = v.ToString() }), "Id", "Name", payments.PaymentType);
            return View(payments);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments
                .Include(p => p.Contract)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payments == null)
            {
                return NotFound();
            }

            return View(payments);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Payments == null)
            {
                return Problem("Entity set 'ClientManagmentContext.Payments'  is null.");
            }
            var payments = await _context.Payments.FindAsync(id);
            if (payments != null)
            {
                _context.Payments.Remove(payments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentsExists(int id)
        {
          return (_context.Payments?.Any(e => e.PaymentId == id)).GetValueOrDefault();
        }
    }
}
