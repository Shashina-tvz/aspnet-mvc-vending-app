using VendingMachineApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class VendingMachineRepository
    {
        private readonly AppDbContext _context;

        public VendingMachineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VendingMachine>> GetAllAsync()
        {
            return await _context.VendingMachines.ToListAsync();
        }

        public async Task<VendingMachine?> GetByMachineNumberAsync(int machineNumber)
        {
            return await _context.VendingMachines
                .FirstOrDefaultAsync(x => x.MachineNumber == machineNumber);
        }

        public async Task<VendingMachine?> GetByIdAsync(int id)
        {
            return await _context.VendingMachines.FindAsync(id);
        }

        public async Task AddAsync(VendingMachine machine)
        {
            await _context.VendingMachines.AddAsync(machine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VendingMachine machine)
        {
            _context.VendingMachines.Update(machine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var machine = await _context.VendingMachines.FindAsync(id);
            if (machine != null)
            {
                _context.VendingMachines.Remove(machine);
                await _context.SaveChangesAsync();
            }
        }
    }
}