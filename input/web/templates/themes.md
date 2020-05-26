Themes allow you to apply general content, data, layouts, and settings to your own site.

In general, themes are placed in a `theme` folder alongside the `input` folder.
Inside the `theme` folder is a theme `input` folder that contains any type of file the normal `input` folder would.
The theme folder is combined with other input folders in the [virtual file system](xref:files-and-paths#virtual-file-system)
at a lower priority (that is, any files in your `input` folder will take precedence over theme files). Themes can take advantage
of this property by providing files that are intended to be overridden.

Themes can also include [configuration files](xref:settings#configuration-files) that contain global settings which are applied
at a lower priority then other configuration files or settings from other sources. Theme configuration files go in the `theme`
folder and can be named:

- `themesettings.json`
- `statiq.json`

A folder structure with a theme and both application and theme configuration files might look like:

- `appsettings.json`
- `input\`
  - `content-file.md`
- `theme\`
  - `themesettings.json`
  - `input\`
    - `styles.css`
    - `_Layout.cshtml`

Themes can be copied into the `theme` folder from anywhere. In the future, Statiq will support built-in commands for
obtaining and working with themes.
