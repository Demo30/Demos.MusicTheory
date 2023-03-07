using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

internal class DiatonicChord : ElementaryChord
{
    private readonly Lazy<NoteProviderFromNoteByInterval> _noteProviderFromNoteByInterval;
    public DiatonicChordQuality Quality { get; }
    public Note BaseNote { get; }

    public DiatonicChord(Note baseNote, DiatonicChordQuality quality) : 
        this(baseNote, quality, new Lazy<NoteProviderFromNoteByInterval>(ServicesManager.GetService<NoteProviderFromNoteByInterval>))
    {
        BaseNote = baseNote ?? throw new ArgumentNullException(nameof(baseNote));
        Quality = quality;
    }

    internal DiatonicChord(Note baseNote, DiatonicChordQuality quality, Lazy<NoteProviderFromNoteByInterval> noteProviderFromNoteByInterval) : base(GetChordStructure(quality))
    {
        _noteProviderFromNoteByInterval = noteProviderFromNoteByInterval;
    }

    public IList<Note> ChordNotes => GetNoteSequenceFromChordStructure(MainChordStructure, BaseNote);

    public IEnumerable<IList<Note>> Inversions()
    {
        return GetChordStructureInversions()
            .Select((chordStructure, index) => GetNoteSequenceFromChordStructure(chordStructure, ChordNotes[index]));
    }

    private IList<Note> GetNoteSequenceFromChordStructure(ChordStructure chordStructure, Note baseNote)
    {
        var result = new List<Note> {BaseNote};

        for(var intervalIndex = 0; intervalIndex < chordStructure.OrderedIntervalStructure.Count; intervalIndex++)
        {
            var interval = chordStructure.OrderedIntervalStructure[intervalIndex];
            var referenceNote = result.Last();
            var nextNoteEnharmonics = _noteProviderFromNoteByInterval
                .Value
                .GetEnharmonics(referenceNote, interval, OneDimensionalDirection.RIGHT)
                .Notes;
            result.Add(GetPreferredNote(nextNoteEnharmonics, intervalIndex, baseNote));
        }

        return result;
    }

    private Note GetPreferredNote(IEnumerable<Note> enharmonics, int intervalIndex, Note baseNote)
    {
        var notes = enharmonics.ToArray();
        return notes.Single(n =>
        {
            var enharmonicNoteQualityIndex = (int)n.Quality;
            
            // the chord is assumed to be constructed from evenly distant notes in thirds
            var expectedNoteQualityIndexBase = (int)baseNote.Quality + ((intervalIndex + 1) * 2);
            var expectedNoteQualityIndex = ((expectedNoteQualityIndexBase - 1) % Constants.ChromaticContextConstants.DiatonicStepsInOctave) + 1;
            
            return enharmonicNoteQualityIndex == expectedNoteQualityIndex;
        });
    }

    private static ChordStructure GetChordStructure(DiatonicChordQuality quality)
    {
        var mappings = new Dictionary<DiatonicChordQuality, ChordStructure>
        {
            { DiatonicChordQuality.MajorTriad, new ChordStructure(new [] { new Interval(3, IntervalQuality.Major), new Interval(3, IntervalQuality.Minor) }) },
            { DiatonicChordQuality.MinorTriad, new ChordStructure(new [] { new Interval(3, IntervalQuality.Minor), new Interval(3, IntervalQuality.Major) }) },
            { DiatonicChordQuality.AugmentedTriad, new ChordStructure(new [] { new Interval(3, IntervalQuality.Major), new Interval(3, IntervalQuality.Major) }) },
            { DiatonicChordQuality.DiminishedTriad, new ChordStructure(new [] { new Interval(3, IntervalQuality.Minor), new Interval(3, IntervalQuality.Minor) }) },
        };

        return mappings[quality];
    }
}