Order: 12
---
Statiq Web supports multiple template languages (_templates_) and you can specify which ones should be applied for your site.

A template applies to a documents with a particular media type (generally inferred from the file extension) and the definition
indicates the [processing phase](xref:pipelines-and-modules#phases) in which the template engine should be executed
(either the [process](xref:pipelines-and-modules#process-phase) or [post-process](xref:pipelines-and-modules#post-process-phase) phase).

# Default Templates

By default the following templates are defined:

- Markdown ([process phase](xref:pipelines-and-modules#process-phase))
- [Razor](xref:web-razor) ([post-process phase](xref:pipelines-and-modules#post-process-phase))
- Handlebars ([post-process phase](xref:pipelines-and-modules#post-process-phase))
- HTML ([post-process phase](xref:pipelines-and-modules#post-process-phase), see the [layouts](#layouts) section below)

# Modifying Templates

Templates are defined and modified using the `Template` class and `Templates` collection. When defining a template you can specify
the [processing phase](xref:pipelines-and-modules#phases) in which the template engine should be executed, the media type
of files the template should be executed on, and the [module](xref:about-modules) to execute for the template.

You can add or modify templates with the [bootstrapper](xref:bootstrapper):

- `.ConfigureTemplates(templates => ...)`

If you want to modify an existing template, you can access the current module for that template using the `Templates` indexer. For
example, to modify the Markdown template to process emoji shortcuts of the form `:smile:` using Markdig's emoji extension, you can write:

```csharp
.ConfigureTemplates(templates =>
  ((RenderMarkdown)templates[MediaTypes.Markdown].Module)
    .UseExtension(new EmojiExtension()))
```

In the code above, the second line gets the template for documents with a media type of `MediaType.Markdown` and casts it to a `RenderMarkdown` module
(since we know that's the kind of module registered for Markdown documents). Then the next like adds the `Markdig.Extensions.Emoji.EmojiExtension`
extension to the module using the `RenderMarkdown.UseExtension()` method.

You can adjust the module for any template this way by getting the module, casting it to the correct type, and then changing it.

# Layouts

In addition to rendering content in the document, templates are responsible for rendering common layouts. A collection
of layout files and other common functionality is often called a [theme](xref:web-themes).

The HTML template is generally responsible for applying layouts, which is why one is defined. By default, the HTML template
references the same [Razor](xref:web-razor) module as the Razor template. That way, any plain HTML files or other non-Razor
document like Markdown files will still be run through the Razor engine to apply layouts. To change this behavior and specify
a different template for processing HTML files, you can replace the module:

```csharp
.ConfigureTemplates(templates => templates[MediaTypes.Html].Module = new SomeOtherModule())
```