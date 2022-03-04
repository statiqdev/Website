Order: 10
Badge: Web
---
[Statiq Web](xref:web) has built-in support for deployment to a variety of services.

Statiq Web can deploy to the following services by default:
- [Azure App Service](xref:azure-app-service)
- [GitHub Pages](xref:github-pages)
- [Netlify](xref:netlify)

Once one or more services has been configured you can trigger deployment via the
[command-line interface](xref:command-line-interface):

```txt
dotnet run -- deploy
```