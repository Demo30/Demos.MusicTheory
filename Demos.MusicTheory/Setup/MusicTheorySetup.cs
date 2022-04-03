using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.Helpers;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.Setup;

public static class MusicTheorySetup
{
    public static void Setup()
    {
        if (ServicesManager.ServicesProvider.Services.Count > 0)
        {
            return;
        }

        var provider = ServicesManager.ServicesProvider;
        
        PrecomputeCaches();
        RegisterServices(provider);
    }

    private static void PrecomputeCaches()
    {
        BaseChromaticIndexMapper.InitializeMapper();
    }

    private static void RegisterServices(ServicesProvider provider)
    {
        provider.Services.Add(typeof(ChromaticNoteFullyQualifiedProviderFromNoteBySpan), () => new ChromaticNoteFullyQualifiedProviderFromNoteBySpan());
        provider.Services.Add(typeof(ChromaticNoteFullyQualifiedProviderFromNoteByInterval), () => new ChromaticNoteFullyQualifiedProviderFromNoteByInterval());
        provider.Services.Add(typeof(ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan), () => new ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan());
    }
}