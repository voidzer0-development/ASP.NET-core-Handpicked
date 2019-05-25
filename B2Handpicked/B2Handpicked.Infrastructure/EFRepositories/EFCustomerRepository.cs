using B2Handpicked.DomainModel;
using B2Handpicked.Infrastructure.EFRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace B2Handpicked.Infrastructure {
    public class EFCustomerRepository : GenericEFRepository<Customer> {
        public EFCustomerRepository(IAppDbContext database) : base(database) { }

        protected override DbSet<Customer> DbSet => _database.Customers;

        protected override void Map(Customer changingCustomer, Customer customer) {
            changingCustomer.Email = customer.Email;
            changingCustomer.PhoneNumber = customer.PhoneNumber;
            changingCustomer.Label = customer.Label;
        }

        // Model error validation is not supported on this model. Return an empty list.
        public override async Task<IDictionary<string, string>> GetModelErrors(Customer customer) => await new Task<IDictionary<string, string>>(() => new Dictionary<string, string>());
    }
}
