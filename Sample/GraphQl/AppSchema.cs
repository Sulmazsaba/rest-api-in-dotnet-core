using GraphQL.Types;
using Sample.GraphQl.Mutations;
using Sample.GraphQl.Queries;

namespace Sample.GraphQl
{
    public class AppSchema : Schema
    {
        public AppSchema(CompanyQuery query, CompanyMutation mutation)
        {
            this.Query = query;
            this.Mutation = mutation;   
        }
    }
}
