using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Entities;
using Sample.Helpers;
using Sample.Models;
using Sample.Services;

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/companycollections")]
    public class CompaniesCollectionController : ControllerBase
    {
        private IMapper mapper;
        private IJobRepository jobRepository;

        public CompaniesCollectionController(IJobRepository jobRepository, IMapper mapper)
        {
            this.jobRepository = jobRepository??throw new ArgumentNullException(nameof(jobRepository));
            this.mapper = mapper??throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{ids}",Name = "GetCompanyCollection")]
        public ActionResult<IEnumerable<CompanyDto>> GetCompanyCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
                return BadRequest();

            var companiesFromRepo = jobRepository.GetCompanies(ids);
            

            return Ok(mapper.Map<IEnumerable<CompanyDto>>(companiesFromRepo));
        }



        [HttpPost]
        public ActionResult<IEnumerable<CompanyDto>> CreateCompanies
            (IEnumerable<CompanyForCreationDto> dtos)
        {
            var entities = mapper.Map<IEnumerable<Company>>(dtos);
            foreach (var company in entities)
            {
                jobRepository.AddCompany(company);
            }

            jobRepository.Save();


            var companiesCollectionToReturn = mapper.Map<IEnumerable<CompanyDto>>(entities);
            var companyIds = string.Join(",", entities.Select(i => i.Id));
            return CreatedAtRoute("GetCompanyCollection",
                new{ids=companyIds},
                companiesCollectionToReturn);

        }
    }
}
