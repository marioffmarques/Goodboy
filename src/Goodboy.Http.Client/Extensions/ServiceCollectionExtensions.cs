using System;
using System.Net;
using System.Net.Http.Headers;
using Goodboy.Http.Client.Configuration;
using Goodboy.Http.Client.Policies;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Goodboy.Http.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configure Json HttpClient.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceConfiguration"></param>
        /// <param name="policyConfiguration"></param>
        /// <typeparam name="TClient"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns>Configured IHttpClientBuilder</returns>
        public static IHttpClientBuilder ConfigureJsonHttpClient<TClient, TImplementation>(this IServiceCollection services, HttpServiceConfiguration serviceConfiguration, PolicyConfiguration policyConfiguration = default)
            where TClient : class
            where TImplementation : class, TClient
        {
            if (serviceConfiguration is null)
            {
                throw new ArgumentNullException(nameof(serviceConfiguration));
            }

            if (policyConfiguration is null)
            {
                throw new ArgumentNullException(nameof(policyConfiguration));
            }

            return services.AddHttpClient<TClient, TImplementation>(client => ConfigureBaseJsonHttpClient(client, serviceConfiguration))
                .SetupHandlersAndPolicies(policyConfiguration);
        }

        private static void ConfigureBaseJsonHttpClient(System.Net.Http.HttpClient client, HttpServiceConfiguration config)
        {
            client.BaseAddress = config.Url;
            client.Timeout = config.RequestTimeout;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        }

        private static IHttpClientBuilder SetupHandlersAndPolicies(this IHttpClientBuilder clientBuilder, PolicyConfiguration policyConfiguration)
        {
            clientBuilder
                //.ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler(x.GetRequiredService<ISOAPMessageLogger>()))
                .SetHandlerLifetime(policyConfiguration.HttpClientRenewalInterval)
                .AddPolicyHandler(
                    RetryPolicyHandler.WaitAndRetry(policyConfiguration.HttpRetry)
                )
                .AddPolicyHandler(
                    CircuitBreakerPolicyHandler.Break(policyConfiguration.HttpCircuitBreaker)
                );

            return clientBuilder;
        }
    }
}