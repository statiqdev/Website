Shortcodes are small but powerful macros that can generate content or add metadata to your documents.

<?! Raw ?><?# Raw ?>

Statiq supports several templating engines like Markdown and Razor, but sometimes you need to generate some content regardless of the templating language you're using. Shortcodes are small text macros that can do big things and work across any templating engine. Statiq Framework and [Statiq Web](xref:web) come with several helpful shortcodes and best of all it's very easy to add your own.

# Using Shortcodes

Shortcodes are a special type of XML processing instruction delimited with `<?#` and `?>` (or `<?!` and `?>` depending on [processing phase](#processing-phases)). This syntax allows the shortcodes to "fall-through" most templating engines like Markdown since those languages ignore XML processing instructions. Every shortcode has a name, often has parameters, and can optionally contain content depending on how and what the shortcode renders.

A shortcode must always be closed with a `/` (similar to XML elements). If the shortcode contains no content, it can be self-closing by using a trailing slash: `<?# ShortcodeName /?>`. If the shortcode does contain content, it must be closed with a matching shortcode: `<?# ShortcodeName ?>content<?#/ ShortcodeName ?>`.

## Nesting Shortcodes

Shortcodes can be nested, but keep in mind the current processing phase. If a shortcode result contains other shortcodes, those will be recursively processed but only if the nested shortcode is also part of the current phase.

## Shortcode Names

By convention shortcode names use CamelCase due to the fact that most of them come from .NET classes, which are also named with CamelCase. That said, the shortcode name is case-insensitive so if you prefer to use some other casing convention, you can.

## Parameters

Some shortcodes accept parameters which can be either positional, named, or a mixture of both depending on the shortcode. Shortcode parameters appear in the opening shortcode element and are delimited from the shortcode name and each other by whitespace. If the value of a parameter requires whitespace of it's own it can be enclosed in quotes. Parameter names do not need to appear in any specific order and are specified as `key=value` (or `key="value"` if the value contains whitespace).

**A single unnamed parameter value:**

``` txt
<?# ShortcodeName parameter-value /?>
```

**A single unnamed quoted parameter value:**

``` txt
<?# ShortcodeName "parameter value" /?>
```

**Multiple unnammed positional parameter values:**

``` txt 
<?# ShortcodeName "parameter 1" parameter2 "parameter value 3" /?>
```

**A single named parameter and value:**

``` txt
<?# ShortcodeName Foo=Bar /?>
```

**A single named parameter and quoted value:**

``` txt
<?# ShortcodeName Foo="Bar Baz" /?>
```

**A mixture of unnamed positional parameter values and named parameters:**

``` txt
<?# ShortcodeName "unnamed value" Foo=Bar /?>
```

Note that unnamed positional parameters almost always must appear before named parameters.

## Content

In addition to parameters, some shortcodes accept or expect content. Shortcode content goes between the opening and closing shortcode tag and is sent verbatim to the shortcode:

``` txt
<?# ShortcodeName "parameter 1" ?>
Here is
Some Shortcode
Content
<?#/ ShortcodeName ?>
```

Because shortcode content is just text in your file, it will be changed by any templating engine(s) before being processed. For example, if the shortcode about was part of a Markdown file, it would end up looking like this before being processed by the shortcode (notice the surrounding `<p>` that the Markdown engine added):

``` txt
<?# ShortcodeName "parameter 1" ?>
<p>Here is
Some Shortcode
Content</p>
<?#/ ShortcodeName ?>
```

Many times that behavior is desirable because we want to use the templating language for the shortcode content. Other times you may want the shortcode content to stay unprocessed by templating engines. In that case, you can surround the content inside a special XML processing instruction with the syntax `<?* ... ?>`. This works because like the shortcodes themselves, most templating engines will ignore XML processing instructions. The shortcode processor will remove the special wrapping XML processing instruction tags inside the content before processing.

For example, this:

``` txt
<?# ShortcodeName "parameter 1" ?>
<?*
Here is
Some Shortcode
Content
?>
<?#/ ShortcodeName ?>
```

Will not get an added `<p>` from the Markdown engine and instead will get processed by the shortcode as:

``` txt
<?# ShortcodeName "parameter 1" ?>
Here is
Some Shortcode
Content
<?#/ ShortcodeName ?>
```

# Writing Shortcodes

Shortcodes get passed the current [document](xref:documents-and-metadata), [execution context](xref:execution-context), any [parameters](#parameters), and any [content](#content) and returns a collection of `ShortcodeResult` objects that contains new content to add to the containing document in the place of the shortcode. The `ShortcodeResult` type is implicitly convertible from both a `string` and a `Stream`, so a shortcode implementation can just return whatever content object is appropriate.

## Bootstrapper

You can define shortcodes through the [bootstrapper](xref:bootstrapper) which contains many `AddShortcode()` overloads for specifying shortcodes by delegates and other means.

For example, if you add the following:

``` txt
.AddShortcode("Foo", (string x) => $"ABC{x}XYZ");
```

And then use it in a document like this:

``` txt
<?# Foo ?>123<?#/ Foo ?>
```

The output will be:

``` txt
ABC123XYZ
```

## As A Class

To write a shortcode as a class, implement `IShortcode`. The shortcode name will generally be the same as the implementing class name. Alternatively, several shortcode base classes are provided as a convenience:

- `Shortcode` is a base class for single-result asynchronous shortcodes.
- `SyncShortcode` is a base class for single-result synchronous shortcodes.
- `MultiShortcode` is a base class for multiple-result asynchronous shortcodes.
- `SyncMultiShortcode` is a base class for multiple-result synchronous shortcodes.

## Registering Your Shortcodes

Any custom shortcodes will need to be registered with the [engine](xref:execution#engine).

This can be done through the [bootstrapper](xref:bootstrapper) using its `AddShortcode<TShortcode>()` extensions. Note that the bootstrapper automatically registers all shortcode types in all referenced assemblies by default so you'll rarely need to manually register shortcodes when using the bootstrapper.

You can also register shortcodes directly with an engine using its `Shortcodes` property.

# Processing Shortcodes

Use the `ProcessShortcodes` module in your own pipelines to find shortcodes within a document and render them. It's generally an accepted pattern to use the `ProcessShortcodes` module after all other templates have been evaluated, but you can certainly use it earlier in your pipelines if you want to.

# Statiq Web Shortcodes <?^ WebBadge /?>

Statiq Web comes with several web-specific shortcodes and support multiple pre-defined processing phases for shortcodes.

## Processing Phases

Statiq Web process shortcodes at three points with different syntax:

- Pre-rendering: `<?! ShortcodeName /?>`

Performed _before_ any other templating engines like Markdown or Razor. That means any output from a shortcode in this phase will be processed by those engines. For example, if you want to include a Markdown document in another Markdown document, you'll need to evaluate the `Include` shortcode during the pre-rendering phase (otherwise the Markdown processor would have already been run and your included Markdown document would never get processed).

- Intermediate: `<?^ ShortcodeName /?>`

Performed after the primary rendering is done (for example, Markdown) but before any post-processing is performed (I.e. Razor). This is valuable when you want a shortcode to flow through the first processing engine like Markdown but it generates code like Razor instructions for the post-processing module(s).

- Post-rendering: `<?# ShortcodeName /?>`

The post-rendering phase happens _after_ all template engines like Markdown and Razor have been evaluated. This is appropriate for most shortcodes and is indicated with the default `#` delimiter as described above.

Of course, you can also continue to add your own pipelines and `ProcessShortcodes` modules in Statiq Web with whatever delimiter you want for further customization.

## Embed And oEmbed Support

[oEmbed is a format](https://oembed.com) for fetching the embedded representation of third-party content The `Embed` shortcode provides general-purpose oEmbed support by calling an oEmbed endpoint and rendering the embedded content:

```html
<?# Embed https://codepen.io/api/oembed https://codepen.io/gingerdude/pen/JXwgdK /?>
```

The `Embed` shortcode accepts the following arguments:

- `Endpoint`: The oEmbed endpoint.
- `Url`: The embeded URL to fetch an embed for.
- `Format`: An optional format to use ("xml" or "json").

[Many sites](https://oembed.com/#section7) have implemented oEmbed support and some site-specific shortcodes for some of them are also available:

### Giphy

Embeds a gif from Giphy:

```html
<?# Giphy excited-birthday-yeah-yoJC2GnSClbPOkV0eA /?>
```

The only argument is the ID of the gif which can be obtained from it's URL: the ID for the URL https://giphy.com/gifs/excited-birthday-yeah-yoJC2GnSClbPOkV0eA is `excited-birthday-yeah-yoJC2GnSClbPOkV0eA`.

### Gist

Embeds a gist from GitHub:

```html
<?# Gist 10a2f6e0186fa34b8a7b4bd7d436785d /?>
```

This shortcode accepts the following arguments:

- `Id`: The ID of the gist.
- `Username`: Contains the username the gist belongs to (optional).
- `File`: Specifies the file within the gist to embed (optional).

### Twitter

Embeds a Tweet from Twitter:

```html
<?# Twitter 123456789 /?>
```

This shortcode accepts the following arguments:

- `Id`: the ID of the Tweet. This can be found at the end of the URL when you copy a link to a Tweet.
- `HideMedia`: When set to `true`, links in a Tweet are not expanded to photo, video, or link previews.
- `HideThread`: When set to `true`, a collapsed version of the previous Tweet in a conversation thread will not be displayed when the requested Tweet is in reply to another Tweet.
- `Theme`: Optionally "light" or "dark". When set to "dark", the Tweet is displayed with light text over a dark background.
- `OmitScript`: When set to `true`, the `<script>` element that contains the Twitter embed JavaScript code will not be rendered (for example, if you're including it at the top of the page).

### YouTube

Embeds a video from YouTube:

```html
<?# YouTube u5ayTqlLWQQ /?>
```

The only argument is the ID of the video to embed.

## Other Shortcodes

### Link

Renders a link from the given path, using default settings or specifying overrides as appropriate:

```html
<?# Link "/foo/bar" /?>
```

This shortcode accepts the following arguments:

- `Path`: The path to get a link for.
- `IncludeHost`: If set to `true` the host configured in the output settings will be included in the link, otherwise the host will be omitted and only the root path will be included (default).
- `Host`: The host to use for the link.
- `Root`: The root of the link. The value of this argument is prepended to the path.
- `Scheme`: The scheme to use for the link (this will override the `UseHttps` argument).
- `UseHttps`: If set to `true`, HTTPS will be used as the scheme for the link.
- `HideIndexPages`: If set to `true`, "index.html" and "index.html" file names will be hidden.
- `HideExtensions`: If set to `true`, extensions will be hidden.
- `Lowercase`: If set to `true`, links will be rendered in all lowercase.

### Figure

Generates HTML5 `<figure>` elements:

```html
<?# Figure Src="/assets/statiq.png" ?>
Statiq Logo
<?#/ Figure ?>
```

Will result in the following HTML:

```html
<figure>
  <img src="/assets/statiq.png" />
  <figcaption>Statiq Logo</figcaption>
</figure>
```

This shortcode accepts the following arguments:

- `Src`: URL of the image to be displayed.
- `Link`: If the image needs to be hyperlinked, URL of the destination.
- `Target`: Optional `target` attribute for the URL if `Link` parameter is set.
- `Rel`: Optional `rel` attribute for the URL if `Link` parameter is set.
- `Alt`: Alternate text for the image if the image cannot be displayed.
- `Class`: The `class` attribute to apply to the `figure` element.
- `Height`: The `height` attribute of the image.
- `Width`: The `width` attribute of the image.

### Table

Renders an HTML table. The content of the shortcode contains the table with each row on a new line and each column separated by new lines. Enclose columns in quotes if they contain a space. Note that since the content of a shortcode may get processed by template engines like Markdown and the content of this shortcode should not be, you probably want to wrap the shortcode content in the special XML processing instruction that will get trimmed like `<?* ... ?>` so it "passes through" any template engines (see example below).

```html
<?# Table Class=table HeaderRows=1 ?>
<?*
Vehicle "Number Of Wheels"
Bike 2
Car 4
Truck "A Whole Lot"
?>
<?#/ Table ?>
```

Will result in the following HTML:

```html
<table class="table">
  <thead>
    <tr>
      <th>Vehicle</th>
      <th>Number Of Wheels</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>Bike</td>
      <td>2</td>
    </tr>
    <tr>
      <td>Car</td>
      <td>4</td>
    </tr>
    <tr>
      <td>Truck</td>
      <td>A Whole Lot</td>
    </tr>
  </tbody>
</table>
```

This shortcode accepts the following arguments:

- `Class`: The `class` attribute to apply to the `table` element.
- `HeaderRows`: The number of header rows in the table.
- `FooterRows`: The number of footer rows in the table.
- `HeaderCols`: The number of header columns to the left of the table.
- `HeaderClass`: The `class` attribute to apply to the `<thead>` element.
- `BodyClass`: The `class` attribute to apply to the `<tbody>` element.
- `FooterClass`: The `class` attribute to apply to the `<tfoot>` element.

<?#/ Raw ?><?!/ Raw ?>