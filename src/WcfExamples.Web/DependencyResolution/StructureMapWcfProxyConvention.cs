using System;
using System.Reflection;
using System.ServiceModel;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using WcfExamples.Web.Code;

namespace WcfExamples.Web.DependencyResolution
{
    public class StructureMapWcfProxyConvention : IRegistrationConvention
    {
        private static MethodInfo _method;

        static StructureMapWcfProxyConvention()
        {
            _method = typeof (WcfModule).GetMethod("CreateProxy");
            
        }

        public void Process(Type type, Registry registry)
        {
            if (type.IsDefined(typeof (ServiceContractAttribute), false))
            {
                var closedGenericMethod = _method.MakeGenericMethod(type);
                var module = new WcfModule();
                registry.For(type).Use(x => closedGenericMethod.Invoke(module, new object[0]));
            }
        }
    }
}