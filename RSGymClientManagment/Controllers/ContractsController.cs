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
    public class ContractsController : Controller
    {
        private readonly ClientManagmentContext _context;

        public ContractsController(ClientManagmentContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var clientManagmentContext = _context.Contracts.Include(c => c.Client).Include(c => c.Loyalty);
            return View(await clientManagmentContext.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contracts = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Loyalty)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contracts == null)
            {
                return NotFound();
            }

            return View(contracts);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientName");
            ViewData["LoyaltyId"] = new SelectList(_context.Loyalties, "LoyaltyId", "LoyaltyProgram");
            ViewData["ContractType"] = new SelectList(Enum.GetValues(typeof(ContractType)).Cast<ContractType>().Select(v => new { Id = (int)v, Name = v.ToString() }), "Id", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,ClientId,LoyaltyId,Contract,StartDate,EndDate,MonthlyFee")] Contracts contracts)
        {
            // Verifica se o cliente já possui um contrato ativo ou com EndDate nula
            var existingContract = await _context.Contracts
                .Where(c => c.ClientId == contracts.ClientId &&
                           (c.EndDate == null || c.EndDate > DateTime.Now))
                .FirstOrDefaultAsync();

            if (existingContract != null)
            {
                // Se já existir um contrato ativo ou com EndDate nula, não permitir a criação de um novo contrato
                ModelState.AddModelError("", "This client already has an active or ongoing contract with no end date.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(contracts);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientName", contracts.ClientId);
            ViewData["LoyaltyId"] = new SelectList(_context.Loyalties, "LoyaltyId", "LoyaltyProgram", contracts.LoyaltyId);
            ViewData["ContractType"] = new SelectList(Enum.GetValues(typeof(ContractType)).Cast<ContractType>().Select(v => new { Id = (int)v, Name = v.ToString() }), "Id", "Name", contracts.Contract);
            return View(contracts);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contracts = await _context.Contracts.FindAsync(id);
            if (contracts == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientName", contracts.ClientId);
            ViewData["LoyaltyId"] = new SelectList(_context.Loyalties, "LoyaltyId", "LoyaltyProgram", contracts.LoyaltyId);
            ViewData["ContractType"] = new SelectList(Enum.GetValues(typeof(ContractType)).Cast<ContractType>().Select(v => new { Id = (int)v, Name = v.ToString() }), "Id", "Name", contracts.Contract);
            return View(contracts);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractId,ClientId,LoyaltyId,Contract,StartDate,EndDate,MonthlyFee")] Contracts contracts)
        {
            if (id != contracts.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contracts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractsExists(contracts.ContractId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientName", contracts.ClientId);
            ViewData["LoyaltyId"] = new SelectList(_context.Loyalties, "LoyaltyId", "LoyaltyProgram", contracts.LoyaltyId);
            ViewData["ContractType"] = new SelectList(Enum.GetValues(typeof(ContractType)).Cast<ContractType>().Select(v => new { Id = (int)v, Name = v.ToString() }), "Id", "Name", contracts.Contract);
            return View(contracts);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contracts = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Loyalty)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contracts == null)
            {
                return NotFound();
            }

            return View(contracts);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contracts == null)
            {
                return Problem("Entity set 'ClientManagmentContext.Contracts'  is null.");
            }
            var contracts = await _context.Contracts.FindAsync(id);
            if (contracts != null)
            {
                _context.Contracts.Remove(contracts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractsExists(int id)
        {
          return (_context.Contracts?.Any(e => e.ContractId == id)).GetValueOrDefault();
        }
    }
}
