using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using WcfExamples.Web.Code;

[assembly: PreApplicationStartMethod(typeof(HttpModuleRegistration), "Start")]

namespace WcfExamples.Web.Code
{

    public class HttpModuleRegistration
    {
        public static void Start()
        {
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof (WcfModule));
        } 
    }

    public class WcfModule : IHttpModule
    {

        private static readonly Dictionary<Type, ChannelFactory> _channelFactories =
            new Dictionary<Type, ChannelFactory>();

        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.EndRequest += (sender, arg) => CloseWcfClientChannels();
        }

        #endregion

        public T CreateProxy<T>() where T : class
        {
            //Create a channel and add to the current context, so we can close it later.
            List<ICommunicationObject> listOfServices = GetListOfServices();
            ChannelFactory<T> factory = GetChannelFactory<T>();
            T channel = factory.CreateChannel();
            listOfServices.Add((ICommunicationObject) channel);
            return channel;
        }

        private ChannelFactory<T> GetChannelFactory<T>()
        {
            if (!_channelFactories.ContainsKey(typeof (T)))
            {
                lock (_channelFactories)
                {
                    if (!_channelFactories.ContainsKey(typeof (T)))
                        _channelFactories.Add(typeof (T), new ChannelFactory<T>(typeof (T).Name));
                }
            }
            return (ChannelFactory<T>) _channelFactories[typeof (T)];
        }

        private List<ICommunicationObject> GetListOfServices()
        {
            var listOfServices = HttpContext.Current.Items["__wcf_services__"] as List<ICommunicationObject>;
            if (listOfServices == null)
            {
                listOfServices = new List<ICommunicationObject>();
                HttpContext.Current.Items.Add("__wcf_services__", listOfServices);
            }
            return listOfServices;
        }

        private void CloseWcfClientChannels()
        {
            List<ICommunicationObject> listOfServices = GetListOfServices();
            if (listOfServices != null)
            {
                foreach (ICommunicationObject service in listOfServices)
                {
                    try
                    {
                        service.Close();
                    }
                    catch (CommunicationObjectFaultedException)
                    {
                        service.Abort();
                        throw;
                    }
                }
            }
        }

    }
}