using System;
using Koryphaee.Extensions.Fluxor.Global;
using Microsoft.AspNetCore.Components;

namespace Koryphaee.Extensions.Fluxor.Components
{
	/// <summary>
	/// Base class for components that use a single global state.
	/// Offers convenience methods for easy dispatching.
	/// </summary>
	public abstract class GlobalStateComponent<TState> : ComponentBase, IDisposable
		where TState : IStateMarker
	{
		[Inject]
		private IGlobalState<TState> Container { get; set; } = default!;

		[Inject]
		private IGlobalDispatcher Dispatcher { get; set; } = default!;

		protected TState State => Container.Value;

		protected void Dispatch(IActionMarker action)
		{
			Dispatcher.Dispatch(action);
		}

		protected void Dispatch<TAction>()
			where TAction : IActionMarker, new()
		{
			TAction action = new();
			Dispatcher.Dispatch(action);
		}

		protected override void OnInitialized()
		{
			Container.StateChanged += OnStateChanged;
		}

		public virtual void Dispose()
		{
			Container.StateChanged -= OnStateChanged;
			GC.SuppressFinalize(this);
		}

		private void OnStateChanged(object? sender, TState e)
		{
			InvokeAsync(StateHasChanged);
		}
	}
}
