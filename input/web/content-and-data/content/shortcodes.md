Shortcodes are small but powerful macros that can generate content or add metadata to your documents.

<?! Raw ?><?# Raw ?>

Statiq supports several templating engines like Markdown and Razor, but sometimes you need to generate some content regardless of the templating language you're using. Shortcodes are small text macros that can do big things and work across any templating engine. [The shortcodes page in Statiq Framework](xref:shortcodes) discusses the basics of shortcodes, how to use them, and how to write your own.

# Statiq Web Shortcodes

Statiq Web comes with several web-specific shortcodes.

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

Renders an HTML table. The content of the shortcode contains the table with each row on a new line and each column separated by space. Enclose columns in quotes if they contain a space. Note that since the content of a shortcode may get processed by template engines like Markdown and the content of this shortcode should not be, you probably want to wrap the shortcode content in the special XML processing instruction that will get trimmed like `<?* ... ?>` so it "passes through" any template engines (see example below).

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
