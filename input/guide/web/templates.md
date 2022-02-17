Order: 12
Badge: Web
---
Statiq Web supports multiple template languages and you can specify which ones should be applied for your site. The specification of a template language, to what files it should apply, and when it should be executed is generally referred to as a _template_.

A template applies to a documents with a particular media type (generally inferred from the file extension) and the definition
indicates the [content type](xref:content-and-media-types) and [processing phase](xref:pipelines-and-modules#phases) in which the template engine should be executed
(either the [process](xref:pipelines-and-modules#process-phase) or [post-process](xref:pipelines-and-modules#post-process-phase) phase).

# Default Templates

The following templates are defined by default and are executed in the specified phase:

## Assets

- Less ([process phase](xref:pipelines-and-modules#process-phase))
- Sass ([process phase](xref:pipelines-and-modules#process-phase))

## Data

- JSON ([process phase](xref:pipelines-and-modules#process-phase))
- YAML ([process phase](xref:pipelines-and-modules#process-phase))

## Content

- [Markdown](xref:markdown) ([process phase](xref:pipelines-and-modules#process-phase))
- [Razor](xref:razor) ([post-process phase](xref:pipelines-and-modules#post-process-phase))
- Handlebars ([post-process phase](xref:pipelines-and-modules#post-process-phase))
- HTML ([post-process phase](xref:pipelines-and-modules#post-process-phase), see the [layouts](#layouts) section below)

# Adding Templates

Templates are defined and modified using the `Template` class and `Templates` collection. When defining a template you can specify the [content type](xref:content-and-media-types) the template should apply to (assets, content, or data),
the [processing phase](xref:pipelines-and-modules#phases) in which the template engine should be executed, the media type
of files the template should be executed on, and the [module](xref:about-modules) to execute for the template.

You can add or modify templates with the [bootstrapper](xref:bootstrapper):

- `.ConfigureTemplates(templates => ...)`

There's also a specific extension for adding a new template:

- `.AddTemplate(string mediaType, ContentType contentType, Phase phase, IModule module)`

# Modifying Templates

A bootstrapper extensions is also provided for modifying existing templates:

- `.ModifyTemplate(string mediaType, Func<IModule, IModule> modifyModule)`

This lets you edit or change the module used in the template for a given media type. For example, if you want to customize the `RenderMarkdown` module by adding an extension:

```csharp
bootstrapper
// ...
  .ModifyTemplate(MediaTypes.Markdown, module => ((RenderMarkdown)module)
    .UseExtension(new Markdig.Extensions.Emoji.EmojiExtension()))
```

Using this approach you can change the module for any template by casting the module to the correct type and modifying it.

You can also return an entirely new module:

```csharp
bootstrapper
// ...
  .ModifyTemplate(MediaTypes.Markdown, _ => new MyCustomMarkdownModule())
```

If you return `null` from the delegate you can "deactivate" the template as well. For example, this will turn off Markdown rendering:

```csharp
bootstrapper
// ...
  .ModifyTemplate(MediaTypes.Markdown, _ => null)
```

# Removing Templates

Instead of modifying the template to set a `null` module, there's also an extension for removing a template entirely:

- `.RemoveTemplate(string mediaType)`

For example, if you process Sass files yourself you might not want Statiq to process them:

```csharp
bootstrapper
// ...
  .RemoveTemplate(MediaTypes.Sass)
```

# Layouts

In addition to rendering content in the document, templates are responsible for rendering common layouts. A collection
of layout files and other common functionality is often called a [theme](xref:themes).

The HTML template is generally responsible for applying layouts, which is why one is defined. By default, the HTML template
references the same [Razor](xref:razor) module as the Razor template. That way, any plain HTML files or other non-Razor
document like Markdown files will still be run through the Razor engine to apply layouts. To change this behavior and specify
a different template for processing HTML files, you can replace the module:

```csharp
.ConfigureTemplates(templates => templates[MediaTypes.Html].Module = new SomeOtherModule())
```