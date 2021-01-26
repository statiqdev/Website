Order: 5
---
Statiq has built-in support for deployment to a variety of services.

Statiq can deploy to the following services by default:
- [Azure App Service](xref:web-azure-app-service)
- [GitHub Pages](xref:web-github-pages)
- [Netlify](xref:web-netlify)

Once one or more services has been configured you can trigger deployment via the
[command-line interface](xref:web-command-line-interface):

```txt
dotnet run -- deploy
```