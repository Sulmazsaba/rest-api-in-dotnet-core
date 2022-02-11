using GraphQL.Types;
using Sample.GraphQl.Queries;

namespace Sample.GraphQl
{
    public class AppSchema : Schema
    {
        public AppSchema(CompanyQuery query)
        {
            this.Query = query;
        }
    }
}
