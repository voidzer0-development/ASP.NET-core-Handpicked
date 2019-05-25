using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace B2Handpicked.DomainModel {
    public class Employee : IEntity {
        public int Id { get; set; }

        [EmailAddress, DisplayName("Gmail address"), MaxLength(128, ErrorMessage = "Must be maximally 128 characters")]
        public string Gmail { get; set; }

        /* 
         * Put a limit on the character count to increase Database performance. 
         * See https://stackoverflow.com/questions/30485/what-is-a-reasonable-length-limit-on-person-name-fields#30509 for information about the chosen length.
         */
        [MaxLength(64, ErrorMessage = "Must be maximally 64 characters")]
        public string Name { get; set; }

        [Phone, DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Has access")]
        public bool HasAccess { get; set; } = false;

        [JsonIgnore]
        public string OAuth { get; set; }

        [NotMapped]
        public IEnumerable<int> DealIds {
            get => DealEmployees.Select(item => item.DealId).Where(id => id != null) as IEnumerable<int> ?? new HashSet<int>();
            set => DealEmployees = value.Select(dealId => new DealEmployee { DealId = dealId, EmployeeId = Id }).ToHashSet();
        }

        [NotMapped, JsonIgnore]
        public IEnumerable<Deal> Deals {
            get => DealEmployees.Select(item => item.Deal);
        }

        [JsonIgnore]
        public virtual ICollection<DealEmployee> DealEmployees { get; set; } = new HashSet<DealEmployee>();

        public int? LabelId { get; set; }

        [JsonIgnore]
        [ForeignKey("LabelId")]
        public virtual Label Label { get; set; }
    }
}
