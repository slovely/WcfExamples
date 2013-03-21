using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfExamples.Contracts
{
    [ServiceContract]
    public interface IDatabaseService
    {
        [OperationContract]
        Person GetObjectFromDatabase(int id);

        [OperationContract]
        void SavePerson(Person person);

        [OperationContract]
        Tuple<List<Animal>, List<Person>> LoadPeopleAndAnimals();
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

    [DataContract]
    public class Animal
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Species { get; set; }
    }
}