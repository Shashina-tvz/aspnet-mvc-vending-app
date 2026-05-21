using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VendingMachineApp.Migrations
{
    /// <inheritdoc />
    public partial class SeededDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "DeliveryDate", "OrderDate", "Status", "SupplierId", "TotalAmount" },
                values: new object[] { 1, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delivered", 1, 100.0m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2027, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "ExpirationDate", "ManufactureDate" },
                values: new object[] { new DateTime(2028, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "ExpirationDate", "ManufactureDate" },
                values: new object[] { new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2012, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "Address", "ContactPerson", "Email", "Name", "PhoneNumber", "RegistrationDate" },
                values: new object[,]
                {
                    { 2, "Savska cesta 32, 10000 Zagreb", "Marko Mars", "info@mars.com", "Mars Wrigley Hrvatska", "0800-555-333", new DateTime(2018, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Hrusty Lane 7, 10000 Zagreb", "Hrvoje Čips", "info@hrusty.hr", "Hrusty d.o.o.", "0800-444-555", new DateTime(2013, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Ulica Snackova 15, 21000 Split", "Petra Grickalica", "info@snackgo.hr", "Snack&Go d.o.o.", "0800-777-888", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Technicians",
                columns: new[] { "TechnicianId", "Email", "HireDate", "LicenseNumber", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "ivan@firma.com", new DateTime(2019, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "LIC001", "Ivan Ivić", "061-111-111" },
                    { 2, "ana@firma.com", new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "LIC002", "Ana Aničić", "062-222-222" },
                    { 3, "marko@firma.com", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LIC003", "Marko Marković", "063-333-333" }
                });

            migrationBuilder.InsertData(
                table: "VendingMachines",
                columns: new[] { "MachineId", "Address", "Capacity", "CurrentBalance", "LastMaintenanceDate", "MachineNumber", "ManufacturedDate", "Status" },
                values: new object[,]
                {
                    { 1, "Ilica 15, Zagreb", 50, 100.0m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 101, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Avenija Dubrovnik 10, Zagreb", 60, 200.0m, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 102, new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Savska cesta 50, Zagreb", 40, 50.0m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 103, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, "Korzo 5, Rijeka", 70, 300.0m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 104, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, "Trg bana Jelačića 1, Zagreb", 55, 0.0m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 105, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 6, "Riva 8, Split", 80, 500.0m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 106, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceLogs",
                columns: new[] { "MaintenanceLogId", "Cost", "Description", "MachineId", "MaintenanceDate", "Status", "TechnicianId" },
                values: new object[,]
                {
                    { 1, 120m, "Zamjena motora", 1, new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Završeno", 1 },
                    { 2, 30m, "Čišćenje aparata", 2, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Završeno", 2 },
                    { 3, 80m, "Popravka displeja", 1, new DateTime(2026, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "U toku", 1 },
                    { 4, 50m, "Zamjena brave", 3, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Završeno", 3 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderId", "ProductId", "Quantity", "SubTotal", "UnitPrice" },
                values: new object[] { 1, 1, 1, 10, 25.0m, 2.5m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "DeliveryDate", "OrderDate", "Status", "SupplierId", "TotalAmount" },
                values: new object[,]
                {
                    { 2, new DateTime(2026, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delivered", 2, 150.0m },
                    { 3, new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delivered", 3, 200.0m },
                    { 4, null, new DateTime(2026, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 4, 80.0m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "Description", "ExpirationDate", "ManufactureDate", "Name", "Price", "ReorderThreshold", "SupplierId" },
                values: new object[,]
                {
                    { 2, 2, "Snack croissant", new DateTime(2026, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "7Days Croissant", 1.8m, 7, 2 },
                    { 3, 2, "Čokoladica", new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Snickers", 1.5m, 10, 2 },
                    { 4, 2, "Čokoladica", new DateTime(2027, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mars", 1.6m, 10, 2 },
                    { 5, 5, "Krompir čips", new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hrusty čips classic", 2.0m, 8, 3 },
                    { 6, 6, "Sendvič", new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sandwich", 3.5m, 3, 4 },
                    { 7, 4, "Muesli bar", new DateTime(2026, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Muesli Bar", 1.2m, 6, 4 },
                    { 10, 5, "Tortilla čips", new DateTime(2026, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doritos", 2.2m, 8, 3 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "AmountPaid", "ErrorMessage", "MachineId", "ProductId", "ProductNumberEntered", "QuantityDispensed", "Status", "TotalPrice", "TransactionDate" },
                values: new object[] { 1, 2.5m, null, 1, 1, 1, 1, 2, 2.5m, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderId", "ProductId", "Quantity", "SubTotal", "UnitPrice" },
                values: new object[,]
                {
                    { 2, 2, 3, 5, 7.5m, 1.5m },
                    { 3, 3, 2, 8, 16.0m, 2.0m },
                    { 4, 4, 6, 3, 10.5m, 3.5m },
                    { 5, 2, 10, 6, 13.2m, 2.2m }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "AmountPaid", "ErrorMessage", "MachineId", "ProductId", "ProductNumberEntered", "QuantityDispensed", "Status", "TotalPrice", "TransactionDate" },
                values: new object[,]
                {
                    { 2, 1.5m, null, 2, 3, 2, 0, 3, 1.5m, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2.0m, null, 1, 2, 3, 1, 2, 2.0m, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 3.5m, null, 3, 6, 4, 1, 2, 3.5m, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MaintenanceLogs",
                keyColumn: "MaintenanceLogId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MaintenanceLogs",
                keyColumn: "MaintenanceLogId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MaintenanceLogs",
                keyColumn: "MaintenanceLogId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MaintenanceLogs",
                keyColumn: "MaintenanceLogId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VendingMachines",
                keyColumn: "MachineId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VendingMachines",
                keyColumn: "MachineId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VendingMachines",
                keyColumn: "MachineId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Technicians",
                keyColumn: "TechnicianId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Technicians",
                keyColumn: "TechnicianId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Technicians",
                keyColumn: "TechnicianId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VendingMachines",
                keyColumn: "MachineId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VendingMachines",
                keyColumn: "MachineId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VendingMachines",
                keyColumn: "MachineId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2027, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "ExpirationDate", "ManufactureDate" },
                values: new object[] { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "ExpirationDate", "ManufactureDate" },
                values: new object[] { new DateTime(2027, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
