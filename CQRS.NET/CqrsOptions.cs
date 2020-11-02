using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CQRS.NET {
    public class CqrsOptions {

        [NotNull]
        public Func<Assembly[]> AssembliesLoader { get; set; }

        public QuerySideOptions QuerySide { get; } = new QuerySideOptions();
        public CommandSideOptions CommandSide { get; } = new CommandSideOptions();

    }
}