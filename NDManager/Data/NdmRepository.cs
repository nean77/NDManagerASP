using NDManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDManager.Data
{
    public class NdmRepository<T> : INdmRepository<T> where T : class
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<NdmRepository<T>> _logger;
        private readonly DbSet<T> _table = null;

        public NdmRepository(ApplicationDbContext ctx, ILogger<NdmRepository<T>> logger)
        {
            _ctx = ctx;
            _logger = logger;

            _table = _ctx.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _ctx.Groups
                .Include(g => g.Teacher)
                .ToListAsync();
        }
        public async Task<IEnumerable<Group>> GetAllActiveGroupsAsync()
        {
            return await _ctx.Groups
                .Include(g => g.Teacher)
                .Where(x=>x.IsActive == true)
                .ToListAsync();
        }
        public async Task<Group> GetGroupByIdAsync(int id)
        {
            return await _ctx.Groups
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _ctx.Teachers.ToListAsync();
        }

        public async Task<IEnumerable<Kid>> GetAllKidsAsync()
        {
            return await _ctx.Kids
                .Include(k => k.Group)
                .ToListAsync();
        }

        public async Task<IEnumerable<Kid>> GetAllKidsByGroupAsync(int id)
        {
            return await _ctx.Kids
                .Where(k => k.GroupId == id)
                .ToListAsync();
        }
        public async Task<Kid> GetKidByIdAsync(int id)
        {
            return await _ctx.Kids
                .Include(g => g.Group)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _ctx.Payment
                .Include(k => k.Kid)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Payment> GetPaymentByKidIdAsync(int id)
        {
            return await _ctx.Payment
                .Include(k=>k.Kid)
                .FirstOrDefaultAsync(p=>p.KidId==id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByGroupId(int id)
        {
            return await _ctx.Payment
                .Include(k=>k.Kid)
                .Where(k=>k.Kid.GroupId == id)
                .ToListAsync();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public async Task<bool> UpdateAsync(T obj)
        {
            _table.Update(obj);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public async Task InsertAsync(T obj)
        {
            _table.Add(obj);
            await _ctx.SaveChangesAsync();
        }
        public async Task DeleteAsync(T obj)
        {
            _table.Remove(obj);
            await _ctx.SaveChangesAsync();
        }
    }
}
