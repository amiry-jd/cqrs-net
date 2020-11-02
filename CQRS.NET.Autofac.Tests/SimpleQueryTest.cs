using System.Linq;
using CQRS.NET.Autofac.Tests.TestObjects;
using Xunit;

namespace CQRS.NET.Autofac.Tests {

    public class SimpleQueryTest : IClassFixture<SimpleQueryFixture> {

        private readonly SimpleQueryFixture _fixture;

        public SimpleQueryTest(SimpleQueryFixture fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void It_Should_Be_Handled_By_SimpleQueryHandler() {
            Assert.Equal(typeof(SimpleQueryHandler), _fixture.Query.HandledBy);
            Assert.Equal(typeof(SimpleQueryHandler), _fixture.Result.GeneratedBy);
        }

        [Fact]
        public void It_Should_Be_Validated() {
            Assert.Contains(typeof(SimpleQueryValidator), _fixture.Query.CallChain);
        }

        [Fact]
        public void It_Should_Be_Validated_Exactly_Before_Being_Handled() {
            var callChain = _fixture.Query.CallChain.ToList();
            var handlerIndex = callChain.FindIndex(t => t == typeof(SimpleQueryHandler));
            var validatorIndex = handlerIndex - 1;
            var validator = callChain[validatorIndex];
            Assert.Equal(typeof(SimpleQueryValidator), validator);
        }
    }
}
