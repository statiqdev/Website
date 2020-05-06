Order: 4
---
Statiq uses a dependency injection container to provide certain services and you can register your own services and use them in your [pipelines and modules](xref:pipelines-and-modules).

You can register a service through the bootstrapper using the `ConfigureServices` extension method which provides an `IServiceCollection`:

```csharp
using System;
using Statiq.App;

namespace MyGenerator
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateDefault(args)
        .ConfigureServices(services =>
          services.AddTransient<IMyService, MyServiceInstance>())
        .RunAsync();
  }
}
```

Once a service is registered, you can access it through the `Services` property of the [execution context](xref:execution-context#properties).

You can also [inject dependencies into pipelines](xref:defining-pipelines#service-injection).
