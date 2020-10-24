using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Entities;

namespace Sample.Services
{
   public interface IJobRepository
   {
       IEnumerable<Company> GetCompanies();
       Company GetCompany(Guid companyId);
   }
}
