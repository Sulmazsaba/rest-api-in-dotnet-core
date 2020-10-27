using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sample.Entities;
using Sample.Models;

namespace Sample.Profiles
{
    public class JobPositionsProfile : Profile
    {
        public JobPositionsProfile()
        {
            CreateMap<JobPosition, JobPositionDto>();
            CreateMap<JobPositionForCreationDto, JobPosition>();
            CreateMap<JobPositionForUpdateDto, JobPosition>();
        }
    }
}
