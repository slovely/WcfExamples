using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfExamples.Contracts
{
    [ServiceContract]
    public interface IDatabaseService
    {
        [OperationContract]
        Person GetObjectFromDatabase(int id);
    }

    [DataContract]
    public class Person
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }
    }
}