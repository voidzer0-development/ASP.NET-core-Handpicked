using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace B2Handpicked.UI.Models {
    public class LoginModel {
        [Required(ErrorMessage = "This field is required"), DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
