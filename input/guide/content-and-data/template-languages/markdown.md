In Statiq Web and Statiq Docs, the Markdown engine is executed automatically for Markdown file types. In Statiq Framework you must use the `RenderMarkdown` module from the `Statiq.Markdown` package in your pipeline to render Markdown content.

The `Statiq.Markdown` package uses [Markdig](https://github.com/xoofx/markdig) to process the Markdown,
which is a CommonMark compliant Markdown process with additional features like support for
GitHub Flavored Markdown fenced code blocks.

# Escaping @ (Or Not)

Because of the way Markdown is often passed to [Razor](xref:Razor) for rendering within a layout, and because Razor uses the `@` character to escape HTML content and start Razor instructions, it's often helpful to escape the `@` character in the output from `RenderMarkdown` if it's going to be passed on to the Razor engine.

[Statiq Web](xref:web) does this automatically. To escape the `@` character in your own pipelines when using the `RenderMarkdown` module, call the `.EscapeAt()` fluent method on the module.

If you'd like to turn off this behavior in [Statiq Web](xref:web), you can turn it off on a per-file basis by setting the metadata value `EscapeAtInMarkdown` to `false` in something like [front matter](xref:front-matter). You can also turn the `@` escape behavior off globally by [modifying the Markdown template](xref:templates#modifying-templates) that contains the `RenderMarkdown` module:

```csharp
await Bootstrapper.Factory
    .CreateWeb(args)
    // ...
    .ModifyTemplate(
        MediaTypes.Markdown,
        x => ((RenderMarkdown)x).EscapeAt(false))
    // ...
    .RunAsync();
``` 

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

## Adding extensions by modifying the module or template

The above approaches require that the Markdig extension support a parameterless default constructor and may not work for other extensions. If you see error messages like "Markdown extension XYZ does not have a usable constructor" when trying to configure an extension, you may need to add an instance of that extension directly.

If using the `RenderMarkdown` module directly, you can add an instance of an extension directly using the `.UseExtension()` method:

```csharp
new RenderMarkdown()
    .UseExtension(new SmartyPantsExtension(new SmartyPantOptions()))
```

When using [Statiq Web](xref:web) you will likely want to [modify the Markdown template](xref:templates#modifying-templates) that contains the `RenderMarkdown` module:

```csharp
await Bootstrapper.Factory
    .CreateWeb(args)
    // ...
    .ModifyTemplate(
        MediaTypes.Markdown,
        x => ((RenderMarkdown)x)
            .UseExtension(new SmartyPantsExtension(new SmartyPantOptions())))
    // ...
    .RunAsync();
```

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