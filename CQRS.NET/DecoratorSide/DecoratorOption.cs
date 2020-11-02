using System;

namespace CQRS.NET {
    public class DecoratorOption {

        public DecoratorOption(Type decoratorType, Type serviceType, RegisterGenericDecorator? condition = null) {
            DecoratorType = decoratorType;
            ServiceType = serviceType;
            Condition = condition;
        }

        public Type DecoratorType { get; }
        public Type ServiceType { get; }
        public RegisterGenericDecorator? Condition { get; }

    }
}