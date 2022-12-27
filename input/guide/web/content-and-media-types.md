Badge: Web
---
Statiq Web has a variety of ways of reading, writing, and working with different kinds of files.

There are three broad categories of files in Statiq Web:
- [Assets](xref:assets) which are either copied to the output folder directly or transformed during copying. Examples include images, standalone JavaScript, CSS stylesheets, and Less or Sass files.
- [Content](xref:content-files) which typically output as HTML files, directly or after one or more [templates](xref:templates) are applied. Examples include Markdown, Razor, and HTML files.
- [Data](xref:data-files) which may or may not be output and can be used when applying a template to content files. Examples include JSON, YAML, and XML files.

Files should be placed in the `input` folder (or a sub-folder) and the `InputFiles` [setting](xref:web-settings) controls what files from the input folder are loaded. It's set to `**/{!_,}*` by default which loads all files in any input directory unless it starts with an underscore `_`. You can always redefine the `InputFiles` [setting](xref:web-settings) to whatever [globbing pattern(s)](xref:files-and-paths#globbing) you want, but if you just need to include explicit files that would otherwise be excluded (I.e. ones that start with an underscore), you can use the `AdditionalIntputFiles` [setting](xref:web-settings) to do so.

The category of a file, and thus what operations are performed on it, is determined by its media type (which is inferred from its file extension). Unless the file extension is recognized as a content or data file such as Markdown, Razor, JSON, or YAML, the file is considered an asset file. The category can be changed by setting `ContentType` for the document. For example, you can treat a JSON file which would normally be interpreted as a data file and processed as JSON as an asset file that should be copied to the output folder directly by setting `ContentType` to `Asset` via something like a [sidecar file](xref:sidecar-files).

Within a given category, the media type of a file determines what [templates](xref:templates) and other processing will be performed on the file. The media type is typically determined by file extension, but you can also change the media type of a file by changing the `MediaType` setting. This can often be useful in combination with the `ContentType` setting. For example, if you have a JSON file that you want to treat as data but it has a `.css` extension for some reason, set `ContentType` to `Data` _and_ `MediaType` to `application/json`. This will treat the file as if it had a `.json` extension in the first place.

You can also change the `ContentType` and `MediaType` for entire directories using [directory metadata](xref:directory-metadata). For example, if your input folder contains a subfolder like `node_modules` that you want to copy wholesale to the output folder, but some of the files contained within the `node_modules` folder would be processed by either asset, content, or data [templates](xref:templates), you can essentially treat every file as if it were a text file to be copied directly to the output folder by creating a `_directory.yaml` file inside your `node_modules` folder with the following content:

```yaml
ContentType: Asset
MediaType: text/plain
```

Note that to avoid reading every single file and parsing them for front matter, only existing content file types can use front matter to initially change their `ContentType` or `MediaType` values. Other file types need to use [directory metadata](xref:directory-metadata) or [sidecar](xref:sidecar-files) files to set their media type to something other than their extension.

Both [content files](xref:content-files) and [data files](xref:data-files) support functionality like [directory metadata](xref:directory-metadata) files, [sidecar](xref:sidecar-files) files, and [front matter](xref:front-matter). The main difference is that [templates](xref:templates) like [Markdown](xref:template-languages#markdown) and [Razor](xref:template-languages#razor) are processed for content files whereas data files are parsed as data formats like JSON and YAML and add to the [metadata of a document](xref:documents-and-metadata).

Features like [archives](xref:archives) and [feeds](xref:feeds) also let you work with content and data and produce different kinds of outputs from them.