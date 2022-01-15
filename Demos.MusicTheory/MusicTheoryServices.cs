using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Demos.MusicTheory;

public static class MusicTheoryServices
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ChromaticNoteFullyQualifiedProviderFromNoteBySpan>();
        services.AddScoped<ChromaticNoteFullyQualifiedProviderFromNoteByInterval>();
    }
}