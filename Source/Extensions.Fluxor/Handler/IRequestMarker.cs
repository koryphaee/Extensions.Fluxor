namespace Koryphaee.Extensions.Fluxor.Handler
{
	/// <summary>
	/// Marker interface for request actions.
	/// </summary>
	public interface IRequestMarker<TResponse> : IActionMarker
		where TResponse : IResponseMarker
	{
	}
}
