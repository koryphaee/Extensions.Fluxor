using Fluxor;

namespace Koryphaee.Extensions.Fluxor.Global
{
	/// <summary>
	/// State that is shared between all scopes.
	/// </summary>
	public interface IGlobalState<TState> : IState<TState>
		where TState : IStateMarker
	{
	}
}
