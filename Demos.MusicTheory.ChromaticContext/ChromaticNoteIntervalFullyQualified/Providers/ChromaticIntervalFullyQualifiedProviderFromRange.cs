using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;

public class ChromaticIntervalFullyQualifiedProviderFromRange
{
    private readonly ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan _chromaticIndexSpanProvider;

    public ChromaticIntervalFullyQualifiedProviderFromRange() : this(ServicesManager.GetService<ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan>())
    {
    }
    
    internal ChromaticIntervalFullyQualifiedProviderFromRange(ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan chromaticIndexSpanProvider)
    {
        _chromaticIndexSpanProvider = chromaticIndexSpanProvider;
    }

    public ChromaticNoteIntervalFullyQualifiedEnharmonicCluster GetIntervals(ChromaticNoteFullyQualifiedRange range) =>
        _chromaticIndexSpanProvider.GetIntervals(range.ChromaticIndexSpan);
}