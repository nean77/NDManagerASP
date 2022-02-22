using NDManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public IEnumerable<Kid> GetAllKidsByGroup(Group group)
        {
            return _ctx.Kids
                .Where(k => k.Group == group)
                .ToList();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
            _ctx.SaveChanges();
        }
    }
}
