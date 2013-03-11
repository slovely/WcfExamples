using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfExamples.Web.WcfHelper
{
    public static class CallWcfService<TServiceType>
    {
        public static TResult Execute<TResult>(Func<TServiceType, TResult> func)
        {
            var factory = (ChannelFactory<TServiceType>)WcfServiceChannelCache.ChannelFactories.GetOrAdd(typeof (TServiceType), type => new ChannelFactory<TServiceType>(type.Name));
            var channel = factory.CreateChannel();
            try
            {
                var result = func(channel);
                ((IClientChannel) channel).Close();
                return result;
            }
            catch (FaultException)
            {
                ((IClientChannel)channel).Abort();
                throw;
            }
        }
    }

    internal class WcfServiceChannelCache
    {
        //THIS IS NOT HOW THINGS SHOULD BE CACHED!!!
        public static ConcurrentDictionary<Type, IChannelFactory> ChannelFactories = new ConcurrentDictionary<Type, IChannelFactory>();
    }
}