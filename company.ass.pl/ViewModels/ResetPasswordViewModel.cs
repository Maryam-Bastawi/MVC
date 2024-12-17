using System.ComponentModel.DataAnnotations;

namespace company.ass.pl.ViewModels
{
	public class ResetPasswordViewModel
	{
        [Required(ErrorMessage = "Password is Required !")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is Required !")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword is not match the password !")]
        public string ConfirmPassword { get; set; }
    }
}
