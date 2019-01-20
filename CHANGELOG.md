# Changelog

All notable changes to **Dangl.Data.Shared** are documented here.

## v1.1.0:
- The `Dangl.Data.Shared.AspNetCore` package was updated to ASP.NET Core 2.2

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
