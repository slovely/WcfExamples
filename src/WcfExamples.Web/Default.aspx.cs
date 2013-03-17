using System;
using System.Web.UI;
using WcfExamples.Contracts;
using WcfExamples.Web.Code;

namespace WcfExamples.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtResult.Text = CallWcfService<IExampleService>.Execute(svc => svc.SayHello("Dave"));

            var obj = CallWcfService<IExampleService>.Execute(svc => svc.MethodThatReturnsComplexType());
            txtComplex.Text = obj.Name + ": " + obj.Date;

            //Call a service by passing in a request (todo: hide the casts away)
            var response = (LoadPersonResponse)CallWcfService<IRequestResponseService>.Execute(svc => svc.ExecuteCommand(new LoadPersonRequest {PersonId = Guid.NewGuid()}));

            txtRequestResponseObj.Text = "Loaded person ID: " + response.Id;
        }
    }
}