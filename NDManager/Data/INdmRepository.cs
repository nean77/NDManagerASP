using NDManager.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NDManager.Data
{
    public interface INdmRepository<T> where T : class
    {
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<IEnumerable<Group>> GetAllActiveGroupsAsync();
        Task<Group> GetGroupByIdAsync(int id);
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<IEnumerable<Kid>> GetAllKidsAsync();
        Task<IEnumerable<Kid>> GetAllKidsByGroupAsync(int id);
        Task<Kid> GetKidByIdAsync(int id);
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<Payment> GetPaymentByKidIdAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByGroupId(int id);
        T GetById(object id);
        bool SaveAll();
        Task<bool> UpdateAsync(T obj);
        Task InsertAsync(T obj);
        Task DeleteAsync(T obj);
    }
}
