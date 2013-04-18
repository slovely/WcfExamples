using System;
using System.ServiceModel;

namespace WcfExamples.Web.Code
{
    public static class CallWcfService<TServiceType>
    {
        private static volatile ChannelFactory<TServiceType> _channelFactoryForTServiceType;
        private static object _objectSync = new object();

        public static TResult Execute<TResult>(Func<TServiceType, TResult> func)
        {
            EnsureChannelFactory();
            var channel = _channelFactoryForTServiceType.CreateChannel();
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

        private static void EnsureChannelFactory()
        {
            if (_channelFactoryForTServiceType == null)
            {
                lock (_objectSync)
                {
                    if (_channelFactoryForTServiceType == null)
                    {
                        _channelFactoryForTServiceType = new ChannelFactory<TServiceType>(typeof (TServiceType).Name);
                    }
                }
            }
        }
    }
}