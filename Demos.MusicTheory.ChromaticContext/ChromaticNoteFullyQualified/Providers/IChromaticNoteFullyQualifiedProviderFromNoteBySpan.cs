using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal interface IChromaticNoteFullyQualifiedProviderFromNoteBySpan
{
    public ChromaticNoteEnharmonicCluster GetEnharmonicNoteCluster(ChromaticNoteFullyQualified note, int chromaticIndexSpan, OneDimensionDirection direction);
}