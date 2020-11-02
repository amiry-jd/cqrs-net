using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CQRS.NET {
    public class ValidatorAttribute : Attribute {
        public Type[] ValidatorTypes { get; }

        public ValidatorAttribute([NotNull] Type validatorType, params Type[] otherValidatorTypes) {
            ValidatorTypes = new[] { validatorType }.Union(otherValidatorTypes).ToArray();
        }

    }
}