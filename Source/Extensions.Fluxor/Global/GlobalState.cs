using System;
using Fluxor;

namespace Koryphaee.Extensions.Fluxor.Global
{
	public class GlobalState<TState> : IGlobalState<TState>
		where TState : IStateMarker
	{
		private readonly GlobalStore globalStore;
		private IFeature<TState>? feature;

		public GlobalState(GlobalStore globalStore)
		{
			this.globalStore = globalStore;
		}

		private IFeature<TState> Feature => feature ??= globalStore.GetFeature<TState>();

		public TState Value => Feature.State;

		public event EventHandler<TState> StateChanged
		{
			add => Feature.StateChanged += value;
			remove => Feature.StateChanged -= value;
		}

		event EventHandler IState.StateChanged
		{
			add => (Feature as IFeature).StateChanged += value;
			remove => (Feature as IFeature).StateChanged -= value;
		}
	}
}
