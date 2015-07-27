using System;
using System.Collections.Generic;

namespace AttributeFactory
{
    public static class AttributeFactory
    {
        public static object Create(string factoryName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    var customAttributes =
                        type.GetCustomAttributes(typeof(AbstractFactoryAttribute), false);
                    foreach (AbstractFactoryAttribute abstractFactoryAttribute in customAttributes)
                    {
                        if (abstractFactoryAttribute.ClassName == factoryName) return Activator.CreateInstance(type);
                    }                    
                }
            }
            throw new Exception("Cannot find class");
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class AbstractFactoryAttribute : Attribute
    {
        public string ClassName { get; private set; }

        public AbstractFactoryAttribute(string className)
        {
            ClassName = className;
        }
    }
}