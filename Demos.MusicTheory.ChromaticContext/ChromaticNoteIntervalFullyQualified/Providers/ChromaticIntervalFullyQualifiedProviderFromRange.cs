using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;

public class ChromaticIntervalFullyQualifiedProviderFromRange
{
    private readonly ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan _chromaticIndexLengthProvider;

    public ChromaticIntervalFullyQualifiedProviderFromRange() : this(
        ServicesManager.GetService<ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan>()) {}
    
    internal ChromaticIntervalFullyQualifiedProviderFromRange(
        ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan chromaticIndexLengthProvider)
    {
        _chromaticIndexLengthProvider = chromaticIndexLengthProvider;
    }

    public ChromaticNoteIntervalFullyQualifiedCluster GetIntervals(ChromaticNoteFullyQualifiedRange range) =>
        _chromaticIndexLengthProvider.GetIntervals(range.ChromaticIndexSpan);
}