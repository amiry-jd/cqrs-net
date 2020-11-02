using System;

namespace CQRS.NET {
    public interface IStrategyDetector<in TInput> {
        Type FindHandlerType(TInput command);
    }
}