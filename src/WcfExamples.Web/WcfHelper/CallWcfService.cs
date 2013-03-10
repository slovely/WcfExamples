using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfExamples.Web.WcfHelper
{
    public static class CallWcfService<TServiceType>
    {
        //TODO: Better caching...
        private static ConcurrentDictionary<Type, IChannelFactory> _channelFactories = new ConcurrentDictionary<Type, IChannelFactory>();

        public static TResult Execute<TResult>(Func<TServiceType, TResult> func)
        {
            var factory = (ChannelFactory<TServiceType>)_channelFactories.GetOrAdd(typeof (TServiceType), type => new ChannelFactory<TServiceType>(type.Name));
            var channel = factory.CreateChannel();
            return func(channel);
        }
    }
}