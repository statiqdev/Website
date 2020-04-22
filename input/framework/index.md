Title: About Statiq Framework
---
Statiq Framework is a powerful static generation framework that can be used to create custom static generation applications. While many users will find that [Statiq Web](/web) or [Statiq Docs](/docs) have enough functionality built-in, crafting a custom static generator with Statiq Framework provides the most flexibility.

# Quick Start

The easiest way to get started with Statiq Framework is to install the [Statiq.App](https://www.nuget.org/packages/Statiq.App) package into a .NET Core console application and use the [bootstrapper](/framework/usage/bootstrapper) to configure everything.

## Step 1: Install .NET Core

Statiq Framework consists of .NET Core libraries and [installing the .NET Core SDK](https://dot.net) is the only prerequisite.

## Step 2: Create a .NET Core Console Application

Create a new console application using the `dotnet` command-line interface:

```csharp
dotnet new console -o MyGenerator
```

## Step 3: Install Statiq.App

```csharp
dotnet add package Statiq.App
```

## Step 4: Create a Bootstrapper

There are several ways to create and configure an [engine](/framework/concepts/execution#engine), but by far the easiest is to use the [Bootstrapper](/framework/usage/bootstrapper):

```csharp
using System;
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

This creates a default `Bootstrapper` and passes it the [command-line arguments](/framework/usage/bootstrapper#command-line) so it can process them with the `.CreateDefault(args)` call. Then it executes the specified command (from the command-line) during the final `.RunAsync()` call.

This example is all you need for a minimal, functioning Statiq Framework application. The only problem is that it doesn’t actually do anything. Let’s add one more step and process some Markdown files.

## Step 5: Add a Pipeline and Modules

Most functionality in Statiq Framework is provided by [pipelines](/framework/concepts/pipelines) and [modules](/framework/concepts/modules). The `Bootstrapper` has several mechanisms for [defining pipelines](/framework/usage/bootstrapper#defining-pipelines). For this last step lets add a quick pipeline to read Markdown files, render them, and write them back out to disk using some fluent methods to define a pipeline and add modules to it:

TODO

<?# ChildPages /?>