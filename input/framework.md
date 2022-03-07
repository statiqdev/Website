<!---
Title: About Statiq Framework
ShowInNavigation: false
ShowInSidebar: false
NoSidebar: true
Xref: framework
--->
```raw
@section Title { }

<div class="text-center mb-4">
    <img src="/assets/statiq-framework.svg" alt="Statiq Framework" class="w-50"></img>
</div>
```

Statiq Framework is a powerful static generation framework that can be used to create custom static generation applications. While many users will find that [Statiq Web](xref:web) or [Statiq Docs](xref:docs) have enough functionality built-in, crafting a custom static generator with Statiq Framework provides the most flexibility.

# Quick Start

The easiest way to get started with Statiq Framework is to install the [Statiq.App](https://www.nuget.org/packages/Statiq.App) package into a .NET Core console application and use the [bootstrapper](xref:bootstrapper) to configure everything.

There's no `statiq.exe`. Unlike other static generators which ship as a self-contained executable, Statiq is a framework and as such it runs from within your own console application. This provides the greatest flexibility and extensibility and is one of the unique aspects of using Statiq.

## Step 1: Install .NET Core

Statiq Framework consists of .NET Core libraries and [installing the .NET Core SDK](https://dot.net) is the only prerequisite.

## Step 2: Create a .NET Core Console Application

Create a new console application using the `dotnet` command-line interface:

```csharp
dotnet new console -o MyGenerator
```

## Step 3: Install Statiq.App

```csharp
dotnet add package Statiq.App --version x.y.z
```

Use whatever is the [most recent version of Statiq.App](https://www.nuget.org/packages/Statiq.App). The `--version` flag is needed while the package is pre-release.

## Step 4: Create a Bootstrapper

There are several ways to create and configure an [engine](xref:execution#engine), but by far the easiest is to use the [bootstrapper](xref:bootstrapper). Add the following code in your `Program.cs` file:

```csharp
using System;
using System.Threading.Tasks;
using Statiq.App;

namespace MyGenerator
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateDefault(args)
        .RunAsync();
  }
}
```

This creates a default `Bootstrapper` instance and passes it the [command-line arguments](xref:command-line-interface) so it can process them with the `.CreateDefault(args)` call. Then it executes the specified command (from the command-line) during the final `.RunAsync()` call.

This example is all you need for a minimal, functioning Statiq Framework application. The only problem is that it doesn’t actually do anything. Let’s add one more step and process some Markdown files.

## Step 5: Add a Pipeline and Modules

Most functionality in Statiq Framework is provided by [pipelines](xref:pipelines-and-modules) and [modules](xref:about-modules). The [bootstrapper](xref:bootstrapper) has several mechanisms for [adding and defining pipelines](xref:adding-pipelines). For this last step lets add a quick pipeline to read Markdown files, render them, and write them back out to disk using some fluent methods to define a pipeline and add modules to it:

```csharp
using System;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Markdown;

namespace MyGenerator
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateDefault(args)
        .BuildPipeline("Render Markdown", builder => builder
            .WithInputReadFiles("*.md")
            .WithProcessModules(new RenderMarkdown())
            .WithOutputWriteFiles(".html"))
        .RunAsync();
  }
}
```

While Markdown support is part of Statiq Framework (and covered by the same MIT license), you'll also need to add the [Statiq.Markdown](https://www.nuget.org/packages/Statiq.Markdown) package to get access to the `RenderMarkdown` module used in the above code:

```
dotnet add package Statiq.Markdown --version x.y.z
```

That's because Statiq Framework is completely general and doesn't make any assumptions about the kind of generator you're building (unlike [Statiq Web](xref:web) which is somewhat opinionated and provides support for templates and more out of the box). Many other [Statiq Framework extensions](https://www.nuget.org/packages?q=statiq.) also exist and you should pick and choose which ones you need.

## Step 6: Run it!

Let the magic happen:

```
dotnet run
```

# Next Steps

[📖 Read the guide](xref:guide) to learn more about all the features of Statiq.

[💬 Use the Discussions repo](https://github.com/statiqdev/Discussions/discussions) for assistance, questions, and general discussion about all Statiq projects.

[🐞 File an issue](https://github.com/statiqdev/Statiq.Framework/issues) if you find a bug or have a feature request related to Statiq Framework.

# How It Works

<?! ^ _howitworks.md /?>