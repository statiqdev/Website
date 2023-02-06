Title: About Statiq Web
ShowInNavigation: false
ShowInSidebar: false
NoSidebar: true
Xref: web
---
```raw
@section Title { }

<div class="text-center mb-4">
    <img src="/assets/statiq-web.svg" alt="Statiq Web" class="w-50"></img>
</div>
```

Statiq Web is a powerful static web site generation toolkit suitable for most use cases. It's built on top of [Statiq Framework](xref:framework) so you can always extend or customize it beyond those base capabilities as well.

# Quick Start

The easiest way to get started with Statiq Web is to install the [Statiq.Web](https://www.nuget.org/packages/Statiq.Web) package into a .NET Core console application and use the [bootstrapper](xref:bootstrapper) to configure everything.

There's no `statiq.exe`. Unlike other static generators which ship as a self-contained executable, Statiq is a framework and as such it runs from within your own console application. This provides the greatest flexibility and extensibility and is one of the unique aspects of using Statiq.

## Step 1: Install .NET Core

Statiq Web consists of .NET Core libraries and [installing the .NET Core SDK](https://dot.net) is the only prerequisite.

## Step 2: Create a .NET Core Console Application

Create a new console application using the `dotnet` command-line interface:

```csharp
dotnet new console --name MySite
```

## Step 3: Install Statiq.Web

In same folder as your newly created project (*i.e. `MySite`*).

```csharp
dotnet add package Statiq.Web --version x.y.z
```

Use whatever is the [most recent version of Statiq.Web](https://www.nuget.org/packages/Statiq.Web). The `--version` flag is needed while the package is pre-release.

## Step 4: Create a Bootstrapper

Creating a [bootstrapper](xref:bootstrapper) for Statiq Web initializes everything you’ll need to generate your web site. While you can certainly extend Statiq Web with new [pipelines](xref:defining-pipelines) or [custom modules](xref:writing-modules), you shouldn’t need to for most sites. Add the following code in your `Program.cs` file:

```csharp
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Web;

namespace MySite
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateWeb(args)
        .RunAsync();
  }
}

```

Alternatively if you're using .NET 6 and have `<ImplicitUsings>enable</ImplicitUsings>` in your project file,
you can make use of top-level statements and implicit usings to simplify your `Program.cs` file
and this is all that's needed:

```csharp
return await Bootstrapper
  .Factory
  .CreateWeb(args)
  .RunAsync();
```

## Step 5: Add Some Content

Start adding content by creating [Markdown](xref:template-languages#markdown) files in your `input` folder (by default the `input` folder is located in your project root).

To get something started, you can add the following code as `index.md` file in your `input` folder.
```md
Title: My First Statiq page
---
# Hello World!

Hello from my first Statiq page.
```

## Step 6: Run it!

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

[🎨 Download a theme](xref:themes) like [CleanBlog](https://github.com/statiqdev/CleanBlog).

[📖 Read the guide](xref:guide) to learn more about all the features of Statiq.

[💬 Use the Discussions](https://github.com/orgs/statiqdev/discussions) for assistance, questions, and general discussion about all Statiq projects.

[🐞 File an issue](https://github.com/statiqdev/Statiq.Web/issues) if you find a bug or have a feature request related to Statiq Web.

# How It Works

<?! ^ _howitworks.md /?>

Statiq Web includes pipelines, modules, and other functionality related to generating web sites out of the box. 