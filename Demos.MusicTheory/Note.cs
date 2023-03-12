using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Mappers;

namespace Demos.MusicTheory;

public class Note
{
    private readonly NoteInternal _noteInternal;

    internal NoteInternal NoteInternal => _noteInternal;
    
    public Note(NoteQuality noteQuality, NoteModifier noteModifier = NoteModifier.Natural, uint order = 4)
    {
        _noteInternal = new NoteInternal(NoteMapper.Map(noteQuality), (int)order, NoteMapper.Map(noteModifier));
    }

    #region Note properties

    public uint Order => (uint)_noteInternal.Order;

    public NoteQuality NoteQuality => NoteMapper.Map(_noteInternal.QualityInternal);
    
    public NoteModifier NoteModifier => NoteMapper.Map(_noteInternal.Modifier);

    public override string ToString() => _noteInternal.ToString();

    #endregion

    #region operators

    public static bool operator >(Note a, Note b) => a._noteInternal > b._noteInternal;
    
    public static bool operator <(Note a, Note b) => a._noteInternal < b._noteInternal;
    
    public static bool operator ==(Note a, Note b) => a._noteInternal == b._noteInternal;
    
    public static bool operator !=(Note a, Note b) => a._noteInternal != b._noteInternal;
    
    public static bool operator <=(Note a, Note b) => a._noteInternal <= b._noteInternal;
    
    public static bool operator >=(Note a, Note b) => a._noteInternal >= b._noteInternal;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        
        return _noteInternal == ((Note) obj)._noteInternal;
    }
    
    public override int GetHashCode()
    {
        return _noteInternal.GetHashCode();
    }

    #endregion

    #region Note services

    public IEnumerable<Note> GetEnharmonics() =>
        MusicTheoryService.Instance.GetNotesByInterval(this, Interval.PerfectUnison);

    public IEnumerable<Note> GetEnharmonicNotesBySemitoneDistance(int semitoneCount, Direction direction = Direction.Right) =>
        MusicTheoryService.Instance.GetNotesBySemitonesDistance(this, semitoneCount, direction);

    public IEnumerable<Note> GetEnharmonicNotesByInterval(Interval interval, Direction direction = Direction.Right) =>
        MusicTheoryService.Instance.GetNotesByInterval(this, interval, direction);

    public int GetSemitoneDistanceFromOtherNote(Note secondNote) =>
        MusicTheoryService.Instance.GetSemitoneCountBetweenNotes(this, secondNote);
    
    public Interval GetIntervalFromOtherNote(Note secondNote) =>
        MusicTheoryService.Instance.GetIntervalBetweenNotes(this, secondNote);

    public Note GetNoteByDiatonicStepsWithinScale(Scale scale, int diatonicSteps) =>
        MusicTheoryService.Instance.GetNoteByDiatonicStepsFromNoteWithinScale(scale, this, diatonicSteps);

    #endregion
}