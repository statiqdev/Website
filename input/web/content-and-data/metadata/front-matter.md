Order: 3
---
Front matter is a common concept in static generators that lets you define metadata for a file in a file header.

A file that contains front matter might look like this:

``` txt
Title: Some Title
Description: This is a description.
Date: 5/25/2016
---
This is the content of the file.
```

An example of front matter usage would be using metadata to define tags for your blog posts. You could create a "Tags" metadata field in the front matter of your post file and then read that metadata later to create tag clouds, lists of similar posts, etc.

Front matter is delimited by a single line of three dashes following the front matter. A leading line of three dashes in addition to the trailing dashes is also supported for compatibility with other generators.

Currently Statiq Web supports defining front matter as YAML. Support for JSON and XML front matter is coming soon.