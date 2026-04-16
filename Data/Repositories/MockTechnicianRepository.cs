using VendingMachineApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class MockTechnicianRepository
    {
        private List<Technician> technicians = new List<Technician>
        {
            new Technician { TechnicianId = 1, Name = "Ivan Ivić", LicenseNumber = "LIC001", Email = "ivan@firma.com", PhoneNumber = "061-111-111", HireDate = DateTime.Now.AddYears(-5) },
            new Technician { TechnicianId = 2, Name = "Ana Aničić", LicenseNumber = "LIC002", Email = "ana@firma.com", PhoneNumber = "062-222-222", HireDate = DateTime.Now.AddYears(-3) },
            new Technician { TechnicianId = 3, Name = "Marko Marković", LicenseNumber = "LIC003", Email = "marko@firma.com", PhoneNumber = "063-333-333", HireDate = DateTime.Now.AddYears(-2) }
        };
        public List<Technician> GetAll() => technicians;
        public List<Technician> GetByName(string name) => technicians.Where(x => x.Name.Contains(name)).ToList();

        public Technician? GetById(int id)
        {
            return technicians.FirstOrDefault(x => x.TechnicianId == id);
        }
    }
}