using B2Handpicked.DomainModel;
using B2Handpicked.Infrastructure.EFRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace B2Handpicked.Infrastructure {
    public class EFEmployeeRepository : GenericEFRepository<Employee> {
        public EFEmployeeRepository(IAppDbContext database) : base(database) { }

        protected override DbSet<Employee> DbSet => _database.Employees;

        protected override void Map(Employee changingEmployee, Employee employee) {
            changingEmployee.Gmail = employee.Gmail;
            changingEmployee.Name = employee.Name;
            changingEmployee.PhoneNumber = employee.PhoneNumber;
            changingEmployee.HasAccess = employee.HasAccess;
            changingEmployee.OAuth = employee.OAuth;
        }

        // Model error validation is not supported on this model. Return an empty list.
        public override async Task<IDictionary<string, string>> GetModelErrors(Employee employee) => await new Task<IDictionary<string, string>>(() => new Dictionary<string, string>());
    }
}

