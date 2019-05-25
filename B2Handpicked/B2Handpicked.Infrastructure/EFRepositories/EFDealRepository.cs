using B2Handpicked.DomainModel;
using B2Handpicked.Infrastructure.EFRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace B2Handpicked.Infrastructure {
    public class EFDealRepository : GenericEFRepository<Deal> {
        public EFDealRepository(IAppDbContext database) : base(database) { }

        protected override DbSet<Deal> DbSet => _database.Deals;

        protected override void Map(Deal changingDeal, Deal deal) {
            changingDeal.Title = deal.Title;
            changingDeal.Value = deal.Value;
            changingDeal.Deadline = deal.Deadline;
            changingDeal.Percentage = deal.Percentage;
        }

        // Model error validation is not supported on this model. Return an empty list.
        public override async Task<IDictionary<string, string>> GetModelErrors(Deal deal) => await new Task<IDictionary<string, string>>(() => new Dictionary<string, string>());
    }
}
