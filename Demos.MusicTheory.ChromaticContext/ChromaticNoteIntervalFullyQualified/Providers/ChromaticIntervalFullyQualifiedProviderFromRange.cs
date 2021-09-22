using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers
{
    public class ChromaticIntervalFullyQualifiedProviderFromRange
    {
        private readonly ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan _chromaticIndexLengthProvider;

        public ChromaticIntervalFullyQualifiedProviderFromRange(
            ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan chromaticIndexLengthProvider)
        {
            _chromaticIndexLengthProvider = chromaticIndexLengthProvider;
        }

        public ChromaticNoteIntervalFullyQualifiedCluster GetIntervals(ChromaticNoteFullyQualifiedRange range) =>
            _chromaticIndexLengthProvider.GetIntervals(range.ChromaticIndexSpan);
    }
}
