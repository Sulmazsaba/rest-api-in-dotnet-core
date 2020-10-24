using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;
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
        public ActionResult<IEnumerable<CompanyDto>> GetCompanies()
        {
            var companiesFromRepo = jobRepository.GetCompanies();
            return Ok(mapper.Map<IEnumerable<CompanyDto>>(companiesFromRepo));

        }
    }
}
