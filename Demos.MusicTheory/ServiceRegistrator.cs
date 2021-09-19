using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Demos.MusicTheory
{
    public static class ServiceRegistrator
    {
        public static void RegisterMusicTheoryServices(this IServiceCollection services)
        {
            services.AddScoped<ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan>();
        }
    }
}
