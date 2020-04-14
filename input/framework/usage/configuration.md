Statiq has several mechanisms for specifying configuration data.

At [execution time](/framework/concepts/execution) all configuration data is combined into a single set of settings accessible via the [execution context](/framework/concepts/execution#execution-context) which implement `IMetadata` for the purpose of providing configuration and setting data.

# Configuration Files

As with many other .NET Core applications, Statiq supports the use of an `appsettings.json` file. Any values will be ...

# Environment Variables

# Bootstrapper

# Cascade To Documents

All configuration data "cascades" to documents. That means that any value set in the configuration file, environment variables, etc. is also available as document metadata unless otherwise overwritten. This include settings that would otherwise be set just at a document level. This feature can be very useful for situations when you want all documents to have a particular default value for a setting.

For example, if you want to set the "Published" key of _every_ document to the date the site was built you can add this to your `appsettings.json` file:

```json
{
    "Published": "=> DateTime.Today"
}
```

Note that this configuration value uses [computed metadata](/framework/concepts/metadata#computed-metadata) to calculate the current `DateTime` at runtime.