# Features

## Type safety

One thing that bothers me about Fluxor is the lack of type safety: an action is just an `object`.
This can lead to bugs when you accidentally pass a random object instead of the correct action to the dispatcher.
The same goes for states, when using it for some generic magic.

For this reason Fluxor.Extensions provides the following marker interfaces:

* `IActionMarker` for actions
* `IStateMarker` for states

Some other feature also provide more specific markers for their functionality.
These markers are consistently used in generic constraints which should make it almost impossible to accidentally use the wrong type somewhere.

## Reducible actions

In Fluxor you have actions and reducers and most of the time they have a 1:1 relationship.
While actions are state-agnostic by themself, a reducer effectively couples an action to a specific state.
Only the reducer knows how it should use the properties of the action to alter the state.
Flxuor.Extensions allows you to express this tight coupling by using `IReducibleAction<TState>`.

It looks like this:
```c#
public interface IReducibleAction<TState> : IActionMarker
	where TState : IStateMarker
{
	TState Reduce(TState state);
}
```

To automatically reduce such an action for a state you have to add the `ReducibleActionReducer<TState>` to your feature.
This happens automatically when the feature inherts from `ExtensionFeature<TState>`.

An example action could look like this:
```c#
public record SetNameAction(string Name) : IReducibleAction<CustomerState>
{
	public CustomerState Reduce(CustomerState state) => state with
	{
		Name = Name
	};
}
```

## Global states

All services provided by Fluxor are registered with the scoped lifetime by default.
This makes sense as the default behavious, but sometimes you need something else.
There might be certain states you want to share across all scopes.

Fluxor.Extensions adds this funtionality using the following classes:

* `IGlobalStore`
* `IGlobalState<TState>`
* `IGlobalDispatcher`

They can be used the same way you would use their non-global equivalents.

Internally the `IGlobalStore` creates a `ServiceScope` and resolves the features from that.
This means it will automatically work with all your existing features, states etc.
It is also possible to use the same state in scoped and singleton mode simultaneously.

## Handlers

If you have worked with [MediatR](https://github.com/jbogard/MediatR) before you will be familiar with it's handler pattern.
The same logic is usable with Fluxor.Extensions.
There are two new marker interfaces: `IRequestMarker` for requests and `IResponseMarker` for responses.
The handler itself is implemented using a normal `IEffect` that contains a single method:

```c#
public Task<TResponse> Handle(TRequest request);
```

## Razor Components

Since Fluxor.Extensions is independent from ASP.NET Core there is a separate package for Blazor related funcionality.
It provides base classes for components that interact with Fluxor.Extensions.

### StateComponent

The `StateComponent<TState>` hides `IState<TState>` and `IDispatcher` from the child class.
It exposes the state value via an computed property that points to the state and dispatch functionality via multiple methods.

As opposed to `FluxorComponent` it doesn't use `IActionSubscriber`s and therefore no reflection.

Make sure to call `base.OnInitialized()` when you override `OnInitialized()` or the component won't work.

### GlobalStateComponent

The same thing as `StateComponent<TState>` but using the global store as explained above.
