using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.DAL.models
{
    public class ApplicationUser : IdentityUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
		public bool IsAgree { get; set; }

	}
}
