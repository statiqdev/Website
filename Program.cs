﻿using System;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using Statiq.Docs;

namespace Statiqdev
{
    public static class Program
    {
        public static async Task<int> Main(string[] args) =>
            await Bootstrapper.Factory
                .CreateDocs(args)
                .AddMappedInputPath("external/Statiq.Web/examples/Statiq.Web.Examples/input", "web/examples")
                .AddMappedInputPath("external/Statiq.Web/examples/Statiq.Web.Examples/input/Shared", "Shared") // Promote the partial views to the root Shared folder
                .AddShortcode(
                    "WebBadge",
                    new ShortcodeResult(@"<span
                        class=""badge badge-blue text-white""
                        data-toggle=""tooltip""
                        data-html=""true""
                        title=""This section describes functionality available in <em>Statiq Web</em> and <em>Statiq Docs</em>, which require a license for commercial use."">
                            Web
                        </span>"))
                .AddShortcode(
                    "DocsBadge",
                    new ShortcodeResult(@"<span
                        class=""badge badge-orange text-white""
                        data-toggle=""tooltip""
                        data-html=""true""
                        title=""This section describes functionality available in <em>Statiq Docs</em>, which requires a license for commercial use."">
                            Docs
                        </span>"))
                .RunAsync();
    }
}