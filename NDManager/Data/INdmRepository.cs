using NDManager.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NDManager.Data
{
    public interface INdmRepository<T> where T : class
    {
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<IEnumerable<Kid>> GetAllKidsAsync();
        IEnumerable<Kid> GetAllKidsByGroup(Group group);
        T GetById(object id);
        bool SaveAll();
        Task<bool> UpdateAsync(T obj);
        Task InsertAsync(T obj);
    }
}
