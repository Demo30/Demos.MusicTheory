﻿using Demos.MusicTheory.Commons;
using static Demos.MusicTheory.Services.ServicesManager;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal class NoteProviderFromNoteBySpan
{
    private readonly INoteProviderFromIndex _providerFromIndex;

    public NoteProviderFromNoteBySpan() : this(
        GetService<NoteProviderFromIndex>())
    {
    }

    internal NoteProviderFromNoteBySpan(
        INoteProviderFromIndex providerFromIndex)
    {
        _providerFromIndex = providerFromIndex;
    }

    public NoteEnharmonicsInternal GetEnharmonics(NoteInternal noteInternal, int chromaticIndexSpan, OneDimensionalDirection direction)
    {
        return _providerFromIndex.GetEnharmonics(GetSpannedChromaticIndex(noteInternal, chromaticIndexSpan, direction));
    }

    private static int GetSpannedChromaticIndex(IChromaticEntity note, int chromaticIndexSpan, OneDimensionalDirection direction)
    {
        return note.ChromaticContextIndex +
               (direction == OneDimensionalDirection.RIGHT ? chromaticIndexSpan : -chromaticIndexSpan);
    }
}