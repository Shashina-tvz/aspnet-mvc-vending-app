namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Enum representing the operational status of a vending machine
    /// </summary>
    public enum MachineStatus
    {
        Active = 1,
        Maintenance = 2,
        OutOfService = 3,
        Inactive = 4
    }
}