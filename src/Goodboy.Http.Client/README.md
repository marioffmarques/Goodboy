## Goodboy.Http.Client

##### Package providing a set of extensions for resilient http communications.

### Instalation

```sdfsdf```

### How to

To support resiliency in the application, `Goodboy.Http.Client` makes use of [Polly](https://github.com/App-vNext/Polly)
lib to enable *http retries* and *circuit-breaking* handling whenever an  http communication faces transient errors.

- **Http Retries:** Retrying an http call attempt to an external service whenever a transient error occurs.
- **Circuit Breaker:** Opens http circuit -- once a predefined exception threshold occurs in a given http call, the subsequent call attempts will be blocked during a period of time.

This library supports the latter two policy mechanisms by providing `HttpClient` objects to the application,  which can be used to perfom the actual resilient communication.

To use the library, simply add the following lines to the `Startup` class, within `ConfigureServices` method.

```javascript
var conf = new HttpServiceConfiguration()
{
    Url = new Uri("http://localhost:1321")
};

var policyConf = new PolicyConfiguration()
{
    HttpCircuitBreaker = new CircuitBreakerPolicyOptions(),
    HttpRetry = new RetryPolicyOptions()
};
      
services.ConfigureJsonHttpClient<ITestApiClient, TestApiClient>(conf, policyConf);
```

Note: `conf` and `policyConf` are configuration classes that should be resolved through `AppSetting.json` file. 

In addition, we are configuring the `TestApiClient` service class to operate as a wrapper over the actual http operations to be made in the application. This is the class that should be injected and invoqued in other application components.

```javascript
public class TestApiClient : ITestApiClient
{
    private readonly IExternalApi _api;

    public TestApiClient(HttpClient client)
    {
        _api = RestService.For<IExternalApi>(client);
    }

    public async Task<IEnumerable<string>> GetStuff()
    {
        await _api.GetAll();
    }
}
```

`IExternalApi` is an interface for the external service api created with [Reffit](https://github.com/reactiveui/refit) with a simple method for illustration purposes.

```javascript
public interface IExternalApi
{
    [Get("/api/test")]
    Task<IEnumerable<string>> GetAll();
}
```