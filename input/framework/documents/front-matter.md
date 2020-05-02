Title: Front Matter
---
Front matter is a common concept in static generators and lets you define metadata for a file in a file header.

Typically, front matter is placed at the top of an input file and uses some sort of format (like YAML or JSON) to define key/value pairs. In Statiq, front matter can be extracted using the `ExtractFrontMatter` module. That module accepts child modules that process whatever content is contained in the front matter block, such as YAML or JSON. Front matter is often delimited from the actual file content by a series of dashes such as `—`, though an alternate delimiter can be specified in the module.

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