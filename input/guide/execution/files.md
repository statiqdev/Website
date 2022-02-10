Order: 4
Title: Files and Paths
---
Statiq uses an IO abstraction layer designed to provide flexibility and consistency when dealing with files, directories, and path information.

# Normalized Paths

Notional files and paths are generally represented in Statiq by the `NormalizedPath` struct. Think of it as a more standardized way of representing a path as a string. Paths can be either absolute (I.e., starting from the root of the file system) or relative. They can be easily joined together and otherwise manipulated.

# Virtual File System

File and directory classes (implementations of `IFile` and `IDirectory`) point directly to a potential file or directory within a given file system. They are usually obtained using a normalized path that points to them. Each provider implements their own file and directory classes as appropriate for that provider. File and directory implementations often provide functionality for reading and writing files, creating directories, and otherwise manipulating the file system.

The link between paths, file providers, and files and paths is managed by a virtual file system available through the [execution context](xref:execution#execution-context) as the `FileSystem` property. It stores all the root, input, and output paths and provides methods to join them with relative paths to get `IFile` and `IDirectory` instances.

## Root Path

The root path is an absolute path that acts as a starting point for all other relative paths. By default the root path is set to the working path on the underlying file system from where you execute Statiq.

## Input Paths

Statiq uses multiple input paths that together comprise a virtual aggregated set of input files. This lets us do things like specify a set of canonical input files to use for a theme but then selectively override the theme files by putting replacements in an alternate input path with higher precedence.

Input paths are stored in an ordered list. When checking for files, the paths at the end of the list take precedence over those at the start of the list. For example, if path "A" is at index 0, path "B" is at index 1, and they both have a file named "foo.md", the one from path "B" will be used. Further, all paths are aggregated so searching for files or evaluating [globbing](#globbing) expressions will consider all files and directories in all input paths. In the example above, getting all input files will result in a set of files from both path "A" and path "B" (with files of the same name from path "B" replacing those from path "A").

By default a single input path of `input` is set.

## Output Path

The output path is where Statiq will place output files by default. Note that many modules have the ability to manually specify an output path, so this behavior can be modified on a module by module basis.

By default the output path is set to `output`.

# Globbing

Globbing (or globs) is a particular syntax for specifying files or directories using wildcards and other path-based search criteria. Statiq uses a sophisticated globbing engine to give you a lot of flexibility when searching for files. Even better, the globbing engine works with any file provider, be it the local file system or something else.

To demonstrate, let's assume the following files exist in our file provider:
- /a/x.txt
- /a/b/x.txt
- /a/b/y.md
- /c/z.txt
- /d/x.txt

The globbing engine supports the following syntax:
- `*`

  This represents any number of characters at a specific depth. For example, `/*/x.txt` will find:
  - /a/x.txt
  - /d/x.txt

  Note that a wildcard can also be used in the file name. For example, `/*/*.txt` will find:
  - /a/x.txt
  - /c/z.txt
  - /d/x.txt
  
- `**`

  This represents any number of characters at multiple depths. For example, `/**/x.txt` will find:
  - /a/x.txt
  - /a/b/x.txt
  - /d/x.txt
  
- `{,}`

  This represents multiple expansions for the pattern. For example, `/**/{y,z}.*` will find:
  - /a/b/y.md
  - /c/z.txt
  
  Leaving the last option blank indicates any match at that position. For example, `/{a,}/**/x.txt` will find:
  - /a/x.txt
  - /a/b/x.txt
  - /d/x.txt
  
- `!`

  This represents exclusion and is useful in combination with multiple expansions. For example, `/**/{*,!x}.txt` will find:
  - /c/z.txt

Note that relative globbing patterns are often evaluated from the perspective of your `input` folder.

## Troubleshooting Globbing Patterns

Statiq provides the `glob` command to help troubleshoot globbing patterns, which has two subcommands: `glob eval` and `glob test`.

The `glob eval` command evaluates a globbing pattern against actual files on disk and returns all the files that match:

```
dotnet run -- glob eval <pattern> <path>
```

The `glob test` command tests a globbing pattern against a provided path and returns whether the pattern would match the path. The path does not have to be a real file or folder:

```
dotnet run -- glob test <pattern> <path>
```

# Testing

Because the new IO abstraction includes support for virtual file systems, it can be used to greatly simplify testing your custom modules by providing files that don't actually have to exist on disk. Several classes in the `Statiq.Testing` library are provided to help with this.
