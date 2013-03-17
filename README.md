WcfExamples
===========

_**NOTE:**_ This repository is here to demonstrate a very specific example, please don't think it is in anyway representative of best practises or worthy of production use!  Static methods, no IoC, no tests, WebForms are just some of the bad things in this repo!

Modifying an existing service
----
 - Modify the service interface in the Contracts assembly, adding any types required.
 - Update the service implementation.
 - Recompile and go!

Adding a new service
-------------------

 - Create an interface in the 'Contracts' assembly, marked up with the usual` [ServiceContract]` and `[OperationContract]` attributes.
 - Add a service file to the 'Services' assembly and write an implementation for the interface above.
 - Add the endpoint to the web.config in the client website - the endpoint name *must* be the name of the service interface.
 - Compile and your service is good to go.
 
Using a service
----

To call a service method, use the CallWcfMethod helper class, like this:

    var result = CallWcfService<IMyServiceInterface>.Execute(svc => svc.MyServiceMethod("param1", "param2"));
    
Using the Request/Response sample
----
This repository also contains a very simple/naive implementation of a request/response pattern.  This allows having just one service that handles everything you need.  To add a new service method you create a Request class containing any parameters required, a Response class containing the result and then a Handler class that actually does the work.  As long as you derive from the correct types, it'll all just work automagically.

**Example Request Class**

    [DataContract]
    public class LoadPersonRequest : BaseCommandRequest
    {
        [DataMember]
        public Guid PersonId { get; set; }
    }
    
**Example Response Class**
    
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
    
**Example Handler class**
    
    public class LoadPersonHandler : BaseCommandHandler<LoadPersonRequest>
    {
        protected override BaseCommandResponse ExecuteRequest(LoadPersonRequest request)
        {
            //load person from database, file, whatever...
            return new LoadPersonResponse {Age = 18, Id = request.PersonId, Name = "Joe Bloggs"};
        }
    }

**To use this service**

    var response = (LoadPersonResponse)CallWcfService<IRequestResponseService>.Execute(
        svc => svc.ExecuteCommand(new LoadPersonRequest {PersonId = personID));
        
_NOTE_: In reality you'd want to hide the cast to `LoadPersonResponse` which hasn't been implemented yet.

**MVC**

This example now includes a couple of MVC controllers that return data and HTML.  Also added StructureMap as an IoC container to allow WCF services to be injected into the controllers automatically (better than using the CallWcfService above).  Need to create a structuremap convention to get them wired up automatically though.  See the demo.js file for the AJAX details.

Should really change the MVC folder location though, so that it's separated from the WebForms stuff (i.e. move the Controllers/Views/Models folders into a folder called 'mvc' or something).


