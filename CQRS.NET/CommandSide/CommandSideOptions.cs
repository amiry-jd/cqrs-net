using System.Collections.Generic;

namespace CQRS.NET {
    public class CommandSideOptions {

        public ICollection<DecoratorOption> GenericDecorators { get; } = new List<DecoratorOption>();

    }
}