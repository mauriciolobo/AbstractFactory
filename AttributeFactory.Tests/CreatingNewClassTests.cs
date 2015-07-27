
using System;
using System.Collections.Generic;
using System.Linq;
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

            var result = Create(factoryName);

            result.Should().BeAssignableTo<BasicClass>();
        }

        [Test]
        public void ShouldCreateAdvancedClass()
        {
            var factoryName = "Advanced";

            var result = Create(factoryName);

            result.Should().BeAssignableTo<AdvancedClass>();
        }

        private object Create(string factoryName)
        {
            var typesWithMyAttribute =
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                from att in t.GetCustomAttributes(typeof(AbstractFactoryAttribute), false).Cast<AbstractFactoryAttribute>()
                where att.ClassName == factoryName
                select t;

            var withMyAttribute = typesWithMyAttribute as Type[] ?? typesWithMyAttribute.ToArray();
            if(withMyAttribute.Count()!=1)
                throw new Exception("Cannot find classe");

            var obj = Activator.CreateInstance(withMyAttribute.First());

            return obj;
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

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    internal sealed class AbstractFactoryAttribute : Attribute
    {
        public string ClassName { get; private set; }

        public AbstractFactoryAttribute(string className)
        {
            ClassName = className;
        }
    }
}
