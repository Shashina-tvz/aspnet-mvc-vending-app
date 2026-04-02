namespace VendingMachineApp.Models
{
    /// <summary>
    /// Enum representing the status of a vending machine transaction
    /// </summary>
    public enum TransactionStatus
    {
        Pending = 1,
        Successful = 2,
        Failed = 3,
        ProductStuck = 4,
        InsufficientFunds = 5,
        InvalidProductNumber = 6,
        EmptySlot = 7,
        Refunded = 8
    }
}
