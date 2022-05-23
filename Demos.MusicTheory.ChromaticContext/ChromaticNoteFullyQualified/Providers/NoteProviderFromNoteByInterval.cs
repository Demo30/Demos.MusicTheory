using Demos.MusicTheory.Commons;
using ChromaticNote = Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using static Demos.MusicTheory.Services.ServicesManager;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal class NoteProviderFromNoteByInterval
{
    private readonly INoteProviderFromIndex _providerByIndex;

    public NoteProviderFromNoteByInterval() : this(GetService<NoteProviderFromIndex>())
    {
    }

    internal NoteProviderFromNoteByInterval(INoteProviderFromIndex providerByIndex)
    {
        _providerByIndex = providerByIndex;
    }

    public NoteEnharmonics GetEnharmonics(Note note, IChromaticIndexSpan interval, OneDimensionalDirection direction)
    {
        return _providerByIndex.GetEnharmonics(GetSpannedIndex(note, interval, direction));
    }

    private static int GetSpannedIndex(IChromaticEntity note, IChromaticIndexSpan interval, OneDimensionalDirection direction)
    {
        return note.ChromaticContextIndex +
               (direction == OneDimensionalDirection.RIGHT ? interval.ChromaticIndexSpan : -interval.ChromaticIndexSpan);
    }
}