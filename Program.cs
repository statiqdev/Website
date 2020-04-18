using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace Statiqdev
{
    public static class Program
    {
        public static async Task<int> Main(string[] args) =>
            await Bootstrapper.Factory
                .CreateWeb(args)
                .AddSetting(Keys.Host, "statiq.dev")
                .AddSetting(Keys.LinksUseHttps, true)
                .AddSetting(
                    Keys.DestinationPath,
                    Config.FromDocument((doc, ctx) =>
                    {
                        // Only applies to the content pipeline
                        if (ctx.PipelineName == nameof(Statiq.Web.Pipelines.Content))
                        {
                            return doc.Source.Parent.Segments.Last().SequenceEqual("posts".AsMemory())
                                ? new NormalizedPath("blog").Combine(doc.GetDateTime(WebKeys.Published).ToString("yyyy/MM/dd")).Combine(doc.Destination.FileName.ChangeExtension(".html"))
                                : doc.Destination.ChangeExtension(".html");
                        }
                        return doc.Destination;
                    }))
                .AddSetting("EditLink", Config.FromDocument((doc, ctx) => new NormalizedPath("https://github.com/statiqdev/statiqdev.github.io/edit/develop/input").Combine(doc.Source.GetRelativeInputPath())))
                .AddShortcode("ChildPages", Config.FromDocument(doc =>
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine(@"<h4 class=""h-section mb-2"">Child Pages</h4>");
                    foreach (IDocument child in doc.GetChildren())
                    {
                        builder.AppendLine("<div>");
                        builder.AppendLine("<div class=\"p-3 mb-2 bg-light page-box\">");                        
                        builder.AppendLine($@"<h4><a href=""{child.GetLink()}"">{child.GetTitle()}</a></h4>");
                        string excerpt = child.GetString(Statiq.Html.HtmlKeys.Excerpt);
                        if (!string.IsNullOrEmpty(excerpt))
                        {
                            builder.AppendLine("<div class=\"font-size-sm\">");
                            builder.AppendLine(excerpt);
                            builder.AppendLine("</div>");
                        }
                        builder.AppendLine("</div>");
                        builder.AppendLine("</div>");
                    }
                    return builder.ToString();
                }))
                .AddPipelines()
                .RunAsync();
    }
}
