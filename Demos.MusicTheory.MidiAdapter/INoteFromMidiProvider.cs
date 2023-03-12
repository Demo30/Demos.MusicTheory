using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

namespace Demos.MusicTheory.MidiAdapter;

internal interface INoteFromMidiProvider
{
    public NoteEnharmonicsInternal GetEnharmonicNotesFromMidiIndex(int midiIndex);
}