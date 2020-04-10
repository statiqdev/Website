using Statiq.Common;
using Statiq.Core;
using Statiq.Web.GitHub;

namespace Statiqdev
{
    public class DeploySite : Pipeline
    {
        public DeploySite()
        {
            Deployment = true;

            OutputModules = new ModuleList
            {
                new DeployGitHubPages("statiqdev", "statiqdev.github.io", Config.FromSetting<string>("GITHUB_TOKEN")).ToBranch("master")
            };
        }
    }
}
