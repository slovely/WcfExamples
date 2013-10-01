using System;
using System.Runtime.Caching;

namespace WcfExamples.Web.Code
{
    public interface IAopExampleService
    {
        DateTime GetCurrentTime();
    }

    public class AopExampleService : IAopExampleService
    {

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

    }

    public class CachedAopService : IAopExampleService
    {
        private readonly IAopExampleService _theRealService;

        public CachedAopService(IAopExampleService theRealService)
        {
            _theRealService = theRealService;
        }

        public DateTime GetCurrentTime()
        {
            var time = MemoryCache.Default.Get("TheTime") as DateTime?;
            if (time == null)
            {
                time = _theRealService.GetCurrentTime();
                MemoryCache.Default.Add("TheTime", time, DateTimeOffset.Now.AddSeconds(5));
            }

            return time.Value;
        }

    }
}