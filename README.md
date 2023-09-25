# Dangl.Data.Shared
[![Build Status](https://jenkins.dangl.me/buildStatus/icon?job=GeorgDangl%2FDangl.Data.Shared%2Fdevelop)](https://jenkins.dangl.me/job/GeorgDangl/job/Dangl.Data.Shared/job/develop/)  
![NuGet](https://img.shields.io/nuget/v/Dangl.Data.Shared.svg)  

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
        });

## RequiredFormFileValidationFilter
The `RequiredFormFileValidationFilter` is a simple wrapper that returns a `BadRequestObjectResult` with an `ApiError` body when the invoked
Controller action has parameters of type `IFormFile` that are annotated with `[Required]` but have no value bound.

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
        });

## BiggerThanZeroAttribute

The `BiggerThanZeroAttribute` is a `ValidationAttribute` that can be applied to `int` or `long` properties to ensure their values are greater than zero.

## JsonOptionsExtensions

The `JsonOptionsExtensions` class configures default Json options for the Newtonsoft Json serializer.
It ignores null values, uses the `EmptyEnumDeserializer` (derived from `StringEnumConverter` and maps empty or null enum values to their default value), the `GuidStringDeserializer` to deserialize empty or null Guid values to an empty guid and ignores default values for `Guid`, `DateTime`
and `DateTimeOffset`.

## StringFilterQueryExtensions

The `StringFilterQueryExtensions` class has a `.Filter()` extension method with the following signature:

```csharp
public static IQueryable<T> Filter<T>(this IQueryable<T> queryable,
    string filter,
    Func<string, Expression<Func<T, bool>>> filterExpression,
    bool transformFilterToLowercase)
```

It can be used to apply a string filter. Meaning, you supply a string `filter` parameter and a `filterExpression`, and the `filter` is split into word segments and each applied to the `filterExpression`.

### Example

Let's you you want to implement a dynamic filter, e.g. if the user enters two words, you want to check if both of the words are contained in the value.  
So, the user input maybe looks like this: `hello world`, and your filter looks like `x => y => y.Contains(x)`. Instead of you having to manually split the string, you can now do something like this:
```csharp
queryable.Filter("hello world", x => y => y.Contains(x), transformFilterToLowercase: false);
```

This would then get translated to something like this:
```csharp
// The splitted query is "hello" and "world"
queryable.Where(q => q.Contains("hello") && q.Contains("world"));
```

You can also optionally tell it to transform all single segements of your filter to lowercase.

In **EntityFramework**, you could use something like this to build a dynamic LIKE query:

```csharp
var filter = "search query";
var filteredQuery = _context.Projects
    .Filter(filter, text => p =>
        EF.Functions.Like(p.Name, $"%{text}%")
        || EF.Functions.Like(p.ContactName, $"%{text}%")
        || EF.Functions.Like(p.Description, $"%{text}%"),
        transformFilterToLowercase: true);
```

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

If you're using **ASP.NET Core 3.0** or later, you should use the middleware before the call to the routing middleware:

```csharp
app.UseHttpHeadToGetTransform();
app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});
```

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

Additionally, it also tries to resolve relative paths for requests. For example, if the client requests `/assets/picture.jpg`, the extensions will try to serve the file from `/dist/en/assets/picture.jpg` if the user language is `en`. This makes it possible to serve assets that are placed relative to the SPA root folder, without requiring the client to know about the relative path or any other configuration on the SPA side.

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

## CdnNoCacheAttribute

The `CdnNoCacheAttribute` is a class that can be applied to ASP.NET Core controller actions to append the following headers:

    Cache-Control: no-store, no-cache, no-transform

This attribute should be used for API responses on Cloudflare CDN. It sets the 'Cache-Control' header to 'no-store, no-cache, no-transform'. Cloudflare will not automatically compress responses for unknown content types, and will also not automatically pass through compression. For example, returning mime type 'application/octet-stream' without an appropriate 'no-cache, no-transform' entry in 'Cache-Control' will make Cloudflare always return the response uncompressed, even if the original server did compress it.

Example:

```csharp
[HttpGet("NoCacheNoTransform")]
[CdnNoCache]
public IActionResult NoCacheNoTransform()
{
    return Ok(new
    {
        Value = "Some Data"
    });
}
```

## Assembly Strong Naming & Usage in Signed Applications

This module produces strong named assemblies when compiled. When consumers of this package require strongly named assemblies, for example when they
themselves are signed, the outputs should work as-is.
The key file to create the strong name is adjacent to the `csproj` file in the root of the source project. Please note that this does not increase
security or provide tamper-proof binaries, as the key is available in the source code per 
[Microsoft guidelines](https://msdn.microsoft.com/en-us/library/wd40t7ad(v=vs.110).aspx)
