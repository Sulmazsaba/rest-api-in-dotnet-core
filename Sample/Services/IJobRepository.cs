using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Entities;
using Sample.Models;
using Sample.ResourceParameters;

namespace Sample.Services
{
   public interface IJobRepository
   {
       IEnumerable<Company> GetCompanies();
       Company GetCompany(Guid companyId);
       IEnumerable<Company> GetCompanies(CompaniesResourceParameters companiesResourceParameters);
       void AddCompany(Company company);
       bool CompanyExists(Guid companyId);
       IEnumerable<JobPosition> GetJobPositions(Guid companyId);
       JobPosition GetJobPosition(Guid companyId,Guid jobPositionId);
       void AddJobPosition(Guid companyId,JobPosition jobPosition);
       void UpdateJobPosition(JobPosition jobPosition);
       void DeleteJobPosition(JobPosition jobPosition);
       bool Save();

   }
}
