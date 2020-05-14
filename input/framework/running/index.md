Order: 6
Title: Running Your Application
---
Once [pipelines and modules](xref:pipelines-and-modules) are defined and you’ve created your content, you can run your generator application.

# dotnet run

You can run your Statiq application directly using [`dotnet run`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run) but you need to [delimit arguments for your Statiq application](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run#options) with a `--` between the `dotnet run` command and the arguments you want to pass to your Statiq application. Anything after the `--` will be passed to Statiq.

In general, calling `dotnet run` will compile your application, run the default pipelines, and generate output. Statiq Framework includes [a variety of commands and arguments](xref:command-line-interface) you can use to control execution.

# dotnet publish

Alternativly you can publish your application using [`dotnet publish`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish). This will create an executable for your generator that you can run without compilation. For more information see [Publish .NET Core apps with the .NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli).