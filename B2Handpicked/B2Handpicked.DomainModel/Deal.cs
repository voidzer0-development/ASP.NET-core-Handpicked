using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace B2Handpicked.DomainModel {
    public class Deal : IEntity {
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Currency)]
        public float Value { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Deadline { get; set; }

        public int Percentage { get; set; }

        public int? CustomerId { get; set; }

        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [NotMapped]
        public IEnumerable<int> EmployeeIds {
            get => DealEmployees.Select(item => item.EmployeeId).Where(id => id != null) as IEnumerable<int> ?? new HashSet<int>();
            set => DealEmployees = value.Select(employeeId => new DealEmployee { DealId = Id, EmployeeId = employeeId }).ToHashSet();
        }

        [NotMapped, JsonIgnore]
        public IEnumerable<Employee> Employees {
            get => DealEmployees.Select(item => item.Employee);
        }

        [JsonIgnore]
        public virtual ICollection<DealEmployee> DealEmployees { get; set; } = new HashSet<DealEmployee>();

        [NotMapped]
        public int? LabelId {
            get => DealEmployees.Select(item => item.Employee?.LabelId).FirstOrDefault(id => id != null);
        }

        [NotMapped, JsonIgnore]
        public Label Label {
            get => DealEmployees.Select(item => item.Employee?.Label).FirstOrDefault(label => label != null);
        }
    }
}
