using Microsoft.Extensions.DependencyInjection;

using CQRS.NET.Autofac.Tests.TestObjects;

namespace CQRS.NET.Autofac.Tests {

    public class SimpleQueryFixture {

        public SimpleQueryFixture() {
            var sc = new ServiceCollection();

            sc.RegisterCqrs(new[] { GetType().Assembly });

            Container = sc.BuildServiceProvider();

            Executer = Container.GetService<IHandlerExecutor>()!;
           // Query    = new SimpleQuery();
           // Result   = Executer.ExecuteAsync(Query).GetAwaiter().GetResult();
        }

        public IServiceProvider Container { get; }
        public IHandlerExecutor Executer  { get; }
        public SimpleQuery      Query     { get; }
        public SimpleResult     Result    { get; }

    }

}