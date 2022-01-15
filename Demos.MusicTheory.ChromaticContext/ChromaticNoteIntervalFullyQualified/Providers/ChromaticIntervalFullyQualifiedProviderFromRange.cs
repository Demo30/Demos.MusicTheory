using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;

public class ChromaticIntervalFullyQualifiedProviderFromRange : IChromaticIntervalFullyQualifiedProviderFromRange
{
    private readonly IChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan _chromaticIndexLengthProvider;

    public ChromaticIntervalFullyQualifiedProviderFromRange(
        IChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan chromaticIndexLengthProvider)
    {
        _chromaticIndexLengthProvider = chromaticIndexLengthProvider;
    }

    public ChromaticNoteIntervalFullyQualifiedCluster GetIntervals(ChromaticNoteFullyQualifiedRange range) =>
        _chromaticIndexLengthProvider.GetIntervals(range.ChromaticIndexSpan);
}