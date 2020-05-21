Statiq can be configured to deploy to [Azure App Service](https://azure.microsoft.com/en-us/services/app-service/).

The first step is to set up a new Web App which can be found under App Services in the
Azure portal. Once you have the web app set up and configured, you'll need to get the
[deployment credentials](https://github.com/projectkudu/kudu/wiki/Deployment-credentials).

The following [settings](xref:web-settings) are used to configure deployment to Azure App Service:

- `AzureAppServiceSiteName`: The name of the site in Azure App Service.
- `AzureAppServiceUsername`: The Azure App Service username to use for deployment.
- `AzureAppServicePassword`: The Azure App Service password to use for deployment.

It's customary to set one or more of these settings as an environment variable in continuous
integration environments (particularly secrets like the password). In these scenarios you can either
set an environment variable with the name of the setting or set the setting to an alternate environment
variable (which itself will be added as a setting) using a
[computed value](xref:metadata-values#computed-values) or
[configuration delegate](xref:configuration-delegates).

Using a computed value in `appsettings.json`:

```json
{
    "AzureAppServiceSiteName": "MySiteName",
    "AzureAppServiceUsername": "MyUsername",
    "AzureAppServicePassword": "=> PASSWORD_VAR"
}
```

The [bootstrapper](xref:bootstrapper) also has an extension method for configuring deployment:

```csharp
await Bootstrapper
    .Factory
    .CreateWeb(args)
    .DeployToAzureAppService(
        "MySiteName",
        "MyUsername",
        Config.FromSetting<string>("PASSWORD_VAR")
    )
    // ...
    .RunAsync();
```

You'll probably want support for extensionless URLs like most other static site hosts.
Azure App Service and IIS don't support this by default, but you can configure it using
the following `web.config` file:

```xml
<configuration>
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="html">
          <match url="(.*)" />
          <conditions>
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
            <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
          </conditions>
          <action type="Rewrite" url="{R:1}.html" />
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
</configuration>
```

Place this file in your `input` folder and it will get copied to the `output` folder and deployed.
A more detailed examination of `web.config` files for static sites can be found
[at this blog post](http://andyhansen.co.nz/posts/web-config-for-a-static-site).