Badge: Web
---
Statiq can generate redirects for your content if it changes location.

To indicate that a redirect should be created for a content file, add the `RedirectFrom` [metadata](xref:documents-and-metadata) (in [front matter](xref:front-matter) or otherwise). The value should be the relative path(s) of the old location of the file. You can specify more than one old location and redirects will be created for all of them.

For example:

```txt
RedirectFrom:
- old-folder/file
- another-old-folder/file
---
This is my content.
```

By default a client-side redirect file using a [`META-REFRESH` header](https://en.wikipedia.org/wiki/Meta_refresh) in an HTML file will be output at the old location(s) pointing to the current output location. To disable the generation of client-side redirect files, set the [setting](xref:web-settings) `MetaRefreshRedirects` to `false`.

# Netlify

You can also create a [Netlify redirect file](https://docs.netlify.com/routing/redirects/#syntax-for-the-redirects-file) by [setting](xref:web-settings) `NetlifyRedirects` to `true`.