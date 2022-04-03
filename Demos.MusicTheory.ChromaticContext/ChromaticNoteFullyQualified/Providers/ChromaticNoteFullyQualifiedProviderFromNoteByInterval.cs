using Demos.MusicTheory.Commons;
using ChromaticNote = Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using static Demos.MusicTheory.Services.ServicesManager;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal class ChromaticNoteFullyQualifiedProviderFromNoteByInterval
{
    private readonly IChromaticNoteFullyQualifiedProviderFromChromaticIndex _providerByChromaticIndex;

    public ChromaticNoteFullyQualifiedProviderFromNoteByInterval() : this(GetService<ChromaticNoteFullyQualifiedProviderFromChromaticIndex>())
    {
    }
    
    internal ChromaticNoteFullyQualifiedProviderFromNoteByInterval(IChromaticNoteFullyQualifiedProviderFromChromaticIndex providerByChromaticIndex)
    {
        _providerByChromaticIndex = providerByChromaticIndex;
    }

    public ChromaticNoteFullyQualifiedEnharmonicCluster GetEnharmonicNoteCluster(ChromaticNoteFullyQualified note, ChromaticNote.ChromaticNoteIntervalFullyQualified interval, OneDimensionDirection direction) =>
        _providerByChromaticIndex.GetEnharmonicNoteCluster(GetSpannedIndex(note, interval, direction));

    private static int GetSpannedIndex(IChromaticEntity note, ChromaticNote.ChromaticNoteIntervalFullyQualified interval, OneDimensionDirection direction) =>
        note.ChromaticContextIndex +
        (direction == OneDimensionDirection.RIGHT ? interval.SemitoneCount : -interval.SemitoneCount);
}