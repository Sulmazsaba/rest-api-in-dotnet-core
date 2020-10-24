using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sample.Entities;
using Sample.Helpers;
using Sample.Models;

namespace Sample.Profiles
{
    public class CompaniesProfile : Profile
    {
        public CompaniesProfile()
        {
            CreateMap<Company,CompanyDto>()
                .ForMember(dest=>dest.CompanyAge,
                    opt=>
                        opt.MapFrom(src=>src.DateTime.GetCurrentAge()));

            CreateMap<CompanyForCreationDto,Company>();
        }
    }
}
