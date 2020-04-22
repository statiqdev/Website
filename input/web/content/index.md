Order: 2
---
Statiq Web has a variety of ways of reading, writing, and working with different kinds of content.

In general, your content should be placed in the “input” folder (or a sub-folder) and is processed based on it’s media type (which is typically determined by file extension). The following extensions are recognized by default:

- `.html` processes the file as plain HTML.
- `.md` processes the file as [Markdown](/web/templates/markdown).
- `.cshtml` processes the file as [Razor](/web/templates/razor).

In all cases, global processing such as [shortcodes](/web/content/shortcodes) is performed and [layouts](/web/templates#layouts) and [themes](/web/templates/themes) are applied.

Many [themes](/web/templates/themes) also support treating content differently depending on what sub-folder the files are in. For example, themes for blogging often infer blog posts should go in a “input/posts” sub-folder. In most cases the specific paths used for different types of content are [configurable as settings](xref:web_settings).

<?# ChildPages /?>