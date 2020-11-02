using System;
using System.Collections;
using System.Collections.Generic;

namespace CQRS.NET.Autofac.Tests.TestObjects {
    public class CallChain : IEnumerable<Type> {
        private readonly List<Type> _types = new List<Type>();

        public void Add(object @this) => _types.Add(@this.GetType());

        public IEnumerator<Type> GetEnumerator() => _types.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _types.GetEnumerator();
    }
}