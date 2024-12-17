using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.DAL.models
{
    public class Departments : BaseEntity
    {

        [Required(ErrorMessage = "code is required!")]
        public string code { get; set; }
        [Required(ErrorMessage = "name is required!")]
        public string name { get; set; }
      
        [DisplayName("Date of creation")]
        public DateTime Dateofcreation { get; set; }

        public ICollection<Employee>? Employees { get; set; } 
    }
}
