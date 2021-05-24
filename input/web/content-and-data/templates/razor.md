Statiq supports the Razor engine from ASP.NET Core 3.0.

# Document And Context Access

The Razor engine in Statiq uses a custom Razor base page type of `StatiqRazorPage<TModel>`. This exposes several additional properties including `@Document` to access the currently rendering document and `@Context` to access the current [execution context](xref:execution-context).

## Model

The default model for a Razor page in Statiq is typically also set to the current document and can be accessed using `@Model` as you would in Razor from ASP.NET Core. However, it is _strongly_ recommended that you get in the habit of referencing your document via the strongly-typed `@Document` property of the Statiq Razor base page to avoid model typing problems (see below).

# Imports File and Intellisense

Because Statiq uses a custom Razor base page, the Razor Intellisense engine doesn't actually know about the default namespaces, base page, or model. While these will be set during generation, it might be helpful to also let the Intellisense version of Razor know about them so you get better hints and errors. To do so, create a `_ViewImports.cshtml` file in your input folder:

```html
@using Statiq.Common
@using Statiq.Razor
@using Statiq.Web
@using Statiq.Web.Pipelines
@using Microsoft.Extensions.Logging

@inherits StatiqRazorPage<IDocument>
```

## View Imports Model

**Important note:** if you do create a `_ViewImports.cshtml` file, it will also instruct the Statiq Razor engine during generation. Generally this isn't a problem since the file is just reiterating what Statiq is doing behind the scenes. However, if any of your partial views change the model with a `@model` directive, the `@inherits` directive in the `_ViewImports.cshtml` file will take precedence and the model you define in the partial view will not work. In this case you should define your partial model using a full `@inherits` directive. For example, `@model int` should become `@inherits StatiqRazorPage<int>`.

# Layouts and Partials Model

Razor layouts and partials need to generally handle all model types (since different views with different model types can call them), so by default they use a model type of `dynamic` unless explicitly specified otherwise. Unlike normal views where `dynamic` model types are converted to `IDocument` (since we know there aren't other "callers" of each individual view), a layout or partial will keep
it's default `dynamic` model type. This means that treating the `@Model` property as an `IDocument` will fail in some cases (specifically when using extension methods like `IDocument.GetString()`). If you see "does not contain a definition" error messages during Razor compilation, try changing `@Model` property access to `@Document` in your layouts and partials, or using an explicit `@model` directive at the top of the layout partial file to explicitly specify the model type as `IDocument` for that layout or partial.
