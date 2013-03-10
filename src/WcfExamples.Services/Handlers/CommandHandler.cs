using WcfExamples.Contracts;

namespace WcfExamples.Services.Handlers
{
    public interface ICommandHandler
    {
        BaseCommandResponse Execute(BaseCommandRequest request);
    }

    public abstract class BaseCommandHandler<TRequest> : ICommandHandler where TRequest:BaseCommandRequest
    {
        public BaseCommandResponse Execute(BaseCommandRequest request)
        {
            return ExecuteRequest((TRequest) request);
        }

        protected abstract BaseCommandResponse ExecuteRequest(TRequest request);
    }
}