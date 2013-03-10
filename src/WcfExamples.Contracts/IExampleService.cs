using System.ServiceModel;

namespace WcfExamples.Contracts
{
    [ServiceContract]
    public interface IExampleService
    {
        [OperationContract]
        string SayHello(string name);

        [OperationContract]
        ComplexType MethodThatReturnsComplexType();
    }
}
