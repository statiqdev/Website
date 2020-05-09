Order: 2
---
Settings let you define global [metadata](xref:documents-and-metadata#about-metadata) that can be accessed from the [execution context](xref:execution#execution-context) or [documents](xref:documents-and-metadata).

At [execution time](xref:execution) all settings are combined into a single set of metadata accessible via the [execution context](xref:execution#execution-context), which implements `IMetadata` for the purpose of exposing settings values.

Settings that you can use to control Statiq Framework include:

- `Host`: The host to use when generating links (for example, "statiq.dev").
- `LinksUseHttps`: Indicates if generated links should use HTTPS instead of HTTP as the scheme.
- `LinkRoot`: The default relative root path to use when generating links (for example "/virtual/directory").
- `LinkHideIndexPages`: Indicates whether to hide index pages by default when generating links.
  - When using the [bootstrapper](xref:bootstrapper) this defaults to `true`.
- `LinkHideExtensions`: Indicates whether to hide ".html" and ".htm" extensions by default when generating links.
  - When using the [bootstrapper](xref:bootstrapper) this defaults to `true`.
- `LinkLowercase`: Indicates that links should always be rendered in lowercase.
- `UseStringContentFiles`: This will cause temporary backing files to be created for string document content instead of storing that content in memory.
- `UseCache`: Indicates whether caching should be used.
- `CleanOutputPath`: Indicates whether to clean the output path on each execution.
- `DateTimeInputCulture`: Indicates the culture to use for reading and interpreting dates as input.
- `DateTimeDisplayCulture`: Indicates the culture to use for displaying dates in output.

Settings keys are just strings, but most built-in settings are also defined as string constants in the `Keys` class.

As with any other metadata, setting values can be [computed values](xref:metadata-values#computed-values) or [configuration delegates](xref:configuration-delegate) and their value will be evaluated at run-time.

# Cascade To Documents

All settings "cascade" to [documents](xref:documents-and-metadata). That means that any value defined in [configuration files](#configuration-files), [environment variables](#environment-variables), etc. is also available as document metadata unless otherwise overwritten by the document. This include settings that would conventionally be set just at a document level (for example, a page title). This feature can be very useful for situations when you want all documents to have a particular default value for a setting or when you want to use a common [computed value](xref:metadata-values#computed-values) to define a script to use to calculate a different value for each document.

For example, if you want to set the "Published" key of _every_ document to the date the site was built you can add this to your `appsettings.json` file:

```json
{
    "Published": "=> DateTime.Today"
}
```

Note that the setting demonstrated above uses a [computed value](xref:metadata-values#computed-values) to calculate the current `DateTime` at runtime.

# Specifying Settings With The Bootstrapper

Settings can be provided directly to the [engine](xref:execution#engine), but the easiest and most common way to specify settings is to use a [bootstrapper](xref:bootstrapper).

## Configuration Files

As with many other .NET Core applications, Statiq supports the use of an `appsettings.json` file. Additionally, a `statiq.json` file can also be used. Support for both is provisioned by the bootstrapper by default.

## Environment Variables

The bootstrapper adds all existing environment variables as settings by default. This makes it easy to define settings for your generation from build servers.

## Fluent API

The bootstrapper also contains [fluent methods for specifying settings](xref:specifying-settings).
