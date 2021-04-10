using Fluxor.DependencyInjection;
using Koryphaee.Extensions.Fluxor.Global;
using Microsoft.Extensions.DependencyInjection;

namespace Koryphaee.Extensions.Fluxor
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds services provided by Fluxor extensions.
		/// </summary>
		public static Options AddExtensions(this Options options)
		{
			options.Services
				.AddTransient<RegularActionDispatcher>()
				.AddSingleton<FluxorExceptionLogger>()
				.AddGlobal();
			return options;
		}

		private static IServiceCollection AddGlobal(this IServiceCollection services)
		{
			return services
				.AddSingleton<GlobalStore>()
				.AddSingleton<IGlobalDispatcher>(p => p.GetRequiredService<GlobalStore>())
				.AddSingleton(typeof(IGlobalState<>), typeof(GlobalState<>));
		}
	}
}
