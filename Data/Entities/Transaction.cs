namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Complex class representing a transaction (purchase) made at a vending machine
    /// 1-N relationship with VendingMachine and Product
    /// </summary>
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VendingMachineApp.Data.Validation;
    public class Transaction
    {
        
        [Key]
        public int TransactionId { get; set; }
        public int ProductNumberEntered { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public DateTime TransactionDate { get; set; }
        [Required]
        public TransactionStatus Status { get; set; }
        [MaxLength(200)]
        public string? ErrorMessage { get; set; }
        [GreaterThanZero]
        public int QuantityDispensed { get; set; }
        // Foreign Keys
        [Required]
        [ForeignKey("VendingMachine")]
        public int MachineId { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        // Navigation Properties
        public virtual VendingMachine? VendingMachine { get; set; }
        public virtual Product? Product { get; set; }
    }
}