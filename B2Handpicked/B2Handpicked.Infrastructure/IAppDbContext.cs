using B2Handpicked.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace B2Handpicked.Infrastructure {
    public interface IAppDbContext {
        DbSet<ContactPerson> ContactPersons { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Deal> Deals { get; set; }
        DbSet<Deal> DealEmployees { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<Label> Labels { get; set; }

        Task<int> SaveChangesAsync();
    }
}
