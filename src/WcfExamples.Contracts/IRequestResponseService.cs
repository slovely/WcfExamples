using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfExamples.Contracts
{
    [ServiceContract]
    public interface IRequestResponseService
    {
        [OperationContract]
        BaseCommandResponse ExecuteCommand(BaseCommandRequest request);
    }

    [DataContract]
    [KnownType("GetKnownTypes")]
    public abstract class BaseCommandRequest
    {
        public static Type[] GetKnownTypes()
        {
            var types = from t in typeof(BaseCommandRequest).Assembly.GetTypes()
                        where typeof (BaseCommandRequest).IsAssignableFrom(t) && !t.IsAbstract
                        select t;
            return types.ToArray();
        }
    }

    [DataContract]
    [KnownType("GetKnownTypes")]
    public abstract class BaseCommandResponse
    {
        public static Type[] GetKnownTypes()
        {
            var types = from t in typeof(BaseCommandResponse).Assembly.GetTypes()
                        where typeof(BaseCommandResponse).IsAssignableFrom(t) && !t.IsAbstract
                        select t;
            return types.ToArray();
        }
    }

    [DataContract]
    public class LoadPersonRequest : BaseCommandRequest
    {
        [DataMember]
        public Guid PersonId { get; set; }
    }

    [DataContract]
    public class LoadPersonResponse : BaseCommandResponse
    {
        [DataMember]
        public Guid Id { get; set; }
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public int Age { get; set; }
    }
}