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
                .AddSetting("EditLink", Config.FromDocument((doc, ctx) => "https://github.com/statiqdev/statiqdev.github.io/edit/develop/input/" + doc.Source.GetRelativeInputPath()))
                .AddSetting(SiteKeys.NoChildPages, Config.FromDocument(doc => doc.Destination.Segments[0].SequenceEqual("blog".AsMemory())))
                .AddPipelines()
                .RunAsync();
    }
}
