using ContactAddress.Application.Core;
using ContactAddress.Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAddress.Infrastructures.DataAccess.Contacts
{
    public class Repository : IRepository<Contact>
    {
        private IDbContext _dbContext;
        public Repository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Contact model)
        {
            _dbContext.Contacts.Add(model);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<Contact> DeleteEmailAddressAync(string emailaddress)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => emailaddress.ToLower() == c.EmailAddress.ToLower());
            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> GetByEmailAddressAync(string emailaddress)
        {
            return await _dbContext.Contacts.Where(c => emailaddress.ToLower() == c.EmailAddress.ToLower()).FirstOrDefaultAsync();
        }
    }
}
