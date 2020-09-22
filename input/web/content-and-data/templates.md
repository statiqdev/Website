Order: 12
---
Statiq Web supports multiple template languages (_templates_) and you can specify which ones should be applied for your site.

A template applies to a documents with a particular media type (generally infered from the file extension) and the definition
indicates the [processing phase](xref:pipelines-and-modules#phases) in which the template engine should be executed
(templates are executed in the [post-process phase](xref:pipelines-and-modules#post-process-phase) unless otherwise specified.

# Default Templates

By default the following templates are defined:

- Markdown (in the [process phase](xref:pipelines-and-modules#process-phase))
- Razor
- Handlebars

A default template is also specified which will be executed last regardless of media type. This allows a particular template
engine to process layouts and other global files for all documents. For example, the Razor template is specified as the standard
default and any Markdown or Handlebars documents will still be processed by the Razor engine to add layouts from files
like `_Layout.cshtml`.

# Modifying Templates

Templates are defined and modified using the `Template` class and `Templates` collection. When defining a template you can specify
the [processing phase](xref:pipelines-and-modules#phases) in which the template engine should be executed, the media type
of files the template should be executed on, and the [module](xref:about-modules) to execute for the template.

You can add or modify templates with the [bootstrapper](xref:bootstrapper):

- `.ConfigureTemplates(templates => ...)`

You can also set the default template (by name) if you want to change it:

- `.SetDefaultTemplate("Handlebars")`

# Layouts

In addition to rendering content in the document, templates are responsible for rendering common layouts. A collection
of layout files and other common functionality is often called a [theme](xref:web-themes).