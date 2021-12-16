using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Sample.Entities;
using Sample.Helpers;
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
            this.jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
        }

        [HttpGet(Name = "GetCompanies")]
        [HttpHead]
        public ActionResult<IEnumerable<CompanyDto>> GetCompanies
            ([FromQuery] CompaniesResourceParameters companiesResourceParameters)
        {
            var companiesFromRepo = jobRepository.GetCompanies(companiesResourceParameters);

            var previousLink = companiesFromRepo.HasPrevious
                ? CreateLink(companiesResourceParameters, ResourceUriLinkType.PreviousPage) : null;
            var nextLink = companiesFromRepo.HasNext ? CreateLink(companiesResourceParameters,
                ResourceUriLinkType.NextPage) : null;

            var metadataObj = new
            {
                pageSize = companiesFromRepo.PageSize,
                pageNo = companiesFromRepo.CurrentPage,
                previousLink,
                nextLink,
                totalCount = companiesFromRepo.Count,
                totalPages = companiesFromRepo.TotalPages
            };

            Response.Headers.Add("x-pagination", JsonSerializer.Serialize(metadataObj));
            return Ok(mapper.Map<IEnumerable<CompanyDto>>(companiesFromRepo));
        }

        [HttpGet("{companyId}", Name = "GetCompany")]
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
           new { companyId = companyToReturn.Id },
            companyToReturn);
        }

        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allows", "Get,Options,Post");
            return Ok();
        }

        private string CreateLink(CompaniesResourceParameters companiesResourceParameters,
            ResourceUriLinkType resourceUriLinkType)
        {
            switch (resourceUriLinkType)
            {
                case ResourceUriLinkType.NextPage:
                    return Url.Link("GetCompanies",
                        new CompaniesResourceParameters
                        {
                            PageSize = companiesResourceParameters.PageSize,
                            PageNumber = companiesResourceParameters.PageNumber + 1,
                            SearchQuery = companiesResourceParameters.SearchQuery
                        });
                case ResourceUriLinkType.PreviousPage:
                    return Url.Link("GetCompanies", new CompaniesResourceParameters()
                    {
                        PageSize = companiesResourceParameters.PageSize,
                        PageNumber = companiesResourceParameters.PageNumber - 1,
                        SearchQuery = companiesResourceParameters.SearchQuery
                    });
                default:
                    return Url.Link("GetCompanies", companiesResourceParameters);
            }
        }
    }
}
