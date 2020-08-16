using ContactAddress.Application.Core.Models;
using System.Threading.Tasks;

namespace ContactAddress.Application.Core
{
    public interface IRepository<T> where T : BaseModel
    {

        Task<T> GetByEmailAddressAync(string emailId);
        Task<T> DeleteEmailAddressAync(string emailId);
        Task<int> CreateAsync(T model);
    }
}
