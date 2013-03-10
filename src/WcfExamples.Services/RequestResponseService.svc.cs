using System;
using System.Linq;
using WcfExamples.Contracts;
using WcfExamples.Services.Handlers;

namespace WcfExamples.Services
{
    public class RequestResponseService : IRequestResponseService
    {
        public BaseCommandResponse ExecuteCommand(BaseCommandRequest request)
        {
            var handler = FindHandler(request.GetType());
            return handler.Execute(request);
        }

        private ICommandHandler FindHandler(Type type)
        {
            //Logic shouldn't be hard-coded here.
            var handler =
                (from t in typeof (ICommandHandler).Assembly.GetTypes()
                where typeof (BaseCommandHandler<>).MakeGenericType(type).IsAssignableFrom(t)
                select t).SingleOrDefault();

            if (handler == null) throw new Exception("No handler for '" + type.Name + "' found.");

            return (ICommandHandler)Activator.CreateInstance(handler);
        }
    }
}
