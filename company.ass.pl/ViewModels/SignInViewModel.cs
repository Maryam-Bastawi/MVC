using System.ComponentModel.DataAnnotations;

namespace company.ass.pl.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email is Required !")]

        [EmailAddress(ErrorMessage = "invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}
