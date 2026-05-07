namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Complex class representing a product slot (inventory item) in a vending machine
    /// Represents 1-N relationship between VendingMachine and Product through this join entity
    /// </summary>
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class ProductSlot
    {
        
        [Key]
        public int ProductSlotId { get; set; }
        public int SlotNumber { get; set; }
        public int CurrentQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public DateTime LastRestockedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsEmpty { get; set; }
        // Foreign Keys
        [ForeignKey("VendingMachine")]
        [Required]        
        public int MachineId { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        // Navigation Properties
        public virtual VendingMachine? VendingMachine { get; set; }
        public virtual Product? Product { get; set; }
    }
}