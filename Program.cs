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
                .RunAsync();
    }
}
