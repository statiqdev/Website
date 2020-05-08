Order: 2
---
One of the most powerful aspects of your static generator being a .NET application is that you have access to the full .NET development environment and tooling, including robust debugging support.

A Statiq application can be debugged easily by attaching a debugger to it. Additionally, if you start the application from within Visual Studio in debug mode, it can be debugged just like any other application.

The [command-line interface](xref:command-line-interface) also provides two flags to assist with debugging:

- `--debug`: Launches a debugger selection window to select (and launch) a debugger.
- `--attach`: Pauses execution at application start, prints out the current process ID, and waits for a debugger to attach.
