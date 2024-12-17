using System.ComponentModel.DataAnnotations;

namespace company.ass.pl.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "invalid")]

        public string Email { get; set; }
    }
}
