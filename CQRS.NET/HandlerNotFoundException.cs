using System;

namespace CQRS.NET {
    public sealed class HandlerNotFoundException : Exception {

        public Type RequestedType { get; }
        public Type ServiceType { get; }

        public HandlerNotFoundException(Type requestedType, Type serviceType) {
            RequestedType = requestedType;
            ServiceType = serviceType;
        }

    }
}