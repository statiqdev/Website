Order: 2
---
You can use the bootstrapper to specify [settings](xref:settings), either from various sources like configuration file and environment variables, or by using fluent methods.

By default, the bootstrapper will add settings from `appsettings.json` and `statiq.json` files as well as all environment variables. This behavior can be controlled via the [bootstrapper default behavior](xref:bootstrapper#default-behavior).

In addition, you can specify settings directly using fluent methods:

- `AddSetting()` specifies a single setting and it's value.
- `AddSettings()` specifies multiple settings using a `IEnumerable<KeyValuePair<string, object>>` such as a dictionary.
- `AddSettingIfNonExisting()` specifies a single setting only if the setting doesn't already exist.
- `AddSettingsInNonExisting()` specifies multiple settings only for the specified settings that don't already exist.