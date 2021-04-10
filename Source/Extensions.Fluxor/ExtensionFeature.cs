using Fluxor;
using Koryphaee.Extensions.Fluxor.Reducible;

namespace Koryphaee.Extensions.Fluxor
{
	/// <summary>
	/// Base class for features that use Fluxor extensions.
	/// </summary>
	public abstract class ExtensionFeature<TState> : Feature<TState>
		where TState : IStateMarker
	{
		protected ExtensionFeature()
		{
			// no reason to not include this for all features
			Reducers.Add(new ReducibleActionReducer<TState>());
		}

		public override string GetName() => GetType().Name;
	}
}
