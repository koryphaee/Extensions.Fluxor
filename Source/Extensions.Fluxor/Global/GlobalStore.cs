using System;
using System.Linq;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;

namespace Koryphaee.Extensions.Fluxor.Global
{
	public class GlobalStore : IGlobalStore, IDisposable
	{
		private readonly IServiceProvider globalProvider;
		private IServiceScope? scope;
		private IStore? store;

		public GlobalStore(IServiceProvider globalProvider)
		{
			this.globalProvider = globalProvider;
		}

		private IStore Store => store ??= Init();

		private IStore Init()
		{
			scope = globalProvider.CreateScope();
			store = scope.ServiceProvider.GetRequiredService<IStore>();
			store.InitializeAsync().GetAwaiter().GetResult();
			return store;
		}

		public IFeature<TState> GetFeature<TState>()
			where TState : IStateMarker
		{
			IFeature feature = Store.Features.Values.Single(f => f.GetStateType() == typeof(TState));
			return (IFeature<TState>) feature;
		}

		public void Dispatch(IActionMarker action)
		{
			Store.Dispatch(action);
		}

		public void Dispose()
		{
			scope?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
