Order: 1
---
Statiq Web is designed to make the most common scenarios extremely easy without requiring any configuration, but sometimes you need a little more control.

# Bootstrapper Defaults

When you create a bootstrapper using the `Bootstrapper.Factory.CreateWeb(args)` extension method, it automatically configures all [default behavior](xref:bootstrapper#default-behavior). If you'd rather have more control over bootstrapper default behavior, you can use the `AddWeb()` extension method after creating a standard bootstrapper instead.

For example, this creates a bootstrapper without adding pipelines from the entry assembly automatically:

```csharp
using System;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Web;

namespace MySite
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateDefaultWithout(args, DefaultFeatures.Pipelines)
        .AddWeb()
        .RunAsync();
  }
}
```