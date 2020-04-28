Order: 2
---
Settings let you define global [metadata](xref:documents-and-metadata#about-metadata) that can be accessed from the [execution context](xref:execution#execution-context) or [documents](xref:documents-and-metadata).

At [execution time](xref:execution) all settings are combined into a single set of metadata accessible via the [execution context](xref:execution#execution-context), which implements `IMetadata` for the purpose of exposing settings values.

# Specifying Settings

## Settings Files

As with many other .NET Core applications, Statiq supports the use of an `appsettings.json` file. Any values will be ...

## Environment Variables

## Bootstrapper

## Cascade To Documents

All settings "cascade" to documents. That means that any value defined in settings files, environment variables, etc. is also available as document metadata unless otherwise overwritten by the document. This include settings that would conventionally be set just at a document level (for example, a page title). This feature can be very useful for situations when you want all documents to have a particular default value for a setting or when you want to use a common [computed value](xref:metadata-values#computed-values) to define a script to use to calculate a different value for each document.

For example, if you want to set the "Published" key of _every_ document to the date the site was built you can add this to your `appsettings.json` file:

```json
{
    "Published": "=> DateTime.Today"
}
```

Note that this configuration value uses a [computed value](xref:metadata-values#computed-values) to calculate the current `DateTime` at runtime.