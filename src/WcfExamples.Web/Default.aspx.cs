using System;
using System.Web.UI;
using WcfExamples.Contracts;
using WcfExamples.Web.WcfHelper;

namespace WcfExamples.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtResult.Text = CallWcfService<IExampleService>.Execute(svc => svc.SayHello("Dave"));

            var obj = CallWcfService<IExampleService>.Execute(svc => svc.MethodThatReturnsComplexType());
            txtComplex.Text = obj.Name + ": " + obj.Date;
        }
    }
}