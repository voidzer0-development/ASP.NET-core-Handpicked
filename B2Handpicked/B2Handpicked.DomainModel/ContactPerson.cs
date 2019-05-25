using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2Handpicked.DomainModel {
    public class ContactPerson : IEntity {
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

        public int? CustomerId { get; set; }

        [JsonIgnore]
        [ForeignKey("CustomerId")]

        public virtual Customer Customer { get; set; }

        [NotMapped]
        public int? LabelId {
            get => Customer?.LabelId;
        }

        [NotMapped, JsonIgnore]
        public Label Label {
            get => Customer?.Label;
        }
    }
}
