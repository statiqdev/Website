Title: Command-Line Interface
Order: 1
---
Statiq provides a number of default commands and arguments to execute and control your pipeline execution.

You can run your Statiq application directly using [`dotnet run`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run) but you need to [delimit arguments for your Statiq application](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run#options) with a `--` between the `dotnet run` command and the arguments you want to pass to your Statiq application. Anything after the `--` will be passed to Statiq.

In general, calling `dotnet run` will compile your application, run the default pipelines, and generate output.

# Getting Help

You can view help for any command with the `--help` option:

```txt
dotnet run -- --help

USAGE:
    DotnetFoundationWeb.dll [pipelines] [OPTIONS]

ARGUMENTS:
    [pipelines]    The pipeline(s) to execute

OPTIONS:
    -h, --help                  Prints help information
    -l, --log-level <LEVEL>     Sets the minimum log level ("Critical", "Error", "Warning", "Information", "Debug", "Trace", "None")
        --attach                Pause execution at the start of the program until a debugger is attached
    -f, --log-file <LOGFILE>    Log all messages to the specified log file
    -s, --setting <SETTING>     Specifies a setting as a key=value pair (the value can be omited)
    -i, --input <PATH>          The path(s) of input files, can be absolute or relative to the current folder
    -o, --output <PATH>         The path to output files, can be absolute or relative to the current folder
        --noclean               Prevents cleaning of the output path on each execution
        --nocache               Prevents caching information during execution (less memory usage but slower execution)
        --stdin                 Reads standard input at startup and sets ApplicationInput in the execution context
        --serial                Executes pipeline phases and modules in serial
    -r, --root                  The root folder to use
    -n, --normal                Executes normal pipelines as well as those specified

COMMANDS:
    pipelines    Executes the specified pipelines
    deploy       Executes deployment pipelines
```

# Defining Commands

Additional commands can be [defined using a bootstrapper](xref:commands).