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

## Single Page Application Utilities

The `Dangl.Data.Shared.AspNetCore.SpaUtilities` namespace contains utilities to work with Single Page Applications (SPAs).

### Non-Localized SPAs

The `UseSpaStaticFile(this IApplicationBuilder app, string pathToIndex)` extensiion can be used to support regular Single Page Applications that have a single entry point.
It works by redirecting all requests that are not matched by anything else to the entry point on the SPA. The `pathToIndex` should be absolute from the
`wwwroot` folder, e.g. `/dist/index.html`.  
**It must be called as the last action in your app pipeline.**
