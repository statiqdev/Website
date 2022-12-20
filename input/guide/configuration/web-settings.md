Order: 100
Badge: Web
---
In addition to the [settings available from Statiq Framework](xref:settings), Statiq Web also has a number of settings that can be used to control behavior. Global settings that apply to the generation process and all documents are generally defined [in a configuration file](xref:settings#configuration-files). Most settings are also appropriate to define [at the directory level](xref:directory-metadata) or [per-document](xref:front-matter) if you only want them to apply to specific files.

The following global settings are available:

- `InputFiles`: The globbing pattern(s) that are be used to read files from the input folder.
- `DirectoryMetadataFiles`: The globbing pattern(s) that will be used to read [directory metadata files](xref:directory-metadata).
- `OptimizeContentFileNames`: Indicates that content output file names should be optimized to remove spaces, etc.
- `OptimizeDataFileNames`: Indicates that data output file names should be optimized to remove spaces, etc.
- `ApplyDirectoryMetadata`: Set to `false` to prevent processing directory metadata.
- `ProcessSidecarFiles`: Set to `false` to prevent processing sidecar files.
- `GenerateSitemap`: Indicates that a sitemap file should be generated (the default value is `true`).
- `IncludeInSitemap`: Set to `false` to exclude a document from the sitemap file.
- `MirrorResources`: Indicates that [resource mirroring](xref:resource-mirroring) should be enabled.
- `PublishedUsesLastModifiedDate`: Controls whether a file modified date should be used for getting published dates (the default value is `true`).
- `GatherHeadingsLevel`: The level (`2` for `<h2>`, etc.) at which to gather headings for a document (this can be set globally or per-document).
- `MakeLinksAbsolute`: Set to `true` to rewrite all relative links as absolute.
- `MakeLinksRootRelative`: Set to `true` to rewrite all relative links as root-relative.
- `ValidateAbsoluteLinks`: Indicates that absolute links should be validated (may add considerable time to your generation process).
- `ValidateRelativeLinks`: Indicates that relative links should be validated (the default value is `true`).
- `ValidateLinksAsError`: Indicates that link validation failures should be treated as errors and fail the build.
- `MetaRefreshRedirects`: Generates META-REFRESH [redirect pages](xref:redirects) (the default value is `true`).
- `NetlifyRedirects`: Generates a Netlify [redirects file](xref:redirects).

In addition, the following settings can be applied to documents (for example via [front matter](xref:front-matter) or [sidecar files](xref:sidecar-files)):

- `ContentType`: Indicates the type of content and thus which pipeline will process the file (recognized values are `Asset`, `Content`, and `Data`). Automatically set based on file extension but can be overridden.
- `MediaType`: Changes the media type of the document (normally this is interpreted from the file extension).
- `Script`: Set to `true` to indicate that the content contains a script that should be evaluated before processing normally.
- `RemoveScriptExtension`: Indicates that if a script file has a second extension such as `foo.md.csx` that the script extension should be removed and the preceding extension should be used to reset the media type after script evaluation, and thus the templates that will be executed (the default is `true`).
- `Title`: The title of the document, often used by themes.
- `Description`: The description of the document, often used by themes.
- `Excluded`: Indicates the document should be excluded from pipeline processing if `true`.
- `ShouldOutput`: Indicates that the file should be output. By default content files are output and data files are not.
- `ClearDataContent`: Set this to `true` to clear the content of data files after processing (the default is `false`).
- `Layout`: Indicates the layout file that should be used for this document (currently only used by the Razor template engine).
- `Xref`: Specifies the cross-reference ID of the current document. If not explicitly provided, it will default to the title of the document with spaces replaced by underscores.
- `RenderPostProcessTemplates`: Indicates that post-process templates should be rendered (the default is `true`). Set this to `false` for a document to prevent rendering post-process templates such as Razor. This can be helpful when you have small bits of content like Markdown that you want to render as HTML but not as an entire page so that it can be included in other pages.

Settings keys are just strings, but most built-in settings are also defined as string constants in the `WebKeys` class. Also note the lists above are not exhaustive and additional global or document-level settings might be available based on extra modules, the theme in use, or other factors.

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