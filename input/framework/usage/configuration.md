Configuration can be performed in several different ways and can come from a variety of sources. At [execution time](/framework/concepts/execution) all configuration is combined into a single set of settings accessible via the [execution context](/framework/concepts/execution#execution-context) which implement `IMetadata` for the purpose of providing configuration and setting data.

# Configuration Files

As with many other .NET Core applications, Statiq supports the use of an `appsettings.json` file. Any values will be 