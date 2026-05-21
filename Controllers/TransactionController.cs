using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;
using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(DateTime? date)
        {
            var transactionsQuery = _context.Transactions
                .Include(t => t.VendingMachine)
                .Include(t => t.Product)
                .AsQueryable();

            if (date.HasValue)
            {
                transactionsQuery = transactionsQuery.Where(t => 
                t.TransactionDate.Date == date.Value.Date &&
                t.TransactionDate < date.Value.Date.AddDays(1));
            }
            var transactions = await transactionsQuery.ToListAsync();
            return View(transactions);
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.VendingMachine)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(t => t.TransactionId == id);
                
            if (transaction == null)            
                return NotFound();
            
            return View(transaction);
        }

        // GET: Transactions/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.VendingMachines = new SelectList(
                await _context.VendingMachines.ToListAsync(),
                "MachineId",
                "MachineNumber");

            ViewBag.Products = new SelectList(
                await _context.Products.ToListAsync(),
                "ProductId",
                "Name");

            return View();
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.VendingMachines = new SelectList(
                    await _context.VendingMachines.ToListAsync(),
                    "MachineId",
                    "MachineNumber",
                    transaction.MachineId);

                ViewBag.Products = new SelectList(
                    await _context.Products.ToListAsync(),
                    "ProductId",
                    "Name",
                    transaction.ProductId);

                return View(transaction);
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
                return NotFound();

            ViewBag.VendingMachines = new SelectList(
                await _context.VendingMachines.ToListAsync(),
                "MachineId",
                "MachineNumber",
                transaction.MachineId);

            ViewBag.Products = new SelectList(
                await _context.Products.ToListAsync(),
                "ProductId",
                "Name",
                transaction.ProductId);

            return View(transaction);
        }

        // POST: Transactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transaction transaction)
        {
            if (id != transaction.TransactionId)            
                return BadRequest();
            
            if (!ModelState.IsValid)
            {
                ViewBag.VendingMachines = new SelectList(
                    await _context.VendingMachines.ToListAsync(), 
                    "MachineId", 
                    "MachineNumber", 
                    transaction.MachineId);

                ViewBag.Products = new SelectList(
                    await _context.Products.ToListAsync(), 
                    "ProductId", 
                    "Name", 
                    transaction.ProductId);

                return View(transaction);
            }
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.VendingMachine)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(t => t.TransactionId == id);
            if (transaction == null)            
                return NotFound();
           
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
