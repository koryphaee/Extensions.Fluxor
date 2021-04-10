# Extensions.Fluxor

Extensions.Fluxor extends the functionality of the [Fluxor](https://github.com/mrpmorris/Fluxor) library.

## Motivation

Extensions.Fluxor contains multiple new features that I missed when working with Fluxor.
Adding them to Fluxor directly is not really an option because of multiple reasons:

* they depend on each other
* they would introduce breaking changes
* they undermine/replace concepts from Fluxor

Because of that I opted for writing an external library.

## Installation

Extensions.Fluxor is split into two NuGet packages:

* [Koryphaee.Extensions.Fluxor](https://www.nuget.org/packages/Koryphaee.Extensions.Fluxor)
* [Koryphaee.Extensions.Fluxor.Components](https://www.nuget.org/packages/Koryphaee.Extensions.Fluxor.Components)

## Usage

To begin using Fluxor.Extensions simply install the NuGet package and register the services like so:

```c#
public void SetupFluxor(IServiceCollection services)
{
	services.AddFluxor(o => o.AddExtensions());
}
```

This will add all services from Fluxor.Extensions to the service collection.

## Features

See the [documentation](./Docs/README.md)
