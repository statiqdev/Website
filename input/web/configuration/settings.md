In addition to the [settings available from Statiq Framework](xref:settings), Statiq Web also has a number of settings that can be used to control behavior. Global settings that apply to the generation process and all documents are generally defined [in a configuration file](xref:settings#configuration-files). Most settings are also appropriate to define [at the directory level](xref:web-directory-metadata) or [per-document](xref:web-front-matter) if you only want them to apply to specific files.

The following global settings are available:

- `ContentFiles`: The globbing pattern(s) that are be used to read [content files](xref:web-content).
- `DataFiles`: The globbing pattern(s) that are be used to read [data files](xref:web-data).
- `AssetFiles`: The globbing pattern(s) that are be used to find and copy asset files to the output folder.
- `DirectoryMetadataFiles`: The globbing pattern(s) that will be used to read [directory metadata files](xref:web-directory-metadata).
- `OptimizeContentFileNames`: Indicates that content output file names should be optimized to remove spaces, etc.
- `OptimizeDataFileNames`: Indicates that data output file names should be optimized to remove spaces, etc.
- `ApplyDirectoryMetadata`: Set to `false` to prevent processing directory metadata.
- `ProcessSidecarFiles`: Set to `false` to prevent processing sidecar files.
- `GenerateSitemap`: Indicates that a sitemap file should be generated (the default value is `true`).
- `MirrorResources`: Indicates that [resource mirroring](xref:web-resource-mirroring) should be enabled.
- `PublishedUsesLastModifiedDate`: Controls whether a file modified date should be used for getting published dates (the default value is `true`).
- `MakeLinksAbsolute`: Set to `true` to rewrite all relative links as absolute.
- `MakeLinksRootRelative`: Set to `true` to rewrite all relative links as root-relative.
- `ValidateAbsoluteLinks`: Indicates that absolute links should be validated (may add considerable time to your generation process).
- `ValidateRelativeLinks`: Indicates that relative links should be validated (the default value is `true`).
- `ValidateLinksAsError`: Indicates that link validation failures should be treated as errors and fail the build.
- `MetaRefreshRedirects`: Generates META-REFRESH [redirect pages](xref:web-redirects) (the default value is `true`).
- `NetlifyRedirects`: Generates a Netlify [redirects file](xref:web-redirects).

Settings keys are just strings, but most built-in settings are also defined as string constants in the `WebKeys` class.

You can also define settings using the [bootstrapper](xref:specifying-settings):

```csharp
using System;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
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
