using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;
using Demos.MusicTheory.Mappers;
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
    }

    public IEnumerable<Note> GetNotesByChromaticDistance(Note sourceNote, int chromaticDistance, Direction direction = Direction.Right) =>
        _noteProviderFromNoteBySpan.GetEnharmonics(
                sourceNote.NoteInternal,
                chromaticDistance,
                NoteMapper.Map(direction))
            .Notes
            .Select(x => new Note(NoteMapper.Map(x.QualityInternal), NoteMapper.Map(x.Modifier), (uint) x.Order));


    public IEnumerable<Note> GetNotesByInterval(Note sourceNote, Interval interval, Direction direction = Direction.Right) =>
        _noteProviderFromNoteByInterval.GetEnharmonics(
                sourceNote.NoteInternal,
                new IntervalInternal(interval.DiatonicDegree, IntervalMapper.Map(interval.Quality)),
                NoteMapper.Map(direction))
            .Notes
            .Select(x => new Note(NoteMapper.Map(x.QualityInternal), NoteMapper.Map(x.Modifier), (uint) x.Order));
    
}