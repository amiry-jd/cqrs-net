using System.Threading.Tasks;

namespace CQRS.NET.Autofac.Tests.TestObjects {
    public class SimpleQueryHandler : IQueryHandler<SimpleQuery, SimpleResult> {

        public async Task<SimpleResult> HandleAsync(SimpleQuery query) {
            query.HandledBy = typeof(SimpleQueryHandler);
            query.CallChain.Add(this);
            var result = new SimpleResult {
                GeneratedBy = typeof(SimpleQueryHandler)
            };
            result.CallChain.Add(this);
            return await Task.FromResult(result);
        }

    }
}