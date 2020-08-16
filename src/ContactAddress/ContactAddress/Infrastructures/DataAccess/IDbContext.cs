using ContactAddress.Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContactAddress.Infrastructures.DataAccess
{
    public  interface IDbContext
    {
        DbSet<Contact> Contacts { get; set; }
        Task<int> SaveChangesAsync();
    }
}
