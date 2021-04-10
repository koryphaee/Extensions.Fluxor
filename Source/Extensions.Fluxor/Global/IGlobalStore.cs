using Fluxor;

namespace Koryphaee.Extensions.Fluxor.Global
{
	/// <summary>
	/// Store that is shared between all scopes.
	/// </summary>
	public interface IGlobalStore : IGlobalDispatcher
	{
		/// <summary>
		/// Retrieves a specific feature by state type.
		/// </summary>
		public IFeature<TState> GetFeature<TState>()
			where TState : IStateMarker;
	}
}
