using company.ass.DAL.models;
using System.ComponentModel.DataAnnotations;
using IFormFile = Microsoft.AspNetCore.Http.IFormFile;

namespace company.ass.pl.ViewModels
{
    public class EmployeeViewModel 
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Name is Required !")]
        public string name { get; set; }
        [Range(20, 50, ErrorMessage = "must be from older 20-50 ")]
        public int? age { get; set; }
        [RegularExpression(@"[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "must be like 123-street-city-country")]
        public string address { get; set; }
        [Required(ErrorMessage = "Salary is Required !")]
        [DataType(DataType.Currency)]
        public decimal salary { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime HiringDate { get; set; }
        public int? workforid { set; get; } //fk
        public Departments? workfor { set; get; } //navigation property
        public string? ImgName { get; set; }
        public IFormFile? Image { get; set; }



    }
}
