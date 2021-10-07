Title: About Statiq Web
BreadcrumbTitle: Web
Xref: web
---
Statiq Web is a powerful static web site generation toolkit suitable for most use cases. It's built on top of [Statiq Framework](/framework) so you can always extend or customize it beyond those base capabilities as well.

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
dotnet add package Statiq.Web --prelease
```

The `--prerelease` flag is needed while the package is pre-release.

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

## Step 5: Add Some Content

Start adding content by creating [Markdown](xref:template-languages#markdown) files in your `input` folder, by default input folder is located in your project root.

To get something served you can add the following code as `index.md` file in your `input` folder.
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
This will by default create an `output` folder in your project folder if it doesn't exists and generate static web site content based on what's in your `input` folder.

Or run it and [preview the generated site](xref:web-preview-server):

```
dotnet run -- preview
```

This will generate content and serve your output folder over HTTP (i.e. `http://localhost:5080`).
![statiq preview](https://user-images.githubusercontent.com/1647294/89655186-0198b580-d8ca-11ea-9db5-bef9a9592161.png)

## Step 7: (Optional) Check out a Blog theme to see Statiq.Web in practice

Go to the [CleanBlog](https://github.com/statiqdev/CleanBlog) repo and import the theme into your project to start exploring a more functional example.
