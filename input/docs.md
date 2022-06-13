<!---
Title: About Statiq Docs
ShowInNavigation: false
ShowInSidebar: false
NoSidebar: true
Xref: docs
--->
```raw
@section Title { }

<div class="text-center mb-4">
    <img src="/assets/statiq-docs.svg" alt="Statiq Docs" class="w-50"></img>
</div>
```

Statiq Docs builds on [Statiq Web](xref:web) by adding support for .NET API documentation and other functionality that's important for documentation web sites.

# Quick Start

The easiest way to get started with Statiq Docs is to install the [Statiq.Docs](https://www.nuget.org/packages/Statiq.Docs) package into a .NET Core console application and use the [bootstrapper](xref:bootstrapper) to configure everything.

There's no `statiq.exe`. Unlike other static generators which ship as a self-contained executable, Statiq is a framework and as such it runs from within your own console application. This provides the greatest flexibility and extensibility and is one of the unique aspects of using Statiq.

## Step 1: Install .NET Core

Statiq Docs consists of .NET Core libraries and [installing the .NET Core SDK](https://dot.net) is the only prerequisite.

## Step 2: Create a .NET Core Console Application

Create a new console application using the `dotnet` command-line interface:

```csharp
dotnet new console --name MySite
```

## Step 3: Install Statiq.Docs

In same folder as your newly created project (*i.e. `MySite`*).

```csharp
dotnet add package Statiq.Docs --version x.y.z
```

Use whatever is the [most recent version of Statiq.Docs](https://www.nuget.org/packages/Statiq.Docs). The `--version` flag is needed while the package is pre-release.

## Step 4: Create a Bootstrapper

Creating a [bootstrapper](xref:bootstrapper) for Statiq Docs initializes everything you’ll need to generate your web site and API documentation. While you can certainly extend Statiq Docs with new [pipelines](xref:defining-pipelines) or [custom modules](xref:writing-modules), you shouldn’t need to for most documentation sites. Add the following code in your `Program.cs` file:

```csharp
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Docs;

namespace MySite
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateDocs(args)
        .RunAsync();
  }
}
```

## Step 5: Specify Your Code If Needed

By default, Statiq Docs is configured to look for source files in an `src` folder either one level up from your documentation site (I.e. if you've placed in a `docs` subfolder of your main repository) or directly under your documentation site. You can [define alternate locations](xref:specifying-code) for your code, including directly pointing to assemblies, project files, or solution files.

## Step 6: Add Some Content

Start adding content by creating [Markdown](xref:template-languages#markdown) files in your `input` folder (by default the `input` folder is located in your project root).

To get something started, you can add the following code as `index.md` file in your `input` folder.
```md
Title: My First Statiq page
---
# Hello World!

Hello from my first Statiq page.
```

## Step 7: Run it!

Let the magic happen:

```
dotnet run
```

This will create an `output` folder in your project folder by default (if it doesn't already exist) and generate static web site content based on what's in your `input` folder.

You can also [preview the generated site](xref:preview-server):

```
dotnet run -- preview
```

This will generate content and serve your `output` folder over HTTP (i.e. `http://localhost:5080`).
![statiq preview](https://user-images.githubusercontent.com/1647294/89655186-0198b580-d8ca-11ea-9db5-bef9a9592161.png)

# Next Steps

[🎨 Download a theme](xref:themes) like [Docable](https://github.com/statiqdev/Docable).

[📖 Read the guide](xref:guide) to learn more about all the features of Statiq.

[💬 Use the Discussions](https://github.com/orgs/statiqdev/discussions) for assistance, questions, and general discussion about all Statiq projects.

[🐞 File an issue](https://github.com/statiqdev/Statiq.Docs/issues) if you find a bug or have a feature request related to Statiq Docs.

# How It Works

<?! ^ _howitworks.md /?>

Statiq Docs includes pipelines, modules, and other functionality related to generating documentation sites out of the box. 