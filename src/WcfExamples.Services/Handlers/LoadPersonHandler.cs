using WcfExamples.Contracts;

namespace WcfExamples.Services.Handlers
{
    public class LoadPersonHandler : BaseCommandHandler<LoadPersonRequest>
    {
        protected override BaseCommandResponse ExecuteRequest(LoadPersonRequest request)
        {
            //load person from database, file, whatever...
            return new LoadPersonResponse {Age = 18, Id = request.PersonId, Name = "Joe Bloggs"};
        }
    }
}