﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Entities;
using Sample.Helpers;
using Sample.Models;
using Sample.ResourceParameters;

namespace Sample.Services
{
   public interface IJobRepository
   {
       IEnumerable<Company> GetCompanies();
       Company GetCompany(Guid companyId);
       PagedList<Company> GetCompanies(CompaniesResourceParameters companiesResourceParameters);
       void AddCompany(Company company);
       bool CompanyExists(Guid companyId);
       IEnumerable<JobPosition> GetJobPositions(Guid companyId);
       JobPosition GetJobPosition(Guid companyId,Guid jobPositionId);
       void AddJobPositionForCompany(Guid companyId,JobPosition jobPosition);
       void UpdateJobPosition(JobPosition jobPosition);
        void DeleteJobPosition(JobPosition jobPosition);
        void AddJobPosition(JobPosition jobPosition);
        IEnumerable<Company> GetCompanies(IEnumerable<Guid> ids);
       bool Save();

   }
}
