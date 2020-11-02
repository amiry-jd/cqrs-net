using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CQRS.NET {
    public sealed class SkipDecoratorAttribute : Attribute {

        public SkipDecoratorAttribute([NotNull] Type decoratorType, params Type[] otherDecoratorTypes) {
            DecoratorTypes = new[] { decoratorType }.Union(otherDecoratorTypes).ToArray();
        }

        public Type[] DecoratorTypes { get; }

    }
}