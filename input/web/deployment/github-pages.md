Statiq can be configured to deploy to [GitHub Pages](https://pages.github.com).

The following [settings](xref:web-settings) are used to configure deployment to GitHub Pages:

- `GitHubOwner`: The owner of the repository (organization or individual).
- `GitHubName`: The name of the repository.
- `GitHubUsername`: The username to use for deployment.
- `GitHubPassword`: The password to use for deployment.
- `GitHubToken`: The token to use for deployment (configure either this _or_ username and password).
- `GitHubBranch`: The branch to deploy to (defaults to `gh-pages` but you should change this to `master` for organization sites).

It's customary to set one or more of these settings as an environment variable in continuous
integration environments (particularly secrets like the password). In these scenarios you can either
set an environment variable with the name of the setting or set the setting to an alternate environment
variable (which itself will be added as a setting) using a
[computed value](xref:metadata-values#computed-values) or
[configuration delegate](xref:configuration-delegates).

Using a computed value in `appsettings.json`:

```json
{
    "GitHubOwner": "statiqdev",
    "GitHubName": "statiqdev.github.io",
    "GitHubToken": "=> GITHUB_TOKEN"
}
```

The [bootstrapper](xref:bootstrapper) also has an extension method for configuring deployment:

```csharp
await Bootstrapper
    .Factory
    .CreateWeb(args)
    .DeployToGitHubPages(
        "statiqdev",
        "statiqdev.github.io",
        Config.FromSetting<string>("GITHUB_TOKEN")
    )
    // ...
    .RunAsync();
```

There's also an overload to deploy to a specific branch other than `gh-pages`:

```csharp
await Bootstrapper
    .Factory
    .CreateWeb(args)
    .DeployToGitHubPagesBranch(
        "statiqdev",
        "statiqdev.github.io",
        Config.FromSetting<string>("GITHUB_TOKEN"),
        "master"
    )
    // ...
    .RunAsync();
```

Note that the environment variable `GITHUB_TOKEN` is set automatically
by [GitHub Actions](https://github.com/features/actions) but needs to be
passed to a script. The following is a simple GitHub Action definition
to deploy a Statiq site:

```yaml
name: Deploy Site
on: [push]

jobs:
  build:
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@master
    - uses: actions/setup-dotnet@v1
      with:
        dotnet-version: '3.1.100' # SDK Version to use.
    - run: dotnet run -- deploy
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}

```