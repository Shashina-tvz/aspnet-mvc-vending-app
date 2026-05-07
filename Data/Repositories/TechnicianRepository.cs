using VendingMachineApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class TechnicianRepository
    {
        private readonly AppDbContext _context;

        public TechnicianRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Technician>> GetAllAsync()
        {
            return await _context.Technicians.ToListAsync();
        }

        public async Task<List<Technician>> GetByNameAsync(string name)
        {
            return await _context.Technicians
                .Where(x => x.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<Technician?> GetByIdAsync(int id)
        {
            return await _context.Technicians.FindAsync(id);
        }

        public async Task AddAsync(Technician technician)
        {
            await _context.Technicians.AddAsync(technician);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Technician technician)
        {
            _context.Technicians.Update(technician);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var technician = await _context.Technicians.FindAsync(id);
            if (technician != null)
            {
                _context.Technicians.Remove(technician);
                await _context.SaveChangesAsync();
            }
        }
    }
}