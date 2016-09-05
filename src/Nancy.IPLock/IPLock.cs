using System;
using Nancy.Bootstrapper;

namespace Nancy.IPLock
{
    public static class IPLock
    {
        public static void Enable(IPipelines pipelines, IPLockConfiguration configuration)
        {
            if (pipelines == null)
            {
                throw new ArgumentNullException("pipelines");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            pipelines.BeforeRequest.AddItemToStartOfPipeline(GetIPCheckHook(configuration));
        }

        private static Func<NancyContext, Response> GetIPCheckHook(IPLockConfiguration configuration)
        {
            return context => !configuration.IPValidator.IsValid(context.Request.UserHostAddress)
                ? (Response) HttpStatusCode.Forbidden
                : null;
        }
    }
}
