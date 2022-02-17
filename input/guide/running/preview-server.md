Badge: Web
Order: 3
---
Statiq Web comes with a built-in preview server to help you while developing your layout and content.

You can run the preview server with the `preview` command which will build the site and launch the server:

```txt
dotnet run -- preview
```

You can also see help for the preview command by typing:

```txt
dotnet run -- preview --help
```

```txt
USAGE:
    MyGenerator.dll preview [pipelines] [OPTIONS]

ARGUMENTS:
    [pipelines]    The pipeline(s) to execute

OPTIONS:
    -h, --help                   Prints help information
    -l, --log-level <LEVEL>      Sets the minimum log level ("Critical", "Error", "Warning", "Information", "Debug", "Trace", "None")
        --attach                 Pause execution at the start of the program until a debugger is attached
        --debug                  Allows you to select a debugger to attach
    -f, --log-file <LOGFILE>     Log all messages to the specified log file
    -s, --setting <SETTING>      Specifies a setting as a key=value pair (the value can be omited)
    -i, --input <PATH>           The path(s) of input files, can be absolute or relative to the current folder
    -o, --output <PATH>          The path to output files, can be absolute or relative to the current folder
        --noclean                Prevents cleaning of the output path on each execution
        --nocache                Prevents caching information during execution (less memory usage but slower execution)
        --stdin                  Reads standard input at startup and sets ApplicationInput in the execution context
        --serial                 Executes pipeline phases and modules in serial
    -r, --root                   The root folder to use
    -n, --normal                 Executes normal pipelines as well as those specified
        --port <PORT>            Start the preview web server on the specified port (default is 5080)
        --force-ext              Force the use of extensions in the preview web server (by default, extensionless URLs may be used)
        --virtual-dir <PATH>     Serve files in the preview web server under the specified virtual directory
        --content-type <TYPE>    Specifies additional supported content types for the preview server as extension=contenttype
        --no-watch               Turns off watching the input folder(s) for changes and rebuilding
        --no-reload              Turns off LiveReload support after changes
```

# Configuration

You can set the port to use for the preview server with the `--port` option:

```txt
dotnet run -- preview --port 1234
```

There are also several other options that can be set (see the help output above).

# Live Reload

When building in preview mode, Statiq will inject a [LiveReload](http://livereload.com) script into your generated output and will trigger reload
on rebuilds. This will immediatly refresh the page you're viewing with new content (you don't have to hit the refresh button).
Sometimes this gets disconnected and a quick browser refresh will reconnect LiveReload support.

# Serving Without Build

You can also launch the preview server and serve the contents of the `output` directory without rebuilding the site
using the `serve` command:

```txt
dotnet run -- serve
```