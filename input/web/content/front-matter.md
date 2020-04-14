Title: Front Matter
---
Front matter is a convenient way to define [metadata](/framework/concepts/metadata) for your files inline with their content.

Front matter is defined at the start of a file and is delimited from the rest of the file with a line consisting of a series of dashes: `---`. It's typically written as YAML (though support for more front matter formats such as JSON and XML is coming soon).

For example, a file that contains front matter might look like:

```txt
Title: Some Title
Description: This is a description.
Date: 5/25/2016
---
This is the content of the file.
```

