using System.Net.Http;
using Goodboy.Http.Client.Configuration;
using Polly;
using Polly.Extensions.Http;

namespace Goodboy.Http.Client.Policies
{
    public static class CircuitBreakerPolicyHandler
    {
        public static IAsyncPolicy<HttpResponseMessage> Break(CircuitBreakerPolicyOptions circuitBreakerConfiguration) =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<HttpRequestException>()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: circuitBreakerConfiguration.ExceptionsAllowedBeforeBreaking,
                    durationOfBreak: circuitBreakerConfiguration.BreakDuration
                );
    }
}