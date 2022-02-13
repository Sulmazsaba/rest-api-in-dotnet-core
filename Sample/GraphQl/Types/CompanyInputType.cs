using GraphQL.Types;

namespace Sample.GraphQl.Types
{
    public class CompanyInputType : InputObjectGraphType
    {
        public CompanyInputType()
        {
            Name = "CompanyInputType";


            Field<StringGraphType>("Name");
            Field<StringGraphType>("Activity");
            Field<IntGraphType>("NumberOfStaff");
            Field<DateTimeOffsetGraphType>("DateTime");
            Field<ListGraphType<JobPositionInputType>>("JobPositions");
        }
    }
}
