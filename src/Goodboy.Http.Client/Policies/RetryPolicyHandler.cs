using System;
using System.Net;
using System.Net.Http;
using Goodboy.Http.Client.Configuration;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Goodboy.Http.Client.Policies
{
    public static class RetryPolicyHandler
    {
        public static IAsyncPolicy<HttpResponseMessage> WaitAndRetry(RetryPolicyOptions retryPolicyOptions) =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(
                    retryPolicyOptions.Count,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(retryPolicyOptions.BackoffPower, retryAttempt))
                );
    }
}