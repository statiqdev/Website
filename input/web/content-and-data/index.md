Order: 2
---
Statiq Web has a variety of ways of reading, writing, and working with different kinds of content and data.

Content and data files should be placed in the `input` folder (or a sub-folder). Whether a file is considered content or data is determined by two [settings](xref:web-settings):

- The `ContentFiles` [setting](xref:web-settings) controls how [content files](xref:web-content) are located and is set to `**/{!_,}*.{html,cshtml,md}` by default. This loads all `.html`, `.cshtml`, and `.md` files in any input directory unless it starts with an underscore `_`.
- The `DataFiles` [setting](xref:web-settings) controls how data files are located and is set to `**/{!_,}*.{json,yaml,yml}` by default. This loads all `.json`, `.yaml`, and `.yml` files in any input directory unless it starts with an underscore `_`.

Both [content files](xref:web-content) and [data files](xref:web-data) support functionality like [directory metadata](xref:web-directory-metadata) files, [sidecar](xref:web-sidecar-files) files, and [front matter](xref:web-front-matter). The main difference is that [templates](xref:web-templates) like [Markdown](xref:template-languages#markdown) and [Razor](xref:template-languages#razor) are processed for content files whereas data files are parsed as data formats like JSON and YAML and add to the [metadata of a document](xref:documents-and-metadata).

Features like [archives](xref:web-archives) and [feeds](xref:web-feeds) also let you work with content and data and produce different kinds of outputs from them.