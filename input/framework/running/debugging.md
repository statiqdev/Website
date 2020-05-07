Order: 2
---
One of the most powerful aspects of your static generator being a .NET application is that you have access to the full .NET development environment and tooling, including robust debugging support.

A Statiq application can be debugged easily by attaching a debugger to it. Additionally, if you start the application from within Visual Studio in debug mode, it can be debugged just like any other application.

The [command-line interface](xref:command-line-interface) also provides a helpful `--attach` argument you can use to pause execution at application start, print out the current process ID, and wait for a debugger to attach. Use this argument when you want to debug a Statiq application from the command line.