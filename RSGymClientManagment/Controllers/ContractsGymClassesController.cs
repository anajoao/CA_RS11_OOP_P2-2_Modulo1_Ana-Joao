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
    public class ContractsGymClassesController : Controller
    {
        private readonly ClientManagmentContext _context;

        public ContractsGymClassesController(ClientManagmentContext context)
        {
            _context = context;
        }

        // GET: ContractsGymClasses
        public async Task<IActionResult> Index()
        {
            var clientManagmentContext = _context.ContractsGymClasses.Include(c => c.Contract).Include(c => c.GymClass);
            return View(await clientManagmentContext.ToListAsync());
        }

        // GET: ContractsGymClasses/Details/5
        public async Task<IActionResult> Details(int contractId, int gymClassId)
        {
            if (contractId == 0 || gymClassId == 0)
            {
                return NotFound();
            }

            var contractsGymClasses = await _context.ContractsGymClasses
                .Include(c => c.Contract)
                .Include(c => c.GymClass)
                .FirstOrDefaultAsync(m => m.ContractId == contractId && m.GymClassId == gymClassId);
            if (contractsGymClasses == null)
            {
                return NotFound();
            }

            return View(contractsGymClasses);
        }

        // GET: ContractsGymClasses/Create
        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId");
            ViewData["GymClassId"] = new SelectList(_context.GymClasses, "GymClassId", "ClassName");
            return View();
        }

        // POST: ContractsGymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,GymClassId")] ContractsGymClasses contractsGymClasses)
        {
            // Verifica o tipo de contrato
            var contract = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Loyalty)
                .FirstOrDefaultAsync(c => c.ContractId == contractsGymClasses.ContractId);

            if (contract == null)
            {
                ModelState.AddModelError("", "Contract not found.");
                return View(contractsGymClasses);
            }

            if (contract.Contract == ContractType.PerSession)
            {
                // Contar o número de pagamentos associados a este contrato
                var paymentCount = await _context.Payments
                    .CountAsync(p => p.ContractId == contract.ContractId);

                // Contar o número de aulas já associadas a este contrato
                var classCount = await _context.ContractsGymClasses
                    .CountAsync(cgc => cgc.ContractId == contract.ContractId);

                // Verificar se o número de aulas associadas é igual ou superior ao número de pagamentos
                if (classCount >= paymentCount)
                {
                    ModelState.AddModelError("", "The number of associated classes cannot exceed the number of payments. Please make a new payment to register for more classes.");
                    ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", contractsGymClasses.ContractId);
                    ViewData["GymClassId"] = new SelectList(_context.GymClasses, "GymClassId", "ClassName", contractsGymClasses.GymClassId);
                    return View(contractsGymClasses);
                }
            }
            else if (contract.Contract == ContractType.Monthly && contract.Loyalty!.LoyaltyProgram == false)
            {
                // Verifica quantas aulas o cliente já selecionou
                var classCount = await _context.ContractsGymClasses
                    .CountAsync(cgc => cgc.ContractId == contract.ContractId);

                if (classCount >= 2)
                {
                    // Se o cliente já tiver selecionado 2 aulas, impede a seleção de mais
                    ModelState.AddModelError("", "Customers with a monthly contract without loyalty can only select two classes.");
                    ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", contractsGymClasses.ContractId);
                    ViewData["GymClassId"] = new SelectList(_context.GymClasses, "GymClassId", "ClassName", contractsGymClasses.GymClassId);
                    return View(contractsGymClasses);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(contractsGymClasses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", contractsGymClasses.ContractId);
            ViewData["GymClassId"] = new SelectList(_context.GymClasses, "GymClassId", "ClassName", contractsGymClasses.GymClassId);
            return View(contractsGymClasses);
        }

        // GET: ContractsGymClasses/Edit/5
        public async Task<IActionResult> Edit(int contractId, int gymClassId)
        {
            if (contractId == 0 || gymClassId == 0)
            {
                return NotFound();
            }

            var contractsGymClasses = await _context.ContractsGymClasses.FindAsync(contractId, gymClassId);
            if (contractsGymClasses == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", contractsGymClasses.ContractId);
            ViewData["GymClassId"] = new SelectList(_context.GymClasses, "GymClassId", "ClassName", contractsGymClasses.GymClassId);
            return View(contractsGymClasses);
        }

        // POST: ContractsGymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int contractId, int gymClassId, [Bind("ContractId,GymClassId")] ContractsGymClasses contractsGymClasses)
        {
            if (contractId != contractsGymClasses.ContractId || gymClassId != contractsGymClasses.GymClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractsGymClasses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractsGymClassesExists(contractsGymClasses.ContractId, contractsGymClasses.GymClassId))
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
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", contractsGymClasses.ContractId);
            ViewData["GymClassId"] = new SelectList(_context.GymClasses, "GymClassId", "ClassName", contractsGymClasses.GymClassId);
            return View(contractsGymClasses);
        }

        // GET: ContractsGymClasses/Delete/5
        public async Task<IActionResult> Delete(int contractId, int gymClassId)
        {
            if (contractId == 0 || gymClassId == 0)
            {
                return NotFound();
            }

            var contractsGymClasses = await _context.ContractsGymClasses
                .Include(c => c.Contract)
                .Include(c => c.GymClass)
                .FirstOrDefaultAsync(m => m.ContractId == contractId && m.GymClassId == gymClassId);
            if (contractsGymClasses == null)
            {
                return NotFound();
            }

            return View(contractsGymClasses);
        }

        // POST: ContractsGymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int contractId, int gymClassId)
        {
            var contractsGymClasses = await _context.ContractsGymClasses.FindAsync(contractId, gymClassId);
            if (contractsGymClasses != null)
            {
                _context.ContractsGymClasses.Remove(contractsGymClasses);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        private bool ContractsGymClassesExists(int contractId, int gymClassId)
        {
            return _context.ContractsGymClasses.Any(e => e.ContractId == contractId && e.GymClassId == gymClassId);
        }
    }
}
