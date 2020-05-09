In addition to the [settings available from Statiq Framework](xref:settings), Statiq Web also has a number of settings that can be used to control behavior.

The following global settings are available:

- `ContentFiles`: The globbing pattern(s) that will be used to read [content files](xref:web-content).
- `DataFiles`: The globbing pattern(s) that will be used to read [data files](xref:web-data).
- `DirectoryMetadataFiles`: The globbing pattern(s) that will be used to read [directory metadata files](xref:web-directory-metadata).
- `OptimizeContentFileNames`: Indicates that content output file names should be optimized to remove spaces, etc.
- `OptimizeDataFileNames`: Indicates that data output file names should be optimized to remove spaces, etc.
- `GenerateSitemap`: Indicates that a sitemap file should be generated (the default value is `true`).
- `MirrorResources`: Indicates that [resource mirroring](xref:web-resource-mirroring) should be enabled.
- `ValidateAbsoluteLinks`: Indicates that absolute links should be validated (may add considerable time to your generation process).
- `ValidateRelativeLinks`: Indicates that relative links should be validated (the default value is `true`).
- `ValidateLinksAsError`: Indicates that link validation failures should be treated as errors and fail the build.

Settings keys are just strings, but most built-in settings are also defined as string constants in the `WebKeys` class.

You can change settings using the [bootstrapper](xref:specifying-settings):

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
        .AddSetting(
          WebKeys.ValidateAbsoluteLinks,
          true)
        .RunAsync();
  }
}
```

As with any other metadata, setting values can be [computed values](xref:metadata-values#computed-values) or [configuration delegates](xref:configuration-delegates) and their value will be evaluated at run-time.