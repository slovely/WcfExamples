using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfExamples.Contracts;

namespace WcfExamples.Services
{
    public class DatabaseService : IDatabaseService
    {
        public Person GetObjectFromDatabase(int id)
        {
            throw new NotImplementedException();
        }
    }
}
