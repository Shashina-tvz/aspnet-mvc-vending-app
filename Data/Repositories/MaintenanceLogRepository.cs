using VendingMachineApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class MaintenanceLogRepository
    {
        private readonly AppDbContext _context;

        public MaintenanceLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MaintenanceLog>> GetAllAsync()
        {
            return await _context.MaintenanceLogs.ToListAsync();
        }

        public async Task<List<MaintenanceLog>> GetByMachineIdAsync(int machineId)
        {
            return await _context.MaintenanceLogs
                .Where(x => x.MachineId == machineId)
                .ToListAsync();
        }

        public async Task<MaintenanceLog?> GetByIdAsync(int id)
        {
            return await _context.MaintenanceLogs.FindAsync(id);
        }

        public async Task AddAsync(MaintenanceLog log)
        {
            await _context.MaintenanceLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MaintenanceLog log)
        {
            _context.MaintenanceLogs.Update(log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var log = await _context.MaintenanceLogs.FindAsync(id);
            if (log != null)
            {
                _context.MaintenanceLogs.Remove(log);
                await _context.SaveChangesAsync();
            }
        }
    }
}