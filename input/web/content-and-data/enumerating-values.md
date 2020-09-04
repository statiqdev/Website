Order: 7
---
Statiq Web has a nifty feature where you can declare a sequence of values to be enumerated within the [front matter](xref:web-front-matter) of a file and it will create additional documents for each of the values.

To use this feature, define a metadata key `Enumerate` with the values you want to create documents for. Each document will contain the original content and metadata along with an additional `Current` metadata value.

To illustrate how this works, consider an `apple.md` file with the following front matter:

```txt
Enumerate:
  - Red Delicious
  - Honeycrisp
  - Fuji
  - McIntosh
---
The current apple is <?#= Current /?>.
```

This will result in 4 documents, one for each of the apples. It uses the shorthand [`Meta`](xref:web-shortcodes#meta) shortcode syntax to output the name of the current enumerated value, but could also have used [Razor](xref:template-languages#razor) syntax or another templating format.

Unfortunately this file will also result in all four documents having the same destination path and being written to the same file on disk, overwriting each other. We need to add a [`DestinationFileName`](xref:web-settings#destinationfilename) document configuration value to specify the file name of each apple-specific document. Since the destination path also needs to access the "Current" metadata value that the enumeration produces, we'll have to use [computed values](xref:documents-and-metadata#about-metadata#computed-values):

```txt
Enumerate:
  - Red Delicious
  - Honeycrisp
  - Fuji
  - McIntosh
DestinationFileName: => $"apple-{Current}.html"
---
The current apple is <?#= Current /?>.
```

This works well to generate four pages for each of the apples, but what if we also want an index page with all of the apples? We can generate that as well by setting `EnumerateWithInput` to `true`. This will include the original input file without a `Current` value in addition to each of the documents from the enumerated values:

```txt
Enumerate:
  - Red Delicious
  - Honeycrisp
  - Fuji
  - McIntosh
EnumerateWithInput: true
DestinationFileName: >
  => Document.ContainsKey("Current") ? $"apple-{Document["Current"]}.html" : "apple.html"
---
@if (Document.ContainsKey("Current"))
{
    <p>The current apple is @Document.GetString("Current")</p>
}
else
{
    <p>All the yummy apples:</p>
    <ul>
        @foreach (string apple in @Document.GetList<string>("Enumerate"))
        {
            <li><a href="apple-@(apple.ToLower())">@apple</a></li>
        }
    </ul>
}
```

We need to change the `DestinationFileName` setting in this case since the [computed value](xref:documents-and-metadata#about-metadata#computed-values) will need to compile when a `Current` value isn't available (so it must be accessed through the `Document`). This example also uses [Razor](xref:template-languages#razor) syntax (and would presumably be named `apple.cshtml`) given the complexity of the logic. The [`If`](xref:web-shortcodes#if) and [`ForEach`](xref:web-shortcodes#foreach) shortcodes _could_ actually handle this in a [Markdown](xref:template-languages#markdown) file as well, but the syntax wouldn't be as easy to read for the purpose of this example.

<!-- TODO: Usage with the data pipeline - define apples with descriptions as .yml files -->