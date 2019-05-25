using B2Handpicked.DomainModel;
using B2Handpicked.Infrastructure.EFRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace B2Handpicked.Infrastructure {
    public class EFLabelRepository : GenericEFRepository<Label> {
        public EFLabelRepository(IAppDbContext database) : base(database) { }

        protected override DbSet<Label> DbSet => _database.Labels;

        protected override void Map(Label changingLabel, Label label) {
            changingLabel.Token = label.Token;
            changingLabel.Name = label.Name;
        }

        // Model error validation is not supported on this model. Return an empty list.
        public override async Task<IDictionary<string, string>> GetModelErrors(Label label) => await new Task<IDictionary<string, string>>(() => new Dictionary<string, string>());
    }
}
