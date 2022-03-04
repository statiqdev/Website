Title: Front Matter
---
Front matter is a common concept in static generators that lets you define metadata for a file in a file header.

Typically, front matter is placed at the top of an input file and uses some sort of format (like YAML or JSON) to define key/value pairs. In Statiq, front matter can be extracted using the `ExtractFrontMatter` module. That module accepts child modules that process whatever content is contained in the front matter block, such as YAML or JSON.

By default, front matter is delimited by a single line of three dashes following the front matter. A leading line of three dashes in addition to the trailing dashes is also supported for compatibility with other generators. This is customizable in the `ExtractFrontMatter` module, including the ability to specify one or more regular expressions to use for identifying front matter.

A file that contains front matter might look like this:

``` txt
Title: Some Title
Description: This is a description.
Date: 5/25/2016
---
This is the content of the file.
```

An example of front matter usage would be using metadata to define tags for your blog posts. You could create a "Tags" metadata field in the front matter of your post file and then read that metadata later to create tag clouds, lists of similar posts, etc.

Practically, the `ParseYaml` module is usually used as the child of the `ExtractFrontMatter` module. However, like most things in Statiq Framework this is designed to be flexible. You could process any type of front matter (JSON, etc.) with this setup by specifying different child modules.

# Front Matter Style <?# WebBadge /?>

In Statiq Web you don't need to configure the `ExtractFrontMatter` module and front matter is identified and extracted automatically for all input documents. Statiq Web also supports additional front matter definition syntax in addition to the triple-dash style described above. This allows for the greatest flexibility in how you define front matter.

The following styles of front matter are supported in Statiq Web:
- `---` (triple-dash, with or without a leading line of dashes).
- `/*-` ... `-*/` (C-style comment blocks, notice the use of one or more `-` to indicate this is a front matter block).
- `<!---` ... `--->` (HTML comment blocks, notice the use of three or more `-` to indicate this is a front matter block).
- `@*-` ... `-*@` ([Razor](xref:razor) comment blocks, notice the use of one or more `-` to indicate this is a front matter block).

You can also customize the front matter styles using the following settings:
- `FrontMatterRegexes`: Defines the default set of regular expressions  used for identifying front matter. Changing this setting will overwrite the defaults above.
- `AdditionalFrontMatterRegexes`: Defines additional regular expressions used for identifying front matter in addition to the defaults described above.