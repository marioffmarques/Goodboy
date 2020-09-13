using System;

namespace Goodboy.Http.Client.Configuration
{
    public class PolicyConfiguration
    {
        /// <summary>
        /// Gets or Sets a Circuit breaker configuration options.
        /// </summary>
        /// <value>Circuit breaker configuration options.</value>
        public CircuitBreakerPolicyOptions HttpCircuitBreaker { get; set; }

        /// <summary>
        /// Gets or Sets Retries configuration options.
        /// </summary>
        /// <value>Retry configuration.</value>
        public RetryPolicyOptions HttpRetry { get; set; }

        /// <summary>
        /// Gets or sets the Renewal time to renew underlying http client used to invoke the external http service.
        /// </summary>
        /// <returns>Renewal timestamp.</returns>
        public TimeSpan HttpClientRenewalInterval { get; set; } = TimeSpan.FromMinutes(5);
    }

    public static class PolicyName
    {
        /// <summary>
        /// CircuitBreaker policy name.
        /// </summary>
        /// <returns>CircuitBreaker name.</returns>
        public const string HttpCircuitBreaker = nameof(HttpCircuitBreaker);

        /// <summary>
        /// Retry policy name.
        /// </summary>
        /// <returns>Policy name.</returns>
        public const string HttpRetry = nameof(HttpRetry);
    }

    public class CircuitBreakerPolicyOptions
    {
        /// <summary>
        /// Gets or sets the amount of time a break will last.
        /// </summary>
        /// <returns>Break duration amount of time.</returns>
        public TimeSpan BreakDuration { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Gets or sets the amount of exception allowed to occur.
        /// System will allow this amount of exception to occur before start the Breaking.
        /// </summary>
        /// <value>Exceptions allowed.</value>
        public int ExceptionsAllowedBeforeBreaking { get; set; } = 5;
    }

    public class RetryPolicyOptions
    {
        /// <summary>
        /// Gets or sets Amount of reties the system will perform when a transient exception occurs.
        /// </summary>
        /// <value>Amount of tries to perform.</value>
        public int Count { get; set; } = 2;

        /// <summary>
        /// Gets or sets the backoffPower value.
        /// E.g Retry attempt = 1 (2ˆ1 = 2s) -> Waits 2 seconds before the next attempt.
        ///     Retry attempt = 2 (2ˆ2 = 4s) -> Waits 4 seconds before the next attempt.
        /// </summary>
        /// <value>BackoffPower value.</value>
        public int BackoffPower { get; set; } = 2;
    }
}
