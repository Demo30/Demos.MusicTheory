using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;
using Demos.MusicTheory.ChromaticContext.Helpers;
using Demos.MusicTheory.ChromaticContext.Providers;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.Setup;

public static class MusicTheorySetup
{
    internal static bool IsInitialized => ServicesManager.ServicesProvider.Services.Count > 0; 
    public static void Setup()
    {
        if (IsInitialized)
            throw new ServiceInitializationException();

        var provider = ServicesManager.ServicesProvider;

        PrecomputeCaches();
        RegisterServices(provider);
    }

    /// <summary>
    /// Anything that should be computed once on the startup
    /// </summary>
    private static void PrecomputeCaches()
    {
        BaseChromaticIndexMapper.InitializeMapper();
    }

    private static void RegisterServices(ServicesProvider provider)
    {
        provider.RegisterService(() => new NoteProviderFromNoteBySpan());
        provider.RegisterService(() => new NoteProviderFromNoteByInterval());
        provider.RegisterService(() => new NoteProviderFromIndex());
        provider.RegisterService(() => new IntervalProviderFromIndexSpan());
        provider.RegisterService(() => new ElementaryNoteFromDiatonicScaleKeySignatureProvider());
        provider.RegisterService(() => new DiatonicDegreeFromNoteRangeProvider());
        provider.RegisterService(() => new NoteProviderFromIndex());
        provider.RegisterService(() => new DiatonicScalesProviderFromNoteCluster());
        provider.RegisterService(() => new ElementaryNotesProviderFromDiatonicScale());
    }
}