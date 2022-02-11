using GraphQL.Types;
using Sample.Entities;

namespace Sample.GraphQl.Types
{
    public class JobPositionType : ObjectGraphType<JobPosition>
    {
        public JobPositionType()
        {
            Field(x => x.Id, type: typeof(GuidGraphType)).Description("Id of Job Position Object");
            Field(x => x.CompanyId, type: typeof(GuidGraphType)).Description("Id of Company");
            //Field(x => x.Degree, type: typeof(EnumerationGraphType)).Description("Degree of job position");
            Field(x => x.Title, type: typeof(StringGraphType)).Description("Title of Job Position");
            Field(x => x.Description, type: typeof(StringGraphType)).Description("Description of Job Position");

        }
    }
}
