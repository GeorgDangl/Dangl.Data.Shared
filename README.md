# Dangl.Data.Shared
[![Build Status](https://jenkins.dangl.me/buildStatus/icon?job=Dangl.Data.Shared/develop)](https://jenkins.dangl.me/job/Dangl.Data.Shared/)  
![NuGet](https://img.shields.io/nuget/v/Dangl.Data.Shared.svg)  
[![Built with Nuke](http://nuke.build/rounded)](https://www.nuke.build)

This solution builds both **Dangl.Data.Shared** and **Dangl.Data.Shared.AspNetCore** packages.

The aim of this solution is to consolidate simple, reused code such as `ApiError` or `RepositoryResult<T>`.

Link to docs:
  * [Dangl.Data.Shared](https://docs.dangl-it.com/Projects/Dangl.Data.Shared)

[Changelog](./CHANGELOG.md)

## ModelStateValidationFilter
The `ModelStateValidationFilter` is a simple wrapper that returns a `BadRequestObjectResult` with an `ApiError` body when the passed `ModelState`
of an action is invalid. This allows to keep controlls free of basic model state validation logic.

To use the filter, it must be configured in the `AddMvc()` call in `ConfigureServices`:

    services.AddMvc(options =>
        {
            options.Filters.Add(typeof(ModelStateValidationFilter));
        })

## RequiredFormFileValidationFilter
The `RequiredFormFileValidationFilter` is a simple wrapper that returns a `BadRequestObjectResult` with an `ApiError` body when the invoked
Controller action as paramaters of type `IFormFile` that are annotated with `[Required]` but have no value bound.

For example, the following action makes use of the filter:

```csharp
[HttpPost("RequiredFormFile")]
public IActionResult RequiredFormFile([Required]IFormFile formFile)
{
    return Ok();
}
```

To use the filter, it must be configured in the `AddMvc()` call in `ConfigureServices`:

    services.AddMvc(options =>
        {
            options.Filters.Add(typeof(RequiredFormFileValidationFilter));
        })

## BiggerThanZeroAttribute

The `BiggerThanZeroAttribute` is a `ValidationAttribute` that can be applied to `int` properties to ensure their values are greater than zero.

## JsonOptionsExtensions

The `JsonOptionsExtensions` class configures default Json options for the Newtonsoft Json serializer.
It ignores null values, uses the `StringEnumConverter` and ignores default values for `Guid`, `DateTime`
and `DateTimeOffset`.

## IClaimBasedAuthorizationRequirement

The namespace `Dangl.Data.Shared.AspNetCore.Authorization` contains utilities that help in building authorization policies that
check for existing claims on authenticated users. By default, claim values that are either `true` or represent a valid-until time in
the future (like `2018-08-08T09:02:15.5732531Z`) are considered valid and will lead to the requirement handler succeeding.

To use it, there is an interface for the requirements:

```csharp
public interface IClaimBasedAuthorizationRequirement : IAuthorizationRequirement
{
    IReadOnlyList<string> ClaimNames { get; }
}
```

Now, a class can be defined that implements this interface and be added as requirement in a policy:

```csharp
o.AddPolicy(AvaCloudConstants.CONVERSION_POLICY_NAME, policy => policy
                        .AddRequirements(new ConversionRequirement(requiredUserClaim, requiredClientClaim)));
```

And the `IAuthorizationHandler` must be configured in the services:

```csharp
services.AddTransient<IAuthorizationHandler, ClaimBasedAuthorizationRequirementHandler<ConversionRequirement>>();
```

Finally, to use this policy in your controllers or actions, you need to add an `Authorize` attribute with the policy name:

```csharp
[Authorize(Policy = AvaCloudConstants.CONVERSION_POLICY_NAME)]
[Route("api/[Controller]")]
public class CategoriesController : Controller
```

## HttpHeadRequestMiddleware

This middleware transforms incoming Http `HEAD` requests internally to `GET` requests so that they can hit their intended target action.
The body will be set to `Stream.Null`, so that only the response headers are being sent back to the client.  
This should be called before the `AddMvc()` call, like this:

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        app.UseHttpHeadToGetTransform();
        app.UseMvc();
    }

## CompressedRequestMiddleware

This middleware supports clients that send their requests either `gzip` or `deflate` compressed. This can be used when endpoints
expect big upload sizes to save on transfer time.  
This should be called before other calls, like this:

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        app.UseClientCompressionSupport();
        app.UseMvc();
    }

## IUserLocaleService

This service has a single method: `string GetUserLocale();` and should return the locale for the current Http
request. The default implementation `UserLocaleService` will do the following:

1. It must be initiated with a list of available, valid languages and a name for a cookie under which users can store their preffered locale
2. For every request which has a locale set via the cookie, this locale is returned
3. If no cookie is present or the locale in the cookie is not available, the `Accept-Language` Http header is parsed
4. Otherwise, the first configured available locale is returned

The `UserLocaleService` is added automatically when you use the `LocalizedSpaStaticFileExtensions`.

## LocalizedSpaStaticFileExtensions

The `LocalizedSpaStaticFileExtensions` can be used to serve localized Single Page Applications (SPAs). For example,
a localized Angular application might generate these files in your `wwwroot` directory:

    /dist/en/index.html
    /dist/de/index.html

To be able to serve these index files depending on the request of the user, the extensions detect the user
language via the `IUserLocaleService` and serve the correct files.

### Example

First, configure the service:

    services.AddLocalizedSpaStaticFiles(languageCookieName: ".MyApp.Locale",
        availableLocales: new[] { "de", "en" },
        spaRootPath: "dist");

Then add this as the last action for the request pipeline:

    // This serves localized SPA files from disk,
    // e.g. from wwwroot/dist/en
    app.UseLocalizedSpaStaticFiles("index.html");

Please note: `IHttpContextAccessor` must be available via dependency injection

## EmptyFormFileValidator

The `EmptyFormFileValidator` class is used to generate an invalid `ModelState` in an ASP.NET Core Request pipeline if a parameter is of type `IFormFile` (or derived) but has a `Length` of zero bytes.

### Example

Simply configure it in your MVC setup:

```csharp
using Dangl.Data.Shared.AspNetCore.Validation;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public class Startup
    {
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(mvcSetup =>
            {
                mvcSetup.AddEmptyFormFileValidator();
            });
        }
    }
}
```

## Assembly Strong Naming & Usage in Signed Applications

This module produces strong named assemblies when compiled. When consumers of this package require strongly named assemblies, for example when they
themselves are signed, the outputs should work as-is.
The key file to create the strong name is adjacent to the `csproj` file in the root of the source project. Please note that this does not increase
security or provide tamper-proof binaries, as the key is available in the source code per 
[Microsoft guidelines](https://msdn.microsoft.com/en-us/library/wd40t7ad(v=vs.110).aspx)
