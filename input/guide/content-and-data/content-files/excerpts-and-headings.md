Badge: Web
---
Statiq Web attempts to extract an excerpt and headings from your content and stores them in metadata.

# Excerpt

The excerpt can be used when you want to display a short amount of content for a document without displaying the whole document. It's useful for [archives](xref:archives), [feeds](xref:feeds), or any other time you want to show several documents in one view.

By default the excerpt is taken from the first `<p>` element it finds. Alternatively, you can control where the excerpt ends by inserting an excerpt separator contained in your HTML (either `excerpt` or `more`). All `<p>` content up to the separator will be used.

For example:

```html
<html>
  <head>
  ...
  </head>
  <body>
    <div id="header">...</div>
    <div id="content">
      <p>
        Fruit keeps you healthy.
        <!-- excerpt -->
        Different kinds of fruit includes apples, oranges, etc.
    </div>
  </body>
</html>
```

Will result in an excerpt of:

```html
<p>Fruit keeps you healthy.</p>
```

The excerpt is stored in an `Excerpt` metadata value.

# Headings

All headings for a document are automatically gathered and represented by documents stored in the `Headings` metadata value. A new document is created for each heading. The new heading documents contain metadata with the level of the heading, the children of the heading (the following headings with one deeper level), and optionally the heading content which is also set as the content of each heading document. The heading documents have the following metadata:

- `Level`: The level of the heading.
- `HeadingId`: The `id` attribute of the heading if it has one.

The heading documents can be used to create a table-of-contents, an "on this page" sidebar, or any other use cases where knowing the headings in a document might be helpful.