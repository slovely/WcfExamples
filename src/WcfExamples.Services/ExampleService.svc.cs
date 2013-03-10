using System;
using WcfExamples.Contracts;

namespace WcfExamples.Services
{
    public class ExampleService : IExampleService
    {
        public string SayHello(string name)
        {
            return "Hi there, " + name;
        }

        public ComplexType MethodThatReturnsComplexType()
        {
            return new ComplexType
                {
                    Name = "Current Time",
                    Date = DateTime.Now,
                };
        }
    }
}
