using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace B2Handpicked.UI.Models {
    public class RegisterModel : LoginModel {

        [Required(ErrorMessage = "This field is required"), EmailAddress, DisplayName("Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required"), Phone, DisplayName("Phone number")]
        public string PhoneNumber { get; set; }
    }
}
