using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.Mappers;

namespace Demos.MusicTheory;

public class Chord
{
    private readonly DiatonicChord _chordInternal;
    
    public Chord(Note baseNote, ChordQuality chordQuality)
    {
        _chordInternal = new DiatonicChord(NoteMapper.Map(baseNote), ChordMapper.Map(chordQuality));
    }

    // TODO: custom chords support?
    // public Chord(IEnumerable<Note> customStructure)
    // {
    //     _chordInternal = new DiatonicChord()
    // }

    #region Chord properties

    public Note BaseNote => NoteMapper.Map(_chordInternal.BaseNoteInternal);
    
    public ChordQuality Quality => ChordMapper.Map(_chordInternal.Quality);

    public IEnumerable<Note> ChordNotes => _chordInternal.ChordNotes.Select(NoteMapper.Map);

    public IEnumerable<IEnumerable<Note>> Inversions => _chordInternal.Inversions().Select(i => i.Select(NoteMapper.Map));

    #endregion
    
    #region Services

    public IEnumerable<Scale> GetMatchingScales() =>
        MusicTheoryService.Instance.GetScalesByChord(this);

    #endregion
}
