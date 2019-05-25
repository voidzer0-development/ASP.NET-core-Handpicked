using B2Handpicked.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// ApplicationDbContext connects EntityFramework to a database of choice
namespace B2Handpicked.Infrastructure {
    public class ApplicationDbContext : DbContext, IAppDbContext {
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Deal> DealEmployees { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Label> Labels { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasMany<Deal>(e => e.Deals).WithOne(e => e.Customer).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Customer>().HasMany<Invoice>(e => e.Invoices).WithOne(e => e.Customer).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Customer>().HasMany<ContactPerson>(e => e.ContactPersons).WithOne(e => e.Customer).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Label>().HasMany<Employee>(e => e.Employees).WithOne(e => e.Label).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Label>().HasMany<Invoice>(e => e.Invoices).WithOne(e => e.Label).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Label>().HasMany<Customer>(e => e.Customers).WithOne(e => e.Label).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Employee>().HasMany<DealEmployee>(e => e.DealEmployees).WithOne(e => e.Employee).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Deal>().HasMany<DealEmployee>(e => e.DealEmployees).WithOne(e => e.Deal).OnDelete(DeleteBehavior.SetNull);
        }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();
    }
}
