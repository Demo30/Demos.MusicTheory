using System;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Demos.MusicTheory;

public static class MusicTheoryServices
{
    internal static IServiceProvider ServiceProvider;
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ChromaticNoteFullyQualifiedProviderFromNoteBySpan>();
        services.AddScoped<ChromaticNoteFullyQualifiedProviderFromNoteByInterval>();
    }

    public static void SetServiceProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}