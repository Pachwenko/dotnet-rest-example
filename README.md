# Example ASP.NET Core JSON API

implements https://docs.microsoft.com/en-us/learn/modules/build-web-api-aspnet-core/?WT.mc_id=dotnet-35129-website

### Running locally

1. Install .NET 6.0
2. `dotnet run` in project folder
3. View the API at [https://localhost:7053](https://localhost:7053) or [http://localhost:5059](http://localhost:5059)

### Useful commands

- Install the http repl with `dotnet tool install -g Microsoft.dotnet-httprepl`
- Launch the httpl repl with `httprepl https://localhost:7053`
    - If you get an error message about not finding the OpenAPI specification then you need to do `dotnet dev-certs https --trust` [more info on dev ssl certs here](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos)
- This uses the openapi specification to do operations, such as `ls`, `get`, `post`, `put`, and `delete`
    - The library used for openapi stuff is [Swashbuckle](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio)