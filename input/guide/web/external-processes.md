Order: 1
Badge: Web
---
Statiq Web supports running external processes as part of execution. Processes can be defined to run at different stages during execution and can be run one, every time, or stay resident in the background. This provides a high degree of interoperability with other web stacks and existing tooling like the ecosystem of npm-based tools.

# Timing

There are four points during execution at which a process can be configured to run (these are defined in the `ProcessTiming` enum):

- `Initialization`: The process should be started only once before other processes on the first engine execution.
- `BeforeExecution`: The process should be started before each pipeline execution.
- `AfterExecution`: The process should be started after all pipelines have executed.
- `BeforeDeployment`: The process should be started after normal pipelines are executed and before deployment pipelines are executed. If no deployment pipelines are executed, this is effectively the same as `AfterExecution`.

# Concurrency

In addition to when the process is run, it's interaction with regard to other processes and the engine can also be configured.

A process can be defined as a background process which means that it won't block the engine from continuing with execution. The default non-background process mode will run processes in sequence before running the next process or continuing execution.

When in background mode, a process can also be configured to wait for the process to exit before continuing with the next process timing phase. In other words, a background process with the `BeforeExecution` timing that's also configured to wait for exit will run in the background during engine execution but once engine execution is complete the engine will wait for the process to exit before continuing with the `AfterExecution` process timing phase. This can be helpful when you have a process that should do background work but which must complete before starting deployment or other post-execution activities.

A process can also be configured to run when using the preview command only, when not using the preview command only, or regardless of whether the preview command was used. For example, a background process that watches for file changes and rebuilds assets may be defined when using the preview command but one that only does a single blocking build of resources may be used when not using the preview command.

All these options are available when configuring processes using the methods below.

# Configuring

Processes can be configured through the [bootstrapper](xref:bootstrapper):

- `ConfigureProcesses(...)`: Lets you configure processes using the `Processes` instance which manages them.
- `AddProcess(...)`: Adds a process with a specified `ProcessTiming`, file name, and arguments.
- `AddNonPreviewProcess(...)`: Adds a non-preview-only process with a specified `ProcessTiming`, file name, and arguments.
- `AddPreviewProcess(...)`: Adds a preview-only process with a specified `ProcessTiming`, file name, and arguments.
- `AddBackgroundProcess(...)`: Adds a background process with a specified `ProcessTiming`, file name, and arguments.
- `AddBackgroundNonPreviewProcess(...)`: Adds a background non-preview-only process with a specified `ProcessTiming`, file name, and arguments.
- `AddBackgroundPreviewProcess(...)`: Adds a background preview-only process with a specified `ProcessTiming`, file name, and arguments.
- `AddConcurrentProcess(...)`: Adds a background process that waits for exit with a specified `ProcessTiming`, file name, and arguments.
- `AddConcurrentNonPreviewProcess(...)`: Adds a non-preview-only background process that waits for exit with a specified `ProcessTiming`, file name, and arguments.
- `AddConcurrentPreviewProcess(...)`: Adds a preview-only background process that waits for exit with a specified `ProcessTiming`, file name, and arguments.

For more control, bootstrapper extension overloads that accept a `Func<IExecutionState, ProcessLauncher>` factory are also provided. This lets you define exactly how the process launcher should operate including setting an alternate working directory, specifying environment variables, etc. For example, to run `npm install` while ignoring errors (which are output to the standard error stream by npm and thus would be treated as errors in Statiq and fail the build), you can use:

```csharp
.AddProcess(ProcessTiming.Initialization, _ => new ProcessLauncher("npm", "install")
{
    LogErrors = false
})
```

Alternatively, the `Processes` instance is registered with the engine as a service and can be configured directly by getting the service from `IEngine.Services`.

Be mindful of when processes are running and where they're writing their own output, especially when using a process during preview commands. For example, if a process runs during preview using `AfterExecution` and writes to the `input` folder, it can cause an infinite loop of rebuilding the site because Statiq will see those `input` writes as changed files and trigger another execution. A good rule of thumb is, just like native Statiq pipelines, process should only write to the `output` folder or other non-input locations. Of course there are exceptions to everything, and if a process is running during `Initialization` for example, or isn't running during the preview command, writing to the `input` folder might be appropriate.