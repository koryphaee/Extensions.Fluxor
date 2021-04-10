namespace Koryphaee.Extensions.Fluxor.Global
{
	/// <summary>
	/// Dispatcher for the <see cref="IGlobalStore"/>
	/// </summary>
	public interface IGlobalDispatcher
	{
		/// <summary>
		/// Dispatches an action to the global store.
		/// </summary>
		void Dispatch(IActionMarker action);
	}
}
