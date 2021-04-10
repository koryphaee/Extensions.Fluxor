namespace Koryphaee.Extensions.Fluxor.Reducible
{
	/// <summary>
	/// Action that is reducible for a specific state.
	/// Since reducers and actions usually have a 1:1 relationship this allows to have both in the same file.
	/// </summary>
	public interface IReducibleAction<TState> : IActionMarker
		where TState : IStateMarker
	{
		TState Reduce(TState state);
	}
}
