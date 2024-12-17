using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.DAL.models
{   //model class
    public class Employee : BaseEntity
    {
        public string name { get; set; }
        public int? age { get; set; }
     
        public string? address { get; set; }
        public decimal salary { get; set; }

        public string Email { get; set; }
 
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public int? workforid { set; get; } //fk
        public Departments? workfor { set; get; } //navigation property
        public string? ImgName { get; set; }


    }
}
