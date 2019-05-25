using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2Handpicked.DomainModel {
    public class DealEmployee : IEntity {
        public int Id { get; set; }

        public int? DealId { get; set; }

        [JsonIgnore]
        [ForeignKey("DealId")]
        public virtual Deal Deal { get; set; }

        public int? EmployeeId { get; set; }

        [JsonIgnore]
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
