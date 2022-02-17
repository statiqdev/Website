Badge: Web
---
Statiq Web can locally mirror remote resources such as scripts and stylesheet links.

To enable this feature, set the `MirrorResources` setting to `true` (like most [Statiq Web settings](xref:web-settings) this key is strongly-typed via the `WebKeys` class):

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
        .CreateWeb(args)
        .AddSetting(WebKeys.MirrorResources, true)
        .RunAsync();
  }
}
```

When resource mirroring is on, all `<script>` and `<link>` resources will be copied to the local output directory and the HTML elements will be rewritten to point to the local path. This can be especially helpful when combined with [HTTP pipelining](https://en.wikipedia.org/wiki/HTTP_pipelining) and [HTTP/2 multiplexing](https://developers.google.com/web/fundamentals/performance/http2/#request_and_response_multiplexing). However, it's not appropriate for all cases. Some cases where resource mirroring will not work well include:

- Resources where the content might change.
- Resources that use relative paths to point to other resources.

To disable resource mirroring for a particular resource when the feature is turned on globally, use a `data-no-mirror` attribute on the HTML element:

```html
<script src="https://kit.fontawesome.com/123456789.js" data-no-mirror></script>
```