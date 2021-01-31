Statiq Web provides a number of ways to link to documents.

Given a document you can generate a relative link to it using the [`GetLink()` extension methods](xref:linking-to-documents) (for example, in a [Razor](xref:template-languages#razor) template):

```html
<a href="@Document.GetLink()">My Link</a>
```

A [Razor](xref:template-languages#razor) [HTML helper](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.viewfeatures.htmlhelper) called `DocumentLink()` is also available that will create an entire link element:

```txt
@Html.DocumentLink(document);
```

A number of overloads exist to control things like link text:

```txt
@Html.DocumentLink(document, "My Link");
```

# Cross References

Statiq Web also provides an alternate way of linking to documents via simple cross references that doesn't require you to have a document instance or know the document path. Cross references are indicated with a `xref:` URI scheme and use a special identifier to locate documents.

Every document gets an `Xref` value. By default this is derived from the document title (which itself is derived from the file name if a `Title` metadata value isn't provided). The default `Xref` value is the document title with spaces replaced by a `-`. For example, a document titled "Blog Posts" would have an `Xref` value of `Blog-Posts`. You can override the `Xref` value by specifying an alternate one in metadata.

If a link contains an `xref:` scheme, all documents will be searched for a matching `Xref` value and a link will be created to the matching document. If more than one document matches the requested identifier an error will be generated, in which case you should provide an alternate `Xref` metadata value for one of the target documents to remove the ambiguity. Identifiers are also case-insensitive, so `xref:blog-posts` will match a document with an `Xref` value of `Blog-Posts`.

Because the matching happens at the very end of the generation process, cross reference links can be used anywhere. For example, they can be used in Markdown documents like:

```md
This is a [cross reference](xref:blog-posts) to a document.
```

Likewise, they can be used in plain HTML:

```html
This is a <a href="xref:blog-posts">cross reference</a> to a document.
```

Cross referencing is especially useful when refactoring content because existing links will continue to point to a document's new location even if it moves around.

## Changing The Default Cross Reference Name

The default `xref` value is computed by a [global setting](xref:web-settings) that gets inherited by every document. You can change how all or some documents compute their `xref` values by changing this setting.

For example, to use only the file name instead of the title when computing an `xref` you can add this to the [bootstrapper](xref:bootstrapper):

```csharp
.AddSetting(WebKeys.Xref, Config.FromDocument(doc => doc.Destination.FileNameWithoutExtension.Replace(' ', '-')))
```

Alternatively, if you just want to change it for a subset of files, you can use something like inherited [directory metadata](xref:web-directory-metadata) (in `_directory.yaml`):

```txt
Xref: => Config.FromDocument(doc => doc.Destination.FileNameWithoutExtension.Replace(' ', '-'))
```
