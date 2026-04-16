using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Linq;

namespace VendingMachineApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly MockTransactionRepository _transactionRepo;

        public TransactionController(MockTransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public IActionResult Index(DateTime? date)
        {
            var transactions = date.HasValue 
            ? _transactionRepo.GetByTransactionDate(date.Value) 
            : _transactionRepo.GetAll();
            return View(transactions);
        }

        public IActionResult Details(int id)
        {
            var transaction = _transactionRepo.GetById(id);
            if (transaction == null) return NotFound();
            return View(transaction);
        }
    }
}
