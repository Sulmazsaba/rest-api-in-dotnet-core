using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Entities;
using Sample.Models;
using Sample.ResourceParameters;
using Sample.Services;

namespace Sample.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private IMapper mapper;
        private IJobRepository jobRepository;

        public CompaniesController(IMapper mapper, IJobRepository jobRepository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.jobRepository = jobRepository?? throw new ArgumentNullException(nameof(jobRepository));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<CompanyDto>> GetCompanies
            ([FromQuery] CompaniesResourceParameters companiesResourceParameters)
        {
            var companiesFromRepo = jobRepository.GetCompanies(companiesResourceParameters);
            return Ok(mapper.Map<IEnumerable<CompanyDto>>(companiesFromRepo));
        }

        [HttpGet("{companyId}",Name= "GetCompany")]
        public ActionResult<CompanyDto> GetCompany(Guid companyId)
        {
            var companyFromRepo = jobRepository.GetCompany(companyId);
            if (companyFromRepo == null)
                return NotFound();

            return mapper.Map<CompanyDto>(companyFromRepo);
        }

        [HttpPost]
        public ActionResult<CompanyDto> CreateCompany(CompanyForCreationDto companyForCreationDto)
        {
            
            var entity = mapper.Map<Company>(companyForCreationDto);
            jobRepository.AddCompany(entity);
            jobRepository.Save();

            var companyToReturn = mapper.Map<CompanyDto>(entity);

            return CreatedAtRoute("GetCompany",
           new  {companyId=companyToReturn.Id},
            companyToReturn);
        }

        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allows","Get,Options,Post");
            return Ok();
        }


    }
}
