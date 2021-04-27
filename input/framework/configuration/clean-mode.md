Order: 4
---
Statiq has a few different options for how files in the [output path](xref:files-and-paths#output-path) are deleted on each engine execution.

The different settings are defined in the `CleanMode` enum:

- `Self`: Cleans the entire output folder before the initial execution, then cleans only those files written or copied during the previous execution before each following execution.
- `None`: Does not clean the output folder before each execution.
- `Full`: Cleans the entire output folder before each execution.
- `Unwritten`: Cleans files after each execution that were written or copied during the previous execution but not during the current execution. This mode also uses content hashing and file attributes to avoid copying files when there's already a duplicate file in the output folder.

As of version Statiq Framework 1.0.0-beta.41 and Statiq Web 1.0.0-beta.27 the default mode is `Unwritten`. This mode provides a significant performance benefit because not every file needs to be written to disk on every execution (disk I/O operations are expensive). The mode can be changed using the `--clean-mode` [command-line argument](xref:command-line-interface) or by setting a "CleanMode" [setting](xref:settings).