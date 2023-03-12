using System;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.Constants;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.MidiAdapter;

internal class NoteFromMidiProvider : INoteFromMidiProvider
{
    private readonly Lazy<INoteProviderFromIndex> _noteProviderFromIndex;
    private const int MaxMidiIndex = 128;
    private const int BaseIndex = 21;
    private const int BaseIndexNoteQualityIndex = (int)NoteQualityInternal.A;

    public NoteFromMidiProvider() : this(new Lazy<INoteProviderFromIndex>(ServicesManager.GetService<NoteProviderFromIndex>()))
    {
    }
    
    public NoteFromMidiProvider(Lazy<INoteProviderFromIndex> noteProviderFromIndex)
    {
        _noteProviderFromIndex = noteProviderFromIndex;
    }

    public NoteEnharmonicsInternal GetEnharmonicNotesFromMidiIndex(int midiIndex)
    {
        if (midiIndex is < BaseIndex or > MaxMidiIndex)
        {
            throw new ArgumentException($"Valid midi index spans from {BaseIndex} to {MaxMidiIndex}.");
        }

        var fromBaseDiff = midiIndex - BaseIndex;
        (int order, int chromaticDiff) = Math.DivRem(fromBaseDiff, ChromaticContextConstants.ChromaticStepsFullOctave);

        var referenceNote = new NoteInternal(NoteQualityInternal.A, order, NotationSymbols.None); // this could be calculated to enhance efficiency
        var resultingEnharmonicNotes = _noteProviderFromIndex.Value.GetEnharmonics(referenceNote.ChromaticContextIndex + chromaticDiff);

        return resultingEnharmonicNotes;
    }
}