﻿using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.Setup;

public static class MusicTheoryServices
{
    public static void Setup()
    {
        if (ServicesManager.ServicesProvider.Services?.Count > 0)
        {
            return;
        }
        
        var provider = ServicesManager.ServicesProvider;
        provider.Services.Add(typeof(ChromaticNoteFullyQualifiedProviderFromNoteBySpan), () => new ChromaticNoteFullyQualifiedProviderFromNoteBySpan());
        provider.Services.Add(typeof(ChromaticNoteFullyQualifiedProviderFromNoteByInterval), () => new ChromaticNoteFullyQualifiedProviderFromNoteByInterval());
        provider.Services.Add(typeof(ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan), () => new ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan());
    }
}