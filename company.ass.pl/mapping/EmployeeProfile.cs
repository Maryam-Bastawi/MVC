using AutoMapper;
using company.ass.DAL.models;
using company.ass.pl.ViewModels;

namespace company.ass.pl.mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
