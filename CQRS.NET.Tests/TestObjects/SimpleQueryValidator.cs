using System.Threading.Tasks;

namespace CQRS.NET.Autofac.Tests.TestObjects {
    public class SimpleQueryValidator : IValidator<SimpleQuery> {

        public async Task ValidateAsync(SimpleQuery validatable) {
            validatable.CallChain.Add(this);
            await Task.CompletedTask;
        }

    }

}