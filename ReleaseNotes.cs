using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web.GitHub;
using Statiq.Web.Modules;

namespace Statiqdev
{
    public class ReleaseNotes : Pipeline
    {
        public ReleaseNotes()
        {
            InputModules = new ModuleList
            {
                // TODO: Add credentials once ReadGitHub takes a config for that
                new ReadGitHub(async github =>
                    (await GetReleaseNotesAsync(github, "Statiq.Framework"))
                        .Concat(await GetReleaseNotesAsync(github, "Statiq.Web"))
                        .Concat(await GetReleaseNotesAsync(github, "Statiq.Docs")))
            };

            ProcessModules = new ModuleList
            {
                // Need to replace "@" for Razor and "<?" because some of the release notes reference shortcode syntax
                new SetContent(Config.FromDocument(doc => doc.GetString(nameof(ReleaseNote.Body)).Replace("@", "@@").Replace("<?", "&lt;?"))),
                new SetMetadata(Keys.Title, Config.FromDocument(doc => $"{doc[nameof(ReleaseNote.Project)]} Release {doc[nameof(ReleaseNote.Name)]}")),
                new SetDestination(Config.FromDocument(doc =>
                    new NormalizedPath($"blog/{doc.GetDateTimeOffset(nameof(ReleaseNote.PublishedAt)).ToString("yyyy/MM/dd")}/{doc[nameof(ReleaseNote.Project)]}-{doc[nameof(ReleaseNote.Name)]}.html")
                        .OptimizeFileName()))

                // TODO: 
            };

            PostProcessModules = new ModuleList
            {
                new ProcessTemplates()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }

        private async Task<IEnumerable<ReleaseNote>> GetReleaseNotesAsync(GitHubClient github, string project) =>
            (await github.Repository.Release.GetAll("statiqdev", project)).Where(x => x.PublishedAt.HasValue).Select(x => new ReleaseNote(project, x));

        private class ReleaseNote
        {
            private readonly Release _release;

            public ReleaseNote(string project, Release release)
            {
                Project = project;
                _release = release;
            }

            public string Project { get; }

            public string Name => _release.Name;

            public string Body => _release.Body;

            public DateTimeOffset PublishedAt => _release.PublishedAt.Value;
        }
    }
}
