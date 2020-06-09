# Changelog

All notable changes to **Dangl.Data.Shared** are documented here.

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
