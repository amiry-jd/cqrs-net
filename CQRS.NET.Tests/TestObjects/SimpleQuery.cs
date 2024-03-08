using System;
using System.Collections.Generic;

namespace CQRS.NET.Autofac.Tests.TestObjects {
    public class SimpleQuery : IQuery<SimpleResult> {
        public CallChain CallChain { get; set; } = new CallChain();
        public Type HandledBy { get; internal set; }
    }

}