using System;

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

        public ChromaticNoteIntervalFullyQualified[] GetIntervals(ChromaticNoteFullyQualifiedRange range)
        {
            ChromaticNoteIntervalFullyQualified[] intervals = _chromaticIndexLengthProvider.GetIntervals(range.ChromaticIndexLength);
            return intervals;
        }
    }
}
