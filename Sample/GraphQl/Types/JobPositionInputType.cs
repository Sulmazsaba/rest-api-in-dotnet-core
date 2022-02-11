using GraphQL.Types;

namespace Sample.GraphQl.Types
{
    public class JobPositionInputType : InputObjectGraphType
    {
        public JobPositionInputType()
        {
            Name = "JobPositionInputType";

            Field<StringGraphType>("Description");
            Field<StringGraphType>("Title");
            //Field<EnumerationGraphType>("Degree");
        }
    }
}
