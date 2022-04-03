using Demos.MusicTheory.Commons;
using static Demos.MusicTheory.Services.ServicesManager;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal class ChromaticNoteFullyQualifiedProviderFromNoteBySpan
{
    private readonly IChromaticNoteFullyQualifiedProviderFromChromaticIndex _providerFromChromaticIndex;

    public ChromaticNoteFullyQualifiedProviderFromNoteBySpan() : this(GetService<ChromaticNoteFullyQualifiedProviderFromChromaticIndex>())
    {
    }

    internal ChromaticNoteFullyQualifiedProviderFromNoteBySpan(IChromaticNoteFullyQualifiedProviderFromChromaticIndex providerFromChromaticIndex)
    {
        _providerFromChromaticIndex = providerFromChromaticIndex;
    }
    
    public ChromaticNoteFullyQualifiedEnharmonicCluster GetEnharmonicNoteCluster(ChromaticNoteFullyQualified note, int chromaticIndexSpan, OneDimensionDirection direction) =>
        _providerFromChromaticIndex.GetEnharmonicNoteCluster(GetSpannedChromaticIndex(note, chromaticIndexSpan, direction));

    private static int GetSpannedChromaticIndex(IChromaticEntity note, int chromaticIndexSpan, OneDimensionDirection direction) =>
        note.ChromaticContextIndex + (direction == OneDimensionDirection.RIGHT ? chromaticIndexSpan : -chromaticIndexSpan);
}