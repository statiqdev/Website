Statiq supports the Razor engine from ASP.NET Core 3.0.

# Imports File and Intellisense

Because Statiq uses a custom Razor base page, the Razor Intellisense engine doesn't actually know about the default namespaces, base page, or model. While these will be set during generation, it might be helpful to also let the Intellisense version of Razor know about them so you get better hints and errors. To do so, create a `_ViewImports.cshtml` file in your input folder:

```html
@using Statiq.Common
@using Statiq.Razor
@using Statiq.Web
@using Statiq.Web.Pipelines
@using Statiqdev
@using Microsoft.Extensions.Logging

@inherits StatiqRazorPage<IDocument>
```

**Important note:** if you do create a `_ViewImports.cshtml` file, it will also instruct the Statiq Razor engine during generation. Generally this isn't a problem since the file is just reiterating what Statiq is doing behind the scenes. However, if any of your partial views change the model with a `@model` directive, the `@inherits` directive in the `_ViewImports.cshtml` file will take precedence and the model you define in the partial view will not work. In this case you should define your partial model using a full `@inherits` directive. For example, `@model int` should become `@inherits StatiqRazorPage<int>`.