# Example ASP.NET Core JSON API

Expands on [this initial tutorial](https://docs.microsoft.com/en-us/learn/modules/build-web-api-aspnet-core/?WT.mc_id=dotnet-35129-website) by adding a Postgres database connection with Entity Framework (EF).

### Running locally

1. Install .NET 6.0
2. Install postgres and run [the postgres setup file](./setup-postgres.psql) to configure a database with a user.
3. Migrate the database with `dotnet ef database update`
4. `dotnet run` in project folder
5. View the API documentation at [https://localhost:7053/swagger](https://localhost:7053/swagger) or [http://localhost:5059/swagger](http://localhost:5059/swagger)

Note that I tried to get docker to work but was having issues, mainly with the ssl certificates but http also didn't work...

### Useful commands

- Install the http repl with `dotnet tool install Microsoft.dotnet-httprepl`
- Launch the httpl repl with `dotnet tool run httprepl https://localhost:7053`
    - If you get an error message about not finding the OpenAPI specification then you need to do `dotnet dev-certs https --trust` [more info on dev ssl certs here](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos)
    - if the above didn't work you can just use http instead
- This uses the openapi specification to do operations, such as `ls`, `get`, `post`, `put`, and `delete`
    - The library used for openapi stuff is [Swashbuckle](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio)
    - example post:
        -

- Uses [Entity Framework](https://docs.microsoft.com/en-us/ef/core/) to handle database stuff including migrations
    - Add a migration with `dotnet ef migrations add "name"`
    - Run migrations with `dotnet ef database update`
    - Had issues getting `dotnet ef` tool to work... it should be installed locally as global installs can't be trusted imo