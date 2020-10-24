using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.DbContexts;
using Sample.Entities;
using Sample.Models;
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

        public IEnumerable<Company> GetCompanies(CompaniesResourceParameters companiesResourceParameters)
        {
            if(companiesResourceParameters==null)
                throw new ArgumentNullException(nameof(CompaniesResourceParameters));
            if (string.IsNullOrEmpty(companiesResourceParameters.SearchQuery))
                return GetCompanies();

            var collection = context.Companies as IQueryable<Company>;

            var searchQuery = companiesResourceParameters.SearchQuery.Trim();
            collection = collection.Where(i => i.Name.Contains(searchQuery)
            || i.Activity.Contains(searchQuery)   );


            return collection.ToList();
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

        public bool Save()
        {
            return context.SaveChanges()>=0;
        }
    }
}
