Title: About Statiq Framework
BreadcrumbTitle: Framework
Xref: framework
---
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
dotnet add package Statiq.App --version 1.0.0-beta.9
```

Use whatever is the [most recent version of Statiq.App](https://www.nuget.org/packages/Statiq.App). The `--version` flag is needed while the package is pre-release.

## Step 4: Edit The Project File

This step is optional but is helpful to prevent certain kinds of problems later. Open the project file that was created in the previous step (for example, `MyGenerator.csproj`). Add the following to the `<PropertyGroup>` at the top:

```xml
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFramework>netcoreapp3.1</TargetFramework>
  <RunWorkingDirectory>$(MSBuildProjectDirectory)</RunWorkingDirectory>
  <DefaultItemExcludes>$(DefaultItemExcludes);output\**</DefaultItemExcludes>
</PropertyGroup>
```

The `<RunWorkingDirectory>` will ensure the working directory is equivalent to the project directory so if you run your console application from a different location it can still find the correct root and input folders.

The `<DefaultItemExcludes>` will help exclude the files generated in the `output` folder from the application compilation.

Also add the following `<ItemGroup>` elements to the file:

```xml
<ItemGroup>
  <Compile Remove="input\**" />
</ItemGroup>

<ItemGroup>
  <None Include="input\**">
    <CopyToOutputDirectory>Never</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

This will ensure files in the `input` directory don't get compiled or copied to the output folder. The reason we don't add the `input` folder to the `<DefaultItemExcludes>` above is because unlike the `output` folder, we want the `input` folder to show up in the Visual Studio file tree so we can edit input files.

If you use a different input folder or have added input folders, add those as well.

## Step 5: Create a Bootstrapper

There are several ways to create and configure an [engine](xref:execution#engine), but by far the easiest is to use the [bootstrapper](xref:bootstrapper):

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

## Step 6: Add a Pipeline and Modules

Most functionality in Statiq Framework is provided by [pipelines](xref:pipelines-and-modules) and [modules](xref:about-modules). The [bootstrapper](xref:bootstrapper) has several mechanisms for [adding and defining pipelines](xref:adding-pipelines). For this last step lets add a quick pipeline to read Markdown files, render them, and write them back out to disk using some fluent methods to define a pipeline and add modules to it:

# How It Works

Statiq is powerful because it combines a few simple building blocks that can be rearranged and used in limitless combinations. Think of it like LEGO® for static generation.

- Content and data can come from a variety of sources.

- **[Documents](xref:documents-and-metadata)** are created that each contain **[content](xref:content)** and **[metadata](xref:documents-and-metadata#about-metadata)**.

- The documents are processed by **[pipelines](xref:pipelines-and-modules)**.

- Each pipeline consists of one or more **[modules](xref:about-modules)** that manipulate the documents given to it by transforming, aggregating, filtering, or producing entirely new documents.

- The final output of each pipeline is made available to other pipelines and may be written to output files or deployed to hosting services.

<div><img src="/assets/flow.png" class="mw-100"></div>