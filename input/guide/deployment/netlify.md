Badge: Web
---
Statiq can be configured to deploy to [Netlify](https://www.netlify.com).

The following [settings](xref:web-settings) are used to configure deployment to Netlify:

- `NetlifySiteId`: The ID of the Netlify site.
- `NetlifyAccessToken`: The access token to use for deployment.

It's customary to set one or more of these settings as an environment variable in continuous
integration environments (particularly secrets like the access token). In these scenarios you can either
set an environment variable with the name of the setting or set the setting to an alternate environment
variable (which itself will be added as a setting) using a
[computed value](xref:metadata-values#computed-values) or
[configuration delegate](xref:configuration-delegates).

Using a computed value in `appsettings.json`:

```json
{
    "NetlifySiteId": "MySiteId",
    "NetlifyAccessToken": "=> TOKEN_VAR"
}
```

The [bootstrapper](xref:bootstrapper) also has an extension method for configuring deployment:

```csharp
return await Bootstrapper
    .Factory
    .CreateWeb(args)
    .DeployToNetlify(
        "MySiteId",
        Config.FromSetting<string>("TOKEN_VAR")
    )
    // ...
    .RunAsync();
```

Note that you don't need to call `DeployToNetlify` if you're specifying the `NetlifySiteId` and `NetlifyAccessToken`
settings elsewhere (like an `appsettings.json` file). This method is just a convenience for setting those values. The
Netliy deployment will automatically take place if those settings are defined, regardless of how or where they were set.

If you're deploying to Netlify you can also set `NetlifyRedirects` to `true` to
create a [Netlify redirects file](xref:redirects#netlify).