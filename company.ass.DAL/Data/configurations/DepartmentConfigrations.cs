using company.ass.DAL.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.DAL.Data.configurations
{
    public class DepartmentConfigrations : IEntityTypeConfiguration<Departments>
    {
        public void Configure(EntityTypeBuilder<Departments> builder)
        {
            builder.Property(d => d.id).UseIdentityColumn(10, 10);
        }
    }
}
