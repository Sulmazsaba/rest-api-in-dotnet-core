using GraphQL.Types;
using Sample.Entities;

namespace Sample.GraphQl.Types
{
    public class CompanyType : ObjectGraphType<Company>
    {
        public CompanyType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the company object");
            Field(x => x.Name, type: typeof(StringGraphType)).Description("Name property from the company object");
            Field(x => x.NumberOfStaff, type: typeof(IntGraphType)).Description("Number of Staff peoperty from the company object");
            Field(x => x.Activity, type: typeof(StringGraphType)).Description("Activity property from the company object ");
            Field(x => x.DateTime, type: typeof(DateTimeOffsetGraphType)).Description("Date Time Property from the company object");
            Field(x => x.JobPositions, type: typeof(ListGraphType<JobPositionType>)).Description("Job Positions of Company");
        }
    }
}
