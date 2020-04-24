Order: 5
---
Shortcodes are small but powerful macros that can generate content or add metadata to your documents.

<?! Raw ?><?# Raw ?>

Statiq supports several templating engines like Markdown and Razor, but sometimes you need to generate some content regardless of the templating language you're using. Shortcodes are small text macros that can do big things and work across any templating engine. Statiq Framework and [Statiq Web](xref:about_statiq_web) come with several helpful shortcodes and best of all it's very easy to add your own.

# Using Shortcodes

Shortcodes are a special type of XML processing instruction delimited with `<?#` and `?>` (or `<?!` and `?>` depending on [processing phase](#processing-phases)). This syntax allows the shortcodes to "fall-through" most templating engines like Markdown since those languages ignore XML processing instructions. Every shortcode has a name, often has parameters, and can optionally contain content depending on how and what the shortcode renders.

A shortcode must always be closed with a `/` (similar to XML elements). If the shortcode contains no content, it can be self-closing by using a trailing slash: `<?# ShortcodeName /?>`. If the shortcode does contain content, it must be closed with a matching shortcode: `<?# ShortcodeName ?>content<?#/ ShortcodeName ?>`.

## Processing Phases

Shortcodes are generally evaluated in two phases: pre-rendering and post-rendering (though when writing your own pipelines you can execute the shortcode module whenever you like).

The pre-rendering phase is performed _before_ any other templating engines like Markdown or Razor. That means any output from a shortcode in this phase will be processed by those engines. For example, if you want to include a Markdown document in another Markdown document, you'll need to evaluate the `Include` shortcode during the pre-rendering phase (otherwise the Markdown processor would have already been run and your included Markdown document would never get processed). The pre-rendering phase is indicated with a `!` delimiter instead of a `#`: `<?! ShortcodeName /?>` or `<?! ShortcodeName ?>content<?!/ ShortcodeName ?>`.

The post-rendering phase happens _after_ all template engines like Markdown and Razor have been evaluated. This is appropriate for most shortcodes and is indicated with a `#` delimiter as described above.

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

## Bootstrapper

You can define shortcodes through the `Bootstrapper` which contains many `AddShortcode()` overloads for specifying shortcodes by delegates and other means.

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

To write a shortcode as a class, implement `IShortcode`. The shortcode name will generally be the same as the implementing class name.

# Process Shortcodes

To process shortcodes in your own pipelines, the `ProcessShortcodes` module is used to find shortcodes within a document and render them. It's generally an accepted pattern to use the `ProcessShortcodes` module after all other templates have been evaluated, but you can certainly use it earlier in your pipelines if you want to.

<?#/ Raw ?><?!/ Raw ?>