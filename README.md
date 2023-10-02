# ASPNET-Core
1. Check the dotnet information: dotnet --info
2. Show dotnet help menu: dotnet -h
3. Show the list of template in dotnet: dotnet new list
4. Create the solution file: dotnet new sln
5. Create a dotnet API with the name API: dotnet new webapi -n API
6. Add one or more projects to a solution file: dotnet sln add API/
7. List the project in a solution: dotnet sln list
8. Run the project: In terminal, goto API folder -> Run: dotnet run
9. Configure the dotnet run execution: Goto Properties folder -> launchSettings.json -> profiles -> http:
    -> applicationUrl: change to "http://localhost:5000; https://localhost:5001"
    -> launchBrouser: Should turn to false
    -> launchUrl: can run http://localhost:5202/swagger/index.html like Postman
10. If you cannot run https, use the command: dotnet dev-certs https --trust
11. There are 2 file: appsettings.json and appsettings.Development.json to configure the application at runtime
    - In appsettings.Development.json: change Microsoft.AspNetCore to "Information" to get more information int he terminal about what's happening with our application as it is running
12. In API.csproj:
    - TargetFramework: the .Net project
    - Nullable: Responsible for string behavior, we can disable of enable it.
    - ImplicitUsings: Remove some of the boilerplate inside our code. Should be enable.
13. Entity Framework:
    - What is it?
        + An Object Relational Mapper (ORM)
        + Translate our code into SQL commands that update our tables in the database
        + DbContext is a class acting as a bridge between our domain or entity classes and the database
    - Features:
        + Querying
        + Change Tracking
        + Saving
        + Concurrency
        + Transactions
        + Caching
        + Built-in conventions
        + Configurations
        + Migrations
14. Add Entity Framework:
    - In VS Code, add extension NuGet Gallery:
        + Press Ctrl + P, type >NuGet: Open NuGet Gallery
        + Install Microsoft.EntityFrameworkCore.Sqlite with the version fix the .Net version and
        + Install Microsoft.EntityFrameworkCore.Design to use the code first approach to creating our database
    - Recheck in API.csproj, there is a new ItemGroup displaying EntityFrameworkCore.Design and Sqlite
15. Next step is creating DbContext.
16. Primary key of an entity
    - If we have a property name Id inside an entity classes, then entity framework will automatically use that as the primary key of our database
    - If we don't, then we have to add [Key] before the property
17. Creating the connection string
    - Appsetting.json is always read
    - Appsettings.Development.json is read in development environment -> We should change this file
    - Add those line below with datingapp.db is the name of the database file:
        {
            "Logging": {
                ...
                }
            },
            "ConnectionStrings": {
                "DefaultConnection": "Data source=datingapp.db"
            }
        }
    - Search for dotnet-ef in the browser or go to the link: https://www.nuget.org/packages/dotnet-ef/#readme-body-tab and run the command line code to install the tool.
    - Run the command line: dotnet ef migrations add InitialCreate -o Data/Migrations
        + This will add a new migrations name InitialCreate and output/stored in Data/Migrations.
        + Run: dotnet build
            $ Check if there is any error.
18. Create the database using Entity Framework Code first migrations.
    - To create the database, run the command: dotnet ef database update.
    - Open a SQLite database:
        + Install extension to read SQLite database.
        + Use Shift+Ctrl+P -> choose Open SQLite database -> Choose the database file in the project.
        + Under SQLite Explorer, we can examine the database.
    - Insert some data to the table:
        + Right click on the table, choose New Query [Insert].
19. Add a new API Controller:
    - When adding [Route("api/[controller]")], if the name of Controller is User Controller, the path should be api/user (omit the word "controller").
    - In Postman, turn of the SSL certificate in Settings if it does not allow you to use https.
20. Make our code Asynchronous
    - Add "async" word to the method
    - Add Task<...> to the return type of method
    - Add "await" to the inside method
    - Change the inside method asSync
21. Source control
    - Create a gitignore file: dotnet new gitignore, add appsettings.json to the file
    - Create globaljson file: dotnet new globaljson
        + This file tells us which version of the SDK we're going to use for this particular project