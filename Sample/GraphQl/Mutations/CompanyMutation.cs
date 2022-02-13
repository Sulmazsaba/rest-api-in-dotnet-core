using AutoMapper;
using GraphQL;
using GraphQL.Types;
using Sample.Entities;
using Sample.GraphQl.Types;
using Sample.Models;
using Sample.Services;
using System;

namespace Sample.GraphQl.Mutations
{
    public class CompanyMutation : ObjectGraphType
    {
        public CompanyMutation(IJobRepository repository)
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
                    repository.AddCompany(company);

                    repository.Save();
                    return company;
                });


            Field<JobPositionType>(
               "UpdateJobPosition",
               "Is used to update job position of specific company",
                arguments: new QueryArguments(
                  new QueryArgument<NonNullGraphType<GuidGraphType>>()
                  {
                      Name = "CompanyId",
                      Description = "Id of Company"

                  }
                  , new QueryArgument<NonNullGraphType<GuidGraphType>>()
                  {
                      Name = "JobPositionId",
                      Description = "Id of Job Position"

                  }
                  , new QueryArgument<NonNullGraphType<JobPositionInputType>>()
                  {
                      Name = "JobPosition",
                      Description = "Job Position Input Parameter"
                  })
                , resolve: context =>
                  {
                      var jobPosition = context.GetArgument<JobPosition>("JobPosition");
                      jobPosition.CompanyId = context.GetArgument<Guid>("CompanyId");
                      jobPosition.Id = context.GetArgument<Guid>("JobPositionId");

                      repository.UpdateJobPosition(jobPosition);

                      repository.Save();
                      return jobPosition;
                  });


            Field<JobPositionType>("DeleteJobPosition", 
                "Is used to delete job position of sepecif compapny",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<GuidGraphType>>()
                    {
                        Name = "CompanyId",
                        Description = "Id of Company"
                    }
                  , new QueryArgument<NonNullGraphType<GuidGraphType>>()
                  {
                      Name = "JobPositionId",
                      Description = "Id of Job Position"

                  }
                    ),resolve : context =>
                    {
                        var companyId = context.GetArgument<Guid>("CompanyId");
                        var jobPositionId = context.GetArgument<Guid>("JobPositionId");
                        var jobPositionFromRepo = repository.GetJobPosition(companyId, jobPositionId);

                        repository.DeleteJobPosition(jobPositionFromRepo);
                        repository.Save();
                        return jobPositionFromRepo;

                    }
                );


        }
    }
}
