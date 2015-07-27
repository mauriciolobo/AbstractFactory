
using FluentAssertions;
using NUnit.Framework;

namespace AttributeFactory.Tests
{
    [TestFixture]
    public class CreatingNewClassTests
    {
        [Test]
        public void ShouldCreateBasicClass()
        {
            var factoryName = "Basic";

            var result = AttributeFactory.Create(factoryName);

            result.Should().BeAssignableTo<BasicClass>();
        }

        [Test]
        public void ShouldCreateAdvancedClass()
        {
            var factoryName = "Advanced";

            var result = AttributeFactory.Create(factoryName);

            result.Should().BeAssignableTo<AdvancedClass>();
        }        
    }

    [AbstractFactory("Basic")]
    public class BasicClass
    {

    }

    [AbstractFactory("Advanced")]
    public class AdvancedClass
    {

    }    
}
