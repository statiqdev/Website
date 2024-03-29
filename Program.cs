﻿await Bootstrapper.Factory
    .CreateDocs(args)
    .AddMappedInputPath("external/Statiq.Web/examples/Statiq.Web.Examples/input", "guide/examples")
    .AddMappedInputPath("external/Statiq.Web/examples/Statiq.Web.Examples/input/Shared", "Shared") // Promote the partial views to the root Shared folder
    .AddShortcode(
        "WebBadge",
        new ShortcodeResult(@"<small class=""float-right ml-1"">
            <span
                class=""badge badge-pill badge-faded""
                data-toggle=""tooltip""
                data-html=""true""
                title=""This section describes functionality available in <em>Statiq Web</em> and <em>Statiq Docs</em>, which require a license for commercial use."">
                <span class=""small font-sans-serif text-blue"">W</span>
                <span class=""small font-sans-serif text-orange"">D</span>
            </span>
        </small>"))
    .AddShortcode(
        "DocsBadge",
        new ShortcodeResult(@"<small class=""float-right ml-1"">
            <span
                class=""badge badge-pill badge-faded""
                data-toggle=""tooltip""
                data-html=""true""
                title=""This section describes functionality available in <em>Statiq Docs</em>, which requires a license for commercial use."">
                <span class=""small font-sans-serif text-orange"">D</span>
            </span>
        </small>"))
    .RunAsync();