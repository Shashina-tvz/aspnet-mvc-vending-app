namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Complex class representing a transaction (purchase) made at a vending machine
    /// 1-N relationship with VendingMachine and Product
    /// </summary>
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int ProductNumberEntered { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
        public int QuantityDispensed { get; set; }
        // Foreign Keys
        public int MachineId { get; set; }
        public int ProductId { get; set; }
        // Navigation Properties
        public virtual VendingMachine? VendingMachine { get; set; }
        public virtual Product? Product { get; set; }
    }
}