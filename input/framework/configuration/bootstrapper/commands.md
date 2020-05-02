Order: 2
---
The [command-line commands](xref:command-line-interface) are fully configurable which allows you to extend or replace the behavior of your generator with whatever you want.

Statiq uses the excellent [Spectre.Cli](https://github.com/spectresystems/spectre.cli) library to manage command-line parsing. This allows commands to be defined as strongly-typed classes with typed sets of command-line arguments and options. It also automatically generates help text when the user uses the `--help` argument.

To define a new command, implement the `Spectre.Cli.AsyncCommand<TSettings>` interface (where `TSettings` is the type that contains command-line arguments and options). You can also derive your command class from `BaseCommand<TSettings>` which adds default logging and debugging command-line arguments to your command.

By default the [bootstrapper](xref:bootstrapper) will add all command types from the entry assembly. You can also use the bootstrapper to add specific commands using the various `AddCommand` and `AddCommands` fluent methods.

In addition, new commands can be added directly from the bootstrapper:

- `AddPipelineCommand` methods allow you to add a command that executes specific pipelines.
- `AddDelegateCommand` methods allow you to add a command using a delegate.