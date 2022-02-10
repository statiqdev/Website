In Statiq Web and Statiq Docs, the Markdown engine is executed automatically for Markdown file types. In Statiq Framework you must use the `RenderMarkdown` module from the `Statiq.Markdown` package in your pipeline to render Markdown content.

# Pass Through Content

You can use the special "raw" language to output verbatim content that isn't subject to Markdown processing. This is helpful when you have raw HTML and other content that you want to pass-through the Markdown engine. While using HTML elements in Markdown also accomplishes a single goal, it has some limitations like breaking when new lines are included.

For example:

<?# Raw ?>
<?*
<pre class="language-txt"><code>
```raw
This
&lt;div&gt;content&lt;/div&gt;

will pass-through.
```
</code></pre>
?>
<?#/ Raw ?>