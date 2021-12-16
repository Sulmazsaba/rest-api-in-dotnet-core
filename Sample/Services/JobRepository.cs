using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.DbContexts;
using Sample.Entities;
using Sample.Helpers;
using Sample.Models;
using Sample.Profiles;
using Sample.ResourceParameters;

namespace Sample.Services
{
    public class JobRepository :IJobRepository
    {
        private SampleContext context;

        public JobRepository(SampleContext context)
        {
            this.context = context??throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Company> GetCompanies()
        {
            return context.Companies.ToList<Company>();
        }

        public Company GetCompany(Guid companyId)
        {
            if(companyId==Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));
            return context.Companies.FirstOrDefault(i=>i.Id==companyId);
        }

        public PagedList<Company> GetCompanies(CompaniesResourceParameters companiesResourceParameters)
        {
            if(companiesResourceParameters==null)
                throw new ArgumentNullException(nameof(CompaniesResourceParameters));
        

            var collection = context.Companies as IQueryable<Company>;

            if (!string.IsNullOrEmpty(companiesResourceParameters.SearchQuery))
            {
                var searchQuery = companiesResourceParameters.SearchQuery.Trim();
                collection = collection.Where(i => i.Name.Contains(searchQuery)
                                                   || i.Activity.Contains(searchQuery));
            }

            //if (!string.IsNullOrEmpty(companiesResourceParameters.OrderBy))
            //{
            //    collection.Sort<Company>(companiesResourceParameters.)
            //}

            return PagedList<Company>.Create(collection,companiesResourceParameters.PageSize,companiesResourceParameters.PageNumber);
        }

        public void AddCompany(Company company)
        {
            if(company==null)
                throw new ArgumentNullException(nameof(company));

            company.Id=Guid.NewGuid();
            foreach (var jobPosition in company.JobPositions)
            {
                jobPosition.Id=Guid.NewGuid();
            }
            context.Companies.Add(company);
        }

        public bool CompanyExists(Guid companyId)
        {
            if(companyId==Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));
            return context.Companies.Any(i => i.Id == companyId);
        }

        public IEnumerable<JobPosition> GetJobPositions(Guid companyId)
        {
            if(companyId==Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));

            return context.JobPositions.Where(i => i.CompanyId == companyId).ToList();
        }

        public JobPosition GetJobPosition(Guid companyId, Guid jobPositionId)
        {
            if(companyId == Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));
            if(jobPositionId==Guid.Empty)
                throw new ArgumentNullException(nameof(jobPositionId));

            return context.JobPositions.FirstOrDefault(i => i.CompanyId == companyId && i.Id == jobPositionId);
        }

        public void AddJobPositionForCompany(Guid companyId,JobPosition jobPosition)
        {
            if(jobPosition==null)
                throw new ArgumentNullException(nameof(JobPosition));
            
            if(companyId==Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));
            jobPosition.CompanyId = companyId;
            context.JobPositions.Add(jobPosition);
        }

        public void UpdateJobPosition(JobPosition jobPosition)
        {
            
        }

        public void DeleteJobPosition(JobPosition jobPosition)
        {
            context.JobPositions.Remove(jobPosition);
        }


        public IEnumerable<Company> GetCompanies(IEnumerable<Guid> ids)
        {
            if(ids==null)
                throw new ArgumentNullException(nameof(ids));

            return context.Companies.Where(i => ids.Contains(i.Id)).ToList();
        }

        public void AddJobPosition(JobPosition jobPosition)
        {
            context.JobPositions.Add(jobPosition);
        }

        public bool Save()
        {
            return context.SaveChanges()>=0;
        }
    }
}
