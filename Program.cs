using System;
using System.Threading.Tasks;
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
                .AddMappedInputPath("external/Statiq.Web/examples/Statiq.Web.Examples/input", "web/examples")
                .AddMappedInputPath("external/Statiq.Web/examples/Statiq.Web.Examples/input/Shared", "Shared") // Promote the partial views to the root Shared folder
                .RunAsync();
    }
}
