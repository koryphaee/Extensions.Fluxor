using Fluxor;

namespace Koryphaee.Extensions.Fluxor.Reducible
{
	/// <summary>
	/// Reducer for <see cref="IReducibleAction{TState}"/>.
	/// </summary>
	/// <typeparam name="TState"></typeparam>
	public class ReducibleActionReducer<TState> : IReducer<TState>
		where TState : IStateMarker
	{
		public TState Reduce(TState state, object action)
		{
			var reducible = (IReducibleAction<TState>) action;
			return reducible.Reduce(state);
		}

		public bool ShouldReduceStateForAction(object action)
		{
			return action is IReducibleAction<TState>;
		}
	}
}
