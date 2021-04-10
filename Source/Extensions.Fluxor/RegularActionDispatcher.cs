using System;
using System.Timers;
using Fluxor;
using Microsoft.Extensions.Logging;

namespace Koryphaee.Extensions.Fluxor
{
	/// <summary>
	/// Dispatches an action at regular intervals.
	/// </summary>
	public class RegularActionDispatcher : IDisposable
	{
		private readonly ILogger<RegularActionDispatcher> logger;
		private readonly IDispatcher dispatcher;

		private readonly Timer timer;

		private Type actionType = null!;
		private Func<IActionMarker> actionFactory = null!;

		public RegularActionDispatcher(
			ILogger<RegularActionDispatcher> logger,
			IDispatcher dispatcher)
		{
			this.logger = logger;
			this.dispatcher = dispatcher;

			timer = new Timer();
		}

		public void Start<TAction>(int interval)
			where TAction : IActionMarker, new()
		{
			actionFactory = () => new TAction();
			StartTimer(interval, typeof(TAction));
		}

		public void Start<TAction>(int interval, Func<TAction> factory)
			where TAction : IActionMarker
		{
			actionFactory = () => factory();
			StartTimer(interval, typeof(TAction));
		}

		private void StartTimer(int interval, Type type)
		{
			timer.AutoReset = true;
			timer.Interval = interval;
			timer.Elapsed += TimerOnElapsed;
			timer.Start();

			actionType = type;
			logger.LogDebug("Started regular action dispatcher for action {type}", actionType.Name);
		}

		private void TimerOnElapsed(object sender, ElapsedEventArgs e)
		{
			IActionMarker action = actionFactory();
			dispatcher.Dispatch(action);
			logger.LogDebug("Dispatched regular action {type}", actionType.Name);
		}

		public void Dispose()
		{
			timer.Stop();
			timer.Dispose();

			logger.LogDebug("Stopped regular action dispatcher for action {type}", actionType.Name);

			GC.SuppressFinalize(this);
		}
	}
}
