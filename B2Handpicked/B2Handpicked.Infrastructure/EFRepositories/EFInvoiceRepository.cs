using B2Handpicked.DomainModel;
using B2Handpicked.Infrastructure.EFRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace B2Handpicked.Infrastructure {

    public class EFInvoiceRepository : GenericEFRepository<Invoice> {
        public EFInvoiceRepository(IAppDbContext database) : base(database) { }

        protected override DbSet<Invoice> DbSet => _database.Invoices;

        protected override void Map(Invoice changingInvoice, Invoice invoice) {
            changingInvoice.Number = invoice.Number;
            changingInvoice.Value = invoice.Value;
            changingInvoice.Date = invoice.Date;
            changingInvoice.LabelId = invoice.LabelId;
            changingInvoice.CustomerId = invoice.CustomerId;
        }

        public override async Task<IDictionary<string, string>> GetModelErrors(Invoice invoice) {
            var dict = new Dictionary<string, string>();
            
            // Check if the foreign keys of this model exist.
            var customer = await new EFCustomerRepository(_database).GetById(invoice.CustomerId);
            if (customer is null && invoice.CustomerId != null) dict.Add(nameof(Invoice.CustomerId), "Customer doesn't exist");

            return dict;
        }
    }
}
