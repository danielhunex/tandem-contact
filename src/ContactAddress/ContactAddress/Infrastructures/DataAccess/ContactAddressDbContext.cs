using ContactAddress.Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContactAddress.Infrastructures.DataAccess
{
    public class ContactAddressDbContext : DbContext, IDbContext
    {
        public ContactAddressDbContext(DbContextOptions<ContactAddressDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var server = "localhost";
        //    var port = "5432";
        //    var name = "contactdb";
        //    var user = "admin";
        //    var password = "p@ssword";

        //    optionsBuilder.UseNpgsql($"Host={server};Port={port};Database={name};Username={user};Password={password}");
        //}

    }
}
