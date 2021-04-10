using System.Threading.Tasks;
using Fluxor;

namespace Koryphaee.Extensions.Fluxor.Handler
{
	/// <summary>
	/// Base class for effects that have a request and response.
	/// </summary>
	public abstract class RequestResponseEffect<TRequest, TResponse> : Effect<TRequest>
		where TRequest : IRequestMarker<TResponse>
		where TResponse : IResponseMarker
	{
		public sealed override async Task HandleAsync(TRequest request, IDispatcher dispatcher)
		{
			TResponse response = await Handle(request);
			dispatcher.Dispatch(response);
		}

		public abstract Task<TResponse> Handle(TRequest request);
	}
}
