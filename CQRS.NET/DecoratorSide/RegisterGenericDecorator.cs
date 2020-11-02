using System;

namespace CQRS.NET {
    public delegate bool RegisterGenericDecorator(Type decoratorType, Type serviceType, Type implementationType);
}
