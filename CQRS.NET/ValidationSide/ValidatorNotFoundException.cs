using System;

namespace CQRS.NET {
    public sealed class ValidatorNotFoundException : Exception {

        public Type RequestedType { get; }
        public Type ValidatorType { get; }

        public ValidatorNotFoundException(Type requestedType, Type validatorType)
            : base($"Cannot find a validator class that implements {validatorType}.") {
            RequestedType = requestedType;
            ValidatorType = validatorType;
        }

        public ValidatorNotFoundException(Type requestedType, Type validatorType, string resolutionKey)
            : base($"Cannot find a validator class with resolution key of {resolutionKey} that implements {validatorType}.") {
            RequestedType = requestedType;
            ValidatorType = validatorType;
        }

    }
}