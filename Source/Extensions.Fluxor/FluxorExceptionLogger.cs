using Fluxor.Exceptions;
using Microsoft.Extensions.Logging;

namespace Koryphaee.Extensions.Fluxor
{
	/// <summary>
	/// Logs exceptions that happen in Fluxor.
	/// </summary>
	public class FluxorExceptionLogger
	{
		private readonly ILogger<FluxorExceptionLogger> logger;

		public FluxorExceptionLogger(ILogger<FluxorExceptionLogger> logger)
		{
			this.logger = logger;
		}

		public void Log(UnhandledExceptionEventArgs e)
		{
			string state = e.WasHandled ? "handled" : "unhandled";
			logger.LogError(e.Exception, "Caught {state} exception in Fluxor", state);
			e.Handled();
		}
	}
}
