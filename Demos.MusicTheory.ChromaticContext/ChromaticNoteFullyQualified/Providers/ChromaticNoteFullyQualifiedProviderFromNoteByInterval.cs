using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Services;
using ChNFQInterval = Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal class ChromaticNoteFullyQualifiedProviderFromNoteByInterval
{
    private readonly ChromaticNoteFullyQualifiedProviderFromNoteBySpan _providerBySpan;

    public ChromaticNoteFullyQualifiedProviderFromNoteByInterval() : this(
        ServicesManager.GetService<ChromaticNoteFullyQualifiedProviderFromNoteBySpan>())
    {
        
    }
    internal ChromaticNoteFullyQualifiedProviderFromNoteByInterval(ChromaticNoteFullyQualifiedProviderFromNoteBySpan providerBySpan)
    {
        _providerBySpan = providerBySpan;
    }

    public ChromaticNoteEnharmonicCluster GetEnharmonicNoteCluster(ChromaticNoteFullyQualified note, ChNFQInterval.ChromaticNoteIntervalFullyQualified interval, OneDimensionDirection direction) =>
        _providerBySpan.GetEnharmonicNoteCluster(note, interval.ChromaticIndexSpan, direction);
}