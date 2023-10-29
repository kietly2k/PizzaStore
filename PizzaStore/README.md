## Introdution

- How you build the API might differ vastly between tech stacks. As part of building an API, you know there are parts like data storage, security, versioning, and documentation
- [What is minimal API](https://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-api/2-what-is-minimal-api)
- [Choose between controller-based APIs and minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/apis?view=aspnetcore-7.0)

## Understand the code in Program.cs

- In the first two lines of code, you create a builder. From the `builder`, you construct an application instance `app`:

```
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
```

The builder has a `Services` property. By using the `Services` property, you can add features like CORS, Entity Framework, or Swagger. Here's an example:

```
builder.Services.AddCors(options => {});
```

- You can also use `app` instance to add middleware. Here's an example of how you would use a capability like CORS:

```
app.UseCors("some unique string");
```

> Note: Middleware is usually code that intercepts the request and carries out checks like checking for authentication or ensuring the client is allowed to perform this operation according to CORS. After the middleware is done executing, the actual request is carried out. Data is either read or written to the store and a response is sent back to the calling client.

Finally, `app.Run()` starts your API and makes it listen for requests from the client.

To run your code, you start your project, like any .NET project, with `dotnet run`. By default, that means you have a project running on http://localhost:{PORT}, where `PORT` is a value between 5000 and 5300.

## Design API
 - [Design API](ttps://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-spa/4-design-api)

## Minimal API Summary
There are many benefits to this approach:
- **Easier to get started**: With four lines of code, you can have an API up and running quickly.
- **Progressive enhancement**: Add features when you need them. Until then, your program code stays small.
- **.NET 6 latest features**: Use all the latest features from .NET 6 like top-level statements and records.

## Steps to add a new database provider in EF
- In general, you'll use the following steps to implement a new database provider:
    - Add one or more NuGet packages to your project to include the database provider.
    - Configure the database connection.
    - Configure the database provider in the ASP.NET Core services.
Perform database migrations.

## CORS
CORS is a protocol that allows a back-end API to accept requests from domains (and ports) other than the one it's currently running on. This is a security feature.

## Configure CORS on the server
```c#
// 1) define a unique string
readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// 2) define allowed domains, in this case "http://example.com" and "*" = all
//    domains, for testing purposes only.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      builder =>
      {
          builder.WithOrigins(
            "http://example.com", "*");
      });
});
// 3) use the capability
app.UseCors(MyAllowSpecificOrigins);
```
The code snippet shows how to add a policy to an API that includes an allowlist of domains that are allowed to communicate with the API. In this example, the domain http://example.com is added to the allowlist. If you want to allow all domains, you can use * as the allowlist, which means that all possible domains are allowed.

The UseCors() method can be used to offer more granular control over which HTTP verbs are allowed for specific routes.