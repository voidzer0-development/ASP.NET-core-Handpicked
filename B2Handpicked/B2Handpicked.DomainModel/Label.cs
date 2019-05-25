using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace B2Handpicked.DomainModel {
    public class Label : IEntity {
        public int Id { get; set; }

        [JsonIgnore]
        public string Token { get; set; }

        public string Name { get; set; }

        
        [NotMapped]
        public IEnumerable<int> CustomerIds {
            get => Customers.Select(item => item.Id);
        }

        [JsonIgnore]
        public virtual ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();


        [NotMapped]
        public IEnumerable<int> InvoiceIds {
            get => Invoices.Select(item => item.Id);
        }

        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();


        [NotMapped]
        public IEnumerable<int> EmployeeIds {
            get => Employees.Select(item => item.Id);
        }

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        [NotMapped]
        public IEnumerable<int> DealIds {
            get => Employees.SelectMany(employee => employee.DealIds);
        }

        [NotMapped, JsonIgnore]
        public IEnumerable<Deal> Deals {
            get => Employees.SelectMany(employee => employee.Deals);
        }

        [NotMapped]
        public IEnumerable<int> ContactPersonIds {
            get => Customers.SelectMany(customer => customer.ContactPersonIds);
        }

        [NotMapped, JsonIgnore]
        public IEnumerable<ContactPerson> ContactPersons {
            get => Customers.SelectMany(customer => customer.ContactPersons);
        }
    }
}
