using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Entities;
using Sample.ResourceParameters;

namespace Sample.Services
{
   public interface IJobRepository
   {
       IEnumerable<Company> GetCompanies();
       Company GetCompany(Guid companyId);
       IEnumerable<Company> GetCompanies(CompaniesResourceParameters companiesResourceParameters);
       void AddCompany(Company company);
       bool Save();

   }
}
