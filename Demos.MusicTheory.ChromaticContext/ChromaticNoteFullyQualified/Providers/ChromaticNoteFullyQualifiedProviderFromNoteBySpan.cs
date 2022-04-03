using Demos.MusicTheory.Commons;
using static Demos.MusicTheory.Services.ServicesManager;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
internal class ChromaticNoteFullyQualifiedProviderFromNoteBySpan : IChromaticNoteFullyQualifiedProviderFromNoteBySpan
{
    private readonly IChromaticNoteFullyQualifiedProviderFromChromaticIndex _providerFromChromaticIndex;

    public ChromaticNoteFullyQualifiedProviderFromNoteBySpan() : this(GetService<ChromaticNoteFullyQualifiedProviderFromChromaticIndex>())
    {
        
    }

    internal ChromaticNoteFullyQualifiedProviderFromNoteBySpan(IChromaticNoteFullyQualifiedProviderFromChromaticIndex providerFromChromaticIndex)
    {
        _providerFromChromaticIndex = providerFromChromaticIndex;
    }
    
    public ChromaticNoteEnharmonicCluster GetEnharmonicNoteCluster(ChromaticNoteFullyQualified note, int chromaticIndexSpan, OneDimensionDirection direction)
    {
        var diff = direction == OneDimensionDirection.RIGHT ? chromaticIndexSpan : (-1 * chromaticIndexSpan);
        var spannedIndex = note.ChromaticContextIndex + diff;
        return _providerFromChromaticIndex.GetEnharmonicNoteCluster(spannedIndex);
    }
}