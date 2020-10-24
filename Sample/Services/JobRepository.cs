using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.DbContexts;
using Sample.Entities;
using Sample.Models;

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
    }
}
