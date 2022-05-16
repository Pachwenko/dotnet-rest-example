# Example ASP.NET Core JSON API

Expands on [this initial tutorial](https://docs.microsoft.com/en-us/learn/modules/build-web-api-aspnet-core/?WT.mc_id=dotnet-35129-website) by adding a Postgres database and Entity Framework (EF). Exposes a CRUD API for the Rental model.

### Running locally

1. Install .NET 6.0
2. Install postgres and run [the postgres setup file](./setup-postgres.psql) to configure the database and user.
    - If you are using Windows the command is `'C:\Program Files\PostgreSQL\14\bin\psql' -p 5433 -U root -f .\setup-postgres.psql`
	- On mac/linux the command is `sudo -u postgres psql -f setup-postgres.psql`
3. In the `src\SpookyRentals` folderL
    1. Install dotnet packages and local tools with `dotnet restore` and `dotnet tool restore`
    2. Migrate the database with `dotnet ef database update`
        - This will create the database and tables if they don't exist
    3. Run the `dotnet watch` command to hot reload whenever changes are made, else use `dotnet run`
    4. View the API documentation (if it didn't automatically open) at [https://localhost:7053/swagger](https://localhost:7053/swagger) or [http://localhost:5059/swagger](http://localhost:5059/swagger)

### Helpful Stuff

- Install the http repl with `dotnet tool install Microsoft.dotnet-httprepl`
- Launch the httpl repl with `dotnet tool run httprepl https://localhost:7053`
    - If you get an error message about not finding the OpenAPI specification then you need to do `dotnet dev-certs https --trust` [more info on dev ssl certs here](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos)
    - if the above didn't work you can just use http instead
- This uses the openapi specification to do operations, such as `ls`, `get`, `post`, `put`, and `delete`
    - The package used to auto-generate the openapi specification and swagger documentation is [Swashbuckle](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio)

- There are tests under the `Tests\SpookyRentals.Tests` folder using [Xunit](https://xunit.net/). There are many ways to test the application, I chose to use a real database. You can read more about different strategies [in the ef core testing docs](https://docs.microsoft.com/en-us/ef/core/testing/)
    - Execute the tests by going into the `Tests\SpookyRentals.Tests` folder and running `dotnet test`
    - To help debug tests, make sure to read [how to get console output](https://xunit.net/docs/capturing-output)

- Uses [Entity Framework](https://docs.microsoft.com/en-us/ef/core/) to handle database stuff including migrations
    - Add a migration with `dotnet ef migrations add "name"`
    - Run migrations with `dotnet ef database update`
