using VendingMachineApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class MockSupplierRepository
    {
        private List<Supplier> suppliers = new List<Supplier>
        {
            new Supplier {
                SupplierId = 1,
                Name = "Coca-Cola Bottling Hrvatska",
                PhoneNumber = "0800-123-456",
                Email = "info@cocacola.hr",
                Address = "Radnička cesta 1, 10000 Zagreb",
                ContactPerson = "Ana Kola",
                RegistrationDate = DateTime.Now.AddYears(-12)
            },
            new Supplier {
                SupplierId = 2,
                Name = "Mars Wrigley Hrvatska",
                PhoneNumber = "0800-555-333",
                Email = "info@mars.com",
                Address = "Savska cesta 32, 10000 Zagreb",
                ContactPerson = "Marko Mars",
                RegistrationDate = DateTime.Now.AddYears(-8)
            },
            new Supplier {
                SupplierId = 3,
                Name = "Hrusty d.o.o.",
                PhoneNumber = "0800-444-555",
                Email = "info@hrusty.hr",
                Address = "Hrusty Lane 7, 10000 Zagreb",
                ContactPerson = "Hrvoje Čips",
                RegistrationDate = DateTime.Now.AddYears(-7)
            },
            new Supplier {
                SupplierId = 4,
                Name = "Snack&Go d.o.o.",
                PhoneNumber = "0800-777-888",
                Email = "info@snackgo.hr",
                Address = "Ulica Snackova 15, 21000 Split",
                ContactPerson = "Petra Grickalica",
                RegistrationDate = DateTime.Now.AddYears(-4)
            }
        };
        public List<Supplier> GetAll() => suppliers;
        public Supplier GetByName(string name) => suppliers.FirstOrDefault(x => x.Name == name)?? new Supplier();

        public Supplier GetById(int id)
        {
            return suppliers.FirstOrDefault(x => x.SupplierId == id) ?? new Supplier();
        }
    }
}