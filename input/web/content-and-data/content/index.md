Order: 1
---
Content files contain the pages of your site and are processed based on their media type (which is determined by file extension).

The following extensions are recognized by default:

- `.html` processes the file as plain HTML.
- `.md` processes the file as [Markdown](xref:template-languages#markdown).
- `.cshtml` processes the file as [Razor](xref:template-languages#razor).

The `ContentFiles` [setting](xref:web-settings) controls how content files are located and is set to `**/{!_,}*.{html,cshtml,md}` by default. This loads all `.html`, `.cshtml`, and `.md` files in any input directory unless it starts with an underscore `_`.

In all cases, global processing (such as processing [front matter](xref:web-front-matter) and applying [shortcodes](xref:web-shortcodes)) is performed and [layouts](xref:web-templates#layouts) and [themes](xref:web-themes) are applied.

Many [themes](xref:web-themes) treat content differently depending on what sub-folder the files are in. For example, themes for blogging often infer blog posts should go in a "input/posts" sub-folder. In most cases the specific paths used for different types of content are [configurable as settings](xref:web-settings).