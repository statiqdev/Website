Order: 1
---
Directory metadata allows you to specify metadata that applies to all files in a given directory (and optionally all subdirectories).

By default, directory metadata files are named `_directory.*` where the extension is one of the supported data formats like `.json`, `.yaml`, or `.yml`. The file name(s) and applicable paths can be controlled by the `DirectoryMetadataFiles` [setting](xref:web-settings). The default value is `**/_{d,D}irectory.{json,yaml,yml}`.

Any metadata defined in a directory metadata file will be applied to all files in the same directory and all subdirectories by default. To turn off recursive application and only apply metadata to files in the same directory set `Recursive` to `false` in the directory metadata file. Directory metadata files closer to the file being applied take precedence over those higher up the folder tree.

Local metadata in the front matter of a file overrides any defined directory metadata.

Directory metadata support can be globally turned off by [setting](xref:web-settings) `ApplyDirectoryMetadata` to `false`.ß
