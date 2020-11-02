using System;
using System.Collections.Generic;

namespace CQRS.NET.Autofac.Tests.TestObjects {
    public class SimpleResult {
        public CallChain CallChain { get; set; } = new CallChain();
        public Type GeneratedBy { get; internal set; }
    }
}