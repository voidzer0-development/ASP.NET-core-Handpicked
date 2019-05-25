using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2Handpicked.DomainModel {
    public class Invoice : IEntity {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int Number { get; set; }

        [Required(ErrorMessage = "This field is required"), DataType(DataType.Currency)]
        public float Value { get; set; }

        [Required(ErrorMessage = "This field is required"), DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public int? CustomerId { get; set; }

        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public int? LabelId { get; set; }

        [JsonIgnore]
        [ForeignKey("LabelId")]
        public virtual Label Label { get; set; }
    }
}
