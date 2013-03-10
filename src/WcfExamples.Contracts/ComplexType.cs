using System;
using System.Runtime.Serialization;

namespace WcfExamples.Contracts
{
    [DataContract]
    public class ComplexType
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}