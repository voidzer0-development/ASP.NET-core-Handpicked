using B2Handpicked.DomainModel;
using B2Handpicked.Infrastructure.EFRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace B2Handpicked.Infrastructure {
    public class EFContactPersonRepository : GenericEFRepository<ContactPerson> {
        public EFContactPersonRepository(IAppDbContext database) : base(database) {}

        protected override DbSet<ContactPerson> DbSet => _database.ContactPersons;

        protected override void Map(ContactPerson changingContact, ContactPerson contact) {
            changingContact.Email = contact.Email;
            changingContact.Name = contact.Name;
            changingContact.PhoneNumber = contact.PhoneNumber;
            changingContact.Customer = contact.Customer;
        }

        // Model error validation is not supported on this model. Return an empty list.
        public override async Task<IDictionary<string, string>> GetModelErrors(ContactPerson contactPerson) => await new Task<IDictionary<string, string>>(() => new Dictionary<string, string>());
    }
}

