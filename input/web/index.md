Title: About Statiq Web
BreadcrumbTitle: Web
Xref: web
---
Statiq Web is a powerful static web site generation toolkit suitable for most use cases. It's built on top of [Statiq Framework](/framework) so you can always extend or customize it beyond those base capabilities as well.

<div class="alert alert-warning" role="alert">
  The Statiq Web documentation is being written as quickly as possible. You may currently find placeholder pages which will be completed with content soon.
</div>

# Quick Start

The easiest way to get started with Statiq Web is to install the [Statiq.Web](https://www.nuget.org/packages/Statiq.Web) package into a .NET Core console application and use the [bootstrapper](xref:bootstrapper) to configure everything.

## Step 1: Install .NET Core

Statiq Web consists of .NET Core libraries and [installing the .NET Core SDK](https://dot.net) is the only prerequisite.

## Step 2: Create a .NET Core Console Application

Create a new console application using the `dotnet` command-line interface:

```csharp
dotnet new console -o MySite
```

## Step 3: Install Statiq.Web

```csharp
dotnet add package Statiq.Web
```

## Step 4: Create a Bootstrapper

Creating a [bootstrapper](xref:bootstrapper) for Statiq Web initializes everything you’ll need to generate your web site. While you can certainly extend Statiq Web with new [pipelines](xref:defining-pipelines) or [custom modules](xref:writing-modules), you shouldn’t need to for most sites.

```csharp
using System;
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

Start adding content by creating [Markdown](xref:web-markdown) files in your "input" folder.

<div class="alert alert-info" role="alert">
  <strong>More features coming soon!</strong> Statiq Web is still under active feature development and a lot more functionality is coming soon including support for themes.
</div>
