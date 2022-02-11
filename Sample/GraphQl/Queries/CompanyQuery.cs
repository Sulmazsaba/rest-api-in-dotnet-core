using GraphQL;
using GraphQL.Types;
using Sample.Entities;
using Sample.GraphQl.Types;
using Sample.Services;
using System;

namespace Sample.GraphQl.Queries
{
    public class CompanyQuery : ObjectGraphType
    {
        public CompanyQuery(IJobRepository repository)
        {
            Field<ListGraphType<CompanyType>>(
                "companies",
                "returns list of companies",
                resolve: context => repository.GetCompanies());

            Field<CompanyType>("company",
                "return single company by id",
                new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id", Description = "company Id" }),
                resolve: context => repository.GetCompany(context.GetArgument("id", Guid.NewGuid())));
        }
    }
}
