<!---
Order: 1
Badge: Docs
--->
By default, Statiq Docs is configured to look for source files in an `src` folder either one level up from your documentation site (I.e. if you've placed in a `docs` subfolder of your main repository) or directly under your documentation site. You can define alternate locations for your code, including directly pointing to assemblies, project files, or solution files.

# Using Settings

The following [settings](xref:settings) can be used to specify the location of your code.
They each accept one or more [globbing patterns](xref:files-and-paths#globbing) as either a string or collection of strings.
If any of these settings are relative,
their relative root is the [virtual file system input path(s)](xref:files-and-paths#input-paths)
which generally includes your `input` folder (as well as any other defined input folders).

- `SourceFiles`: Locates C# source files and compiles them to determine what symbols to document.
  - By default, the patterns `../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs` and `../../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs` are used which searches for all `*.cs` files at any depth under a `src` folder but not under `bin`, `obj`, `packages` or `Tests` folders. The first globbing pattern looks for `src` folders alongside the `input` folder and the second looks for `src` folder up a level alongside the folder of your whole Statiq Docs project (the patterns are initially rooted on the `input` folder).
  - If you want to define other ways of locating code such as project files or assemblies, and don't want source files to be located, set this settings to an empty string.
- `ProjectFiles`: Locates .NET project files and uses those to determine what symbols to document.
- `SolutionFiles`: Locates .NET solution files and uses the projects in those to determine what symbols to document.
- `AssemblyFiles`: Located assembly files and documents the symbols contained within.

# Using The Bootstrapper

The [bootstrapper](xref:bootstrapper) can be used to specify the location of your code. The following bootstrapper extensions are available and correspond to the settings above:

- `AddSourceFiles`
- `AddProjectFiles`
- `AddSolutionFiles`
- `AddAssemblyFiles`

For example:

```csharp
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Docs;

namespace MySite
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateDocs(args)
        .AddAssemblyFiles("assemblies/**/*.dll")
        .RunAsync();
  }
}
```