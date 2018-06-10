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
