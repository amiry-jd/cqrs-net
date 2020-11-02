using Autofac;
using CQRS.NET.Autofac.Tests.TestObjects;

namespace CQRS.NET.Autofac.Tests {
    public class SimpleQueryFixture {
        public SimpleQueryFixture() {
            var builder = new ContainerBuilder();

            builder.RegisterCqrs(new[] { GetType().Assembly });

            Container = builder.Build();

            Executer = Container.Resolve<IHandlerExecutor>();
            Query = new SimpleQuery();
            Result = Executer.ExecuteAsync(Query).GetAwaiter().GetResult();
        }

        public IContainer Container { get; }
        public IHandlerExecutor Executer { get; }
        public SimpleQuery Query { get; }
        public SimpleResult Result { get; }
    }
}
