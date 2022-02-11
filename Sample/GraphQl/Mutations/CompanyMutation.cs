using AutoMapper;
using GraphQL;
using GraphQL.Types;
using Sample.Entities;
using Sample.GraphQl.Types;
using Sample.Models;
using Sample.Services;

namespace Sample.GraphQl.Mutations
{
    public class CompanyMutation : ObjectGraphType
    {
        public CompanyMutation(IJobRepository jobRepository, IMapper mapper)
        {
            Field<CompanyType>("addCompany",
                "Is used to add a new company to the database",
                arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<CompanyInputType>>()
                {
                    Name = "company",
                    Description = "company input parameter"
                }), resolve: context =>
                {
                    var company = context.GetArgument<Company>("company");
                    jobRepository.AddCompany(company);

                    jobRepository.Save();
                    return company;
                });

        }
    }
}
