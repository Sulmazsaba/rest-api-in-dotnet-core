using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sample.Entities;
using Sample.Models;
using Sample.Services;

namespace Sample.Controllers
{
    [Route("api/companies/{companyId}/job-positions")]
    [ApiController]
    public class JobPositionsController : ControllerBase
    {
        private IJobRepository jobRepository;
        private IMapper mapper;

        public JobPositionsController(IJobRepository jobRepository, IMapper mapper)
        {
            this.jobRepository = jobRepository?? throw new ArgumentNullException(nameof(jobRepository));
            this.mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<JobPositionDto>> GetJobPositionsForCompany(Guid companyId)
        {
            if (!jobRepository.CompanyExists(companyId))
                return NotFound();
            var jobPositionsFromRepo = jobRepository.GetJobPositions(companyId);
            return Ok(mapper.Map<IEnumerable<JobPositionDto>>(jobPositionsFromRepo));
        }

        [HttpGet("{jobPositionId}", Name = "GetJobPositionForCompany")]
        public ActionResult<JobPositionDto> GetJobPositionForCompany(Guid companyId,Guid jobPositionId)
        {
            if (!jobRepository.CompanyExists(companyId))
                return NotFound();
            var jobPositionFromRepo = jobRepository.GetJobPosition(companyId, jobPositionId);
            if (jobPositionFromRepo == null)
                return NotFound();
            return Ok(mapper.Map<JobPositionDto>(jobPositionFromRepo));
        }

        [HttpPost]
        public ActionResult<JobPositionDto> CreateJobPositionForCompany(Guid companyId,JobPositionForCreationDto dto)
        {
            if (!jobRepository.CompanyExists(companyId))
                return NotFound();

            var entity = mapper.Map<JobPosition>(dto);
            jobRepository.AddJobPositionForCompany(companyId,entity);
            jobRepository.Save();

            var jobPositionDto = mapper.Map<JobPositionDto>(entity);

            return CreatedAtRoute("GetJobPositionForCompany",
                new {companyId = jobPositionDto.CompanyId, jobPositionId = jobPositionDto.Id},
                jobPositionDto);
        }

        [HttpPut("{jobPositionId}")]
        public IActionResult UpdateJobPositionForCompany(Guid companyId, Guid jobPositionId
            ,JobPositionForUpdateDto jobPositionForUpdateDto)
        {
            if (!jobRepository.CompanyExists(companyId))
                return NotFound();

            var jobPositionFromRepo= jobRepository.GetJobPosition(companyId, jobPositionId);
            if (jobPositionFromRepo == null)
            {
                var jobPositionToAdd = mapper.Map<JobPosition>(jobPositionForUpdateDto);
                jobRepository.AddJobPositionForCompany(companyId,jobPositionToAdd);
                jobRepository.Save();
                var returnDto = mapper.Map<JobPositionDto>(jobPositionToAdd);
                return CreatedAtRoute("GetJobPositionForCompany",
                    routeValues: new
                    {
                        companyId = returnDto.CompanyId,
                        jobPositionId = returnDto.Id
                    }, returnDto);

            }

              mapper.Map(jobPositionForUpdateDto, jobPositionFromRepo);
             jobRepository.UpdateJobPosition(jobPositionFromRepo);
             jobRepository.Save();

             return NoContent();

        }

        [HttpDelete("{jobPositionId}")]
        public ActionResult Delete(Guid companyId, Guid jobPositionId)
        {
            if (!jobRepository.CompanyExists(companyId))
                return NotFound();

            var jobPositionFromRepo = jobRepository.GetJobPosition(companyId, jobPositionId);
            if (jobPositionFromRepo == null)
                return NotFound();

            jobRepository.DeleteJobPosition(jobPositionFromRepo);
            jobRepository.Save();

            return NoContent();
        }

        [HttpPatch("{jobPositionId}")]
        public ActionResult UpdatePartiallyJobPosition(Guid companyId,Guid jobPositionId,
            JsonPatchDocument<JobPositionForUpdateDto> document
        )
        {
            if (!jobRepository.CompanyExists(companyId))
                return NotFound();

            var jobPositionFromRepo = jobRepository.GetJobPosition(companyId, jobPositionId);
            if (jobPositionFromRepo == null)
            {
               var jobPositionDto=new JobPositionForUpdateDto();
               document.ApplyTo(jobPositionDto);

               if (!TryValidateModel(jobPositionDto))
                   return ValidationProblem(ModelState);
               var entity = mapper.Map<JobPosition>(jobPositionDto);
               entity.Id = jobPositionId;
               jobRepository.AddJobPositionForCompany(companyId,entity);
               jobRepository.Save();

               var dtoToReturn = mapper.Map<JobPositionDto>(entity);
               return CreatedAtRoute("GetJobPositionForCompany",
               new {
                   companyId=companyId,
                       jobPositionId=dtoToReturn.Id
               },dtoToReturn);

            }

            var dto = mapper.Map<JobPositionForUpdateDto>(jobPositionFromRepo);
            document.ApplyTo(dto,ModelState);

            if (!TryValidateModel(dto))
            {
                return ValidationProblem(ModelState);
            }
            mapper.Map(dto,jobPositionFromRepo);

            jobRepository.UpdateJobPosition(jobPositionFromRepo);
            jobRepository.Save();

            return NoContent();
        }

        public override ActionResult ValidationProblem(ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult) options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
