using Demos.MusicTheory.Commons;
using ChromaticNote = Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using static Demos.MusicTheory.Services.ServicesManager;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal class ChromaticNoteFullyQualifiedProviderFromNoteByInterval
{
    private readonly ChromaticNoteFullyQualifiedProviderFromNoteBySpan _providerBySpan;

    public ChromaticNoteFullyQualifiedProviderFromNoteByInterval() :
        this(GetService<ChromaticNoteFullyQualifiedProviderFromNoteBySpan>())
    {
        
    }
    internal ChromaticNoteFullyQualifiedProviderFromNoteByInterval(ChromaticNoteFullyQualifiedProviderFromNoteBySpan providerBySpan)
    {
        _providerBySpan = providerBySpan;
    }

    public ChromaticNoteEnharmonicCluster GetEnharmonicNoteCluster(ChromaticNoteFullyQualified note, ChromaticNote.ChromaticNoteIntervalFullyQualified interval, OneDimensionDirection direction) =>
        _providerBySpan.GetEnharmonicNoteCluster(note, interval.ChromaticIndexSpan, direction);
}