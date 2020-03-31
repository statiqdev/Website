using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace Statiq.Dev
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
                    Config.FromDocument(doc => doc.Source.Parent.Segments.Last().SequenceEqual("posts".AsMemory())
                        ? new NormalizedPath("blog").Combine(doc.GetDateTime(WebKeys.Published).ToString("yyyy/MM/dd")).Combine(doc.Destination.FileName.ChangeExtension(".html"))
                        : doc.Destination.ChangeExtension(".html")))
                .AddSetting("EditLink", Config.FromDocument((doc, ctx) => new NormalizedPath("https://github.com/statiqdev/statiq.dev/edit/master/input").Combine(doc.Source.GetRelativeInputPath())))
                .AddShortcode("ChildPages", Config.FromDocument(doc =>
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine(@"<h4 class=""h-section"">Child Pages</h4>");
                    foreach (IDocument child in doc.GetChildren())
                    {
                        builder.AppendLine($@"<h5><a href=""{child.GetLink()}"">{child.GetTitle()}</a></h5>");
                        string description = child.GetString("Description");
                        if (!string.IsNullOrEmpty(description))
                        {
                            builder.AppendLine($@"<p>{description}</p>");
                        }
                    }
                    return builder.ToString();
                }))
                .RunAsync();
    }
}
