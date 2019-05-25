using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace B2Handpicked.DomainModel {
    public class Customer : IEntity {
        public int Id { get; set; }

        [EmailAddress, DisplayName("Email address"), MaxLength(128, ErrorMessage = "Must be maximally 128 characters")]
        public string Email { get; set; }

        /* 
         * Put a limit on the character count to increase Database performance. 
         * See https://stackoverflow.com/questions/30485/what-is-a-reasonable-length-limit-on-person-name-fields#30509 for information about the chosen length.
         */
        [MaxLength(64, ErrorMessage = "Must be maximally 64 characters")]
        public string Name { get; set; }

        [Phone, DisplayName("Phone number")]
        public string PhoneNumber { get; set; }


        [NotMapped]
        public IEnumerable<int> ContactPersonIds {
            get => ContactPersons.Select(item => item.Id);
        }

        [JsonIgnore]
        public virtual ICollection<ContactPerson> ContactPersons { get; set; } = new HashSet<ContactPerson>();


        public int? LabelId { get; set; }

        [JsonIgnore]
        [ForeignKey("LabelId")]

        public virtual Label Label { get; set; }


        [NotMapped]
        public IEnumerable<int> DealIds {
            get => Deals.Select(item => item.Id);
        }

        [JsonIgnore]
        public virtual ICollection<Deal> Deals { get; set; } = new HashSet<Deal>();


        [NotMapped]
        public IEnumerable<int> InvoiceIds {
            get => Invoices.Select(item => item.Id);
        }

        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}
