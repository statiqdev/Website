﻿In Statiq Web and Statiq Docs, the Markdown engine is executed automatically for Markdown file types. In Statiq Framework you must use the `RenderMarkdown` module from the `Statiq.Markdown` package in your pipeline to render Markdown content.

# Settings

The following settings are available for the `Statiq.Markdown` package:

- `MarkdownExtensions`: List of [Markdig extensions]

# Adding extensions

## Adding extensions with the bootstrapper

When using the [Bootstrapper](xref:bootstrapper) you can add [Markdig extensions]
by setting the `MarkdownExtensions` [setting](xref:settings):

```csharp
.AddSetting(Statiq.Markdown.MarkdownKeys.MarkdownExtensions, "bootstrap")
```

This will enable the `Bootstrap` extension.

To enable multiple extension a list can be passed:

```csharp
.AddSetting(
    Statiq.Markdown.MarkdownKeys.MarkdownExtensions,
    new List<string> 
    {
        "Bootstrap",
        "Emoji"
    })
```

This will enable the `Bootstrap` and `Emoji` extensions.

## Adding extensions in Settings and Metadata

You can add [Markdig extensions] by setting the `MarkdownExtensions` [setting](xref:settings).
For example, in a [configuration file](xref:settings#configuration-files) you can write:

```txt
MarkdownExtensions:
  - Bootstrap
```

This will enable the `Bootstrap` extension.

# Pass Through Content

You can use the special "raw" language to output verbatim content that isn't subject to Markdown processing. This is helpful when you have raw HTML and other content that you want to pass-through the Markdown engine. While using HTML elements in Markdown also accomplishes a single goal, it has some limitations like breaking when new lines are included.

For example:

<?# Raw ?>
<?*
<pre class="language-txt"><code>
```raw
This
&lt;div&gt;content&lt;/div&gt;

will pass-through.
```
</code></pre>
?>
<?#/ Raw ?>

[Markdig extensions]: https://github.com/xoofx/markdig/blob/master/readme.md