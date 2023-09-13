# Changelog

All notable changes to **Dangl.Data.Shared** are documented here.

## v2.2.0:
- Added a new target for .NET 6, dropped support for .NET Core 3.1
- Removed `netstandard2.0` and `net5.0` targets for **Dangl.Data.Shared.AspNetCore**, and added `net7.0`
- The `UseLocalizedSpaStaticFiles` will now also try to resolve relative paths for requests, meaning e.g. assets placed relative to an SPA subfolder will also be correctly removed now, transparent to the client requesting the file. For example, a client that requests `/assets/picture.jpg` might be served the file internally from `/dist/en/assets/picture.jpg`

## v2.1.1
- Added `CdnNoCacheAttribute : ActionFilterAttribute`, an attribute which sets the `Cache-Control` header to `no-store, no-cache, no-transform`

## v2.1.0:
- The `LocalizedSpaStaticFileExtensions` now make sure that the default entry file for the SPA is never cached and returns `Cache-Control: no-store` when delivered

## v2.0.0:
- Dropped support for `netstandard1.3`. The lowest supported framework is now `netstandard2.0`

## v1.9.0:
- Added a new generic overload `ApiError<TError>`

## v1.8.0:
- Added a new generic overload `RepositoryResult<TResult, TError>`

## v1.7.0:
- The `CamelCaseDefaultValuesContractResolver` was changed to now preserve the casing for keys in dictionaries
- Drop tests for `netcoreapp2.2` and add tests for `netcoreapp2.1`

## v1.6.0:
- Added the `EmptyEnumDeserializer` and the `GuidStringDeserializer`. Both these classes are used to deserialize either Guids or Enums from null values or empty strings and will return the default values for them, e.g. `Guid.Empty`. They are automatically activated when using `ConfigureDefaultJsonSerializerSettings`

## v1.5.0:
- Drop `netcoreapp3.0` target and add `netcoreapp3.1` target

## v1.4.0:
- Add `netstandard2.1` and `netcoreapp3.0` targets

## v1.3.1:
- Add `CamelCaseDefaultValuesContractResolver`

## v1.3.0:
- The generated assemblies now have a strong name. This is a breaking change of the binary API and will require recompilation on all systems that consume this package. The strong name of the generated assembly allows compatibility with other, signed tools. Please note that this does not increase security or provide tamper-proof binaries, as the key is available in the source code per [Microsoft guidelines](https://msdn.microsoft.com/en-us/library/wd40t7ad(v=vs.110).aspx)

## v1.2.0:
- Move JsonExtensions from Dangl.Data.Shared.AspNetCore to Dangl.Data.Shared

## v1.1.1:
- Add `EmptyFormFileValidator` with extensions to support the generation of an invalid `ModelState` if a parameter of type `IFormFile` (or derived) is passed with a `Length` of zero bytes

## v1.1.0:
- The `Dangl.Data.Shared.AspNetCore` package was updated to ASP.NET Core 2.2
- Add `LocalizedSpaStaticFileExtensions` to help serving localized Single Page Applications (SPAs)
- Add `IUserLanguageService` with default implementation

## v1.0.10:
- Add `CompressedRequestMiddleware` to support clients sending compressed request bodies

## v1.0.9:
- Add `HttpHeadRequestMiddleware` to support Http `HEAD` requests to all actions supporting originally only Http `GET`

## v1.0.8:
- Add `IClaimBasedAuthorizationRequirement` and supporting functionality

## v1.0.7:
- Add `JsonOptionsExtensions`

## v1.0.6:
- Rename `FileResult` to `FileResultContainer`

## v1.0.5:
- Add `FileResult`

## v1.0.3:
- Add logging to `ModelStateValidationFilter` if invalid requests get rejected

## v1.0.2:
- Add `RequiredFormFileValidationFilter` to support checking for `[Required]` parameters of type `IFormFile` on controller actions
