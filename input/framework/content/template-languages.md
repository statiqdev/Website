A template language defines syntax you can use along with your content to describe how it should be rendered to a designated output format. Through various extension libraries, Statiq provides support for multiple templating engines and languages.

# HTML Output

- The `RenderMarkdown` module in the `Statiq.Markdown` package renders Markdown content to HTML.
  - The `Statiq.Markdown` package also contains a `Markdown` shortcode you can use to render Markdown content in any other template, including raw HTML files.
- The `RenderRazor` module in the `Statiq.Razor` package renders Razor templates to HTML, including full .NET Core 3.x conventions such as partials, layout files, and tag helpers.
- The `RenderHandlebars` module in the `Statiq.Handlebars` package renders Handlebars templates to HTML.

# CSS Output

- The `CompileSass` module in the `Statiq.Sass` package compiles Sass content to CSS.
- The `CompileLess` module in the `Statiq.Less` package compiles Less content to CSS.