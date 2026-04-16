using VendingMachineApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class MockTransactionRepository
    {
        private List<Transaction> transactions = new List<Transaction>
        {
            new Transaction { TransactionId = 1, ProductNumberEntered = 1, AmountPaid = 2.5m, TotalPrice = 2.5m, TransactionDate = DateTime.Now.AddDays(-1), Status = TransactionStatus.Successful, QuantityDispensed = 1, MachineId = 1, ProductId = 1 },
            new Transaction { TransactionId = 2, ProductNumberEntered = 2, AmountPaid = 1.5m, TotalPrice = 1.5m, TransactionDate = DateTime.Now.AddDays(-2), Status = TransactionStatus.Failed, QuantityDispensed = 0, MachineId = 2, ProductId = 3 },
            new Transaction { TransactionId = 3, ProductNumberEntered = 3, AmountPaid = 2.0m, TotalPrice = 2.0m, TransactionDate = DateTime.Now.AddDays(-3), Status = TransactionStatus.Successful, QuantityDispensed = 1, MachineId = 1, ProductId = 2 },
            new Transaction { TransactionId = 4, ProductNumberEntered = 4, AmountPaid = 3.5m, TotalPrice = 3.5m, TransactionDate = DateTime.Now.AddDays(-4), Status = TransactionStatus.Successful, QuantityDispensed = 1, MachineId = 3, ProductId = 6 }
        };
        public List<Transaction> GetAll() => transactions;
        public List<Transaction> GetByTransactionDate(DateTime date) => transactions.Where(x => x.TransactionDate.Date == date.Date).ToList();

        public Transaction? GetById(int id)
        {
            return transactions.FirstOrDefault(x => x.TransactionId == id);
        }
    }
}