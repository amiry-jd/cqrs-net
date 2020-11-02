using System.Collections.Generic;

namespace CQRS.NET {
    public class QuerySideOptions {

        public ICollection<DecoratorOption> GenericDecorators { get; } = new List<DecoratorOption>();

    }
}