using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;
using Demos.MusicTheory.Mappers;
using Demos.MusicTheory.MidiAdapter;
using Demos.MusicTheory.Setup;

namespace Demos.MusicTheory;

public class MusicTheoryService
{
    private static MusicTheoryService _instance;

    public static MusicTheoryService Instance => _instance ??= new MusicTheoryService();
    
    private readonly INoteProviderFromIndex _noteProviderFromIndex;
    private readonly NoteProviderFromNoteByInterval _noteProviderFromNoteByInterval;
    private readonly NoteProviderFromNoteBySpan _noteProviderFromNoteBySpan;

    private readonly IntervalProviderFromIndexSpan _intervalProviderFromIndexSpan;
    private readonly IntervalProviderFromNoteRange _intervalProviderFromNoteRange;

    private readonly DiatonicScalesProviderFromNoteCluster _diatonicScalesProviderFromNoteCluster;
    private readonly IElementaryNoteFromDiatonicScaleKeySignatureProvider _elementaryNoteFromDiatonicScaleKeySignatureProvider;
    private readonly IElementaryNotesProviderFromDiatonicScale _elementaryNotesProviderFromDiatonicScale;
    private readonly NoteProviderFromNoteByDiatonicOffset _noteProviderFromNoteByDiatonicOffset;

    private readonly NoteFromMidiProvider _noteFromMidiProvider;
    
    public MusicTheoryService()
    {
        InitializationChecker.CheckLibraryInitialized();
        
        _noteProviderFromIndex = new NoteProviderFromIndex();
        _noteProviderFromNoteByInterval = new NoteProviderFromNoteByInterval();
        _noteProviderFromNoteBySpan = new NoteProviderFromNoteBySpan();
        
        _intervalProviderFromIndexSpan = new IntervalProviderFromIndexSpan();
        _intervalProviderFromNoteRange = new IntervalProviderFromNoteRange();

        _diatonicScalesProviderFromNoteCluster = new DiatonicScalesProviderFromNoteCluster();
        _elementaryNoteFromDiatonicScaleKeySignatureProvider = new ElementaryNoteFromDiatonicScaleKeySignatureProvider();
        _elementaryNotesProviderFromDiatonicScale = new ElementaryNotesProviderFromDiatonicScale();
        _noteProviderFromNoteByDiatonicOffset = new NoteProviderFromNoteByDiatonicOffset();

        _noteFromMidiProvider = new NoteFromMidiProvider();
    }

    public IEnumerable<Note> GetNotesBySemitonesDistance(Note sourceNote, int semitoneCount, Direction direction = Direction.Right) =>
        _noteProviderFromNoteBySpan.GetEnharmonics(
                sourceNote.NoteInternal,
                semitoneCount,
                NoteMapper.Map(direction))
            .Notes
            .Select(NoteMapper.Map);


    public IEnumerable<Note> GetNotesByInterval(Note sourceNote, Interval interval, Direction direction = Direction.Right) =>
        _noteProviderFromNoteByInterval.GetEnharmonics(
                sourceNote.NoteInternal,
                IntervalMapper.Map(interval),
                GeneralMapper.Map(direction))
            .Notes
            .Select(NoteMapper.Map);

    public IEnumerable<Interval> GetIntervalsBySemitoneDistance(int semitoneCount) =>
        _intervalProviderFromIndexSpan
            .GetIntervals(semitoneCount)
            .Intervals
            .Select(IntervalMapper.Map);

    public int GetSemitoneCountBetweenNotes(Note firstNote, Note secondNote) =>
        new NoteRangeInternal(firstNote.NoteInternal, secondNote.NoteInternal)
            .ChromaticIndexSpan;

    public Interval GetIntervalBetweenNotes(Note firstNote, Note secondNote)
    {
        var interval = _intervalProviderFromNoteRange
            .GetInterval(new NoteRangeInternal(firstNote.NoteInternal, secondNote.NoteInternal));
        
        return IntervalMapper.Map(interval);
    }

    public IEnumerable<NoteQuality> GetElementaryNotesByScale(Scale scale) =>
        _elementaryNotesProviderFromDiatonicScale.GetChromaticElementaryNotes(ScaleMapper.Map(scale))
            .Select(x => NoteMapper.Map(x.QualityInternal));

    public IEnumerable<Note> GetNotesByScale(Scale scale, uint order = 4) =>
        _elementaryNotesProviderFromDiatonicScale
            .GetChromaticElementaryNotes(ScaleMapper.Map(scale))
            .Select(en => new Note(NoteMapper.Map(en.QualityInternal), NoteMapper.Map(en.Modifier), order));

    public IEnumerable<Note> GetEnharmonicNotesFromMidiIndex(int midiIndex) =>
        _noteFromMidiProvider.GetEnharmonicNotesFromMidiIndex(midiIndex)
            .Notes
            .Select(NoteMapper.Map);
    
    public Note GetNoteByDiatonicStepsFromNoteWithinScale(Scale scale, Note referenceNote, int diatonicSteps /*, Direction direction = Direction.Right*/) =>
        NoteMapper.Map(
            _noteProviderFromNoteByDiatonicOffset.GetNote(ScaleMapper.Map(scale), NoteMapper.Map(referenceNote), diatonicSteps));

    public IEnumerable<Scale> GetScalesByElementaryNotes(IEnumerable<(NoteQuality quality, NoteModifier modifier)> elementaryNotes) =>
        _diatonicScalesProviderFromNoteCluster
            .GetDiatonicScales(elementaryNotes.Select(en =>
                new ElementaryNoteInternal(NoteMapper.Map(en.quality), NoteMapper.Map(en.modifier))))
            .Select(ScaleMapper.Map);
    
    public IEnumerable<Scale> GetScalesByNotes(IEnumerable<Note> notes) =>
        _diatonicScalesProviderFromNoteCluster
            .GetDiatonicScales(notes.Select(NoteMapper.Map))
            .Select(ScaleMapper.Map);

    public IEnumerable<Scale> GetScalesByChord(Chord chord) =>
        _diatonicScalesProviderFromNoteCluster
            .GetDiatonicScales(chord.ChordNotes.Select(NoteMapper.Map))
            .Select(ScaleMapper.Map);

}