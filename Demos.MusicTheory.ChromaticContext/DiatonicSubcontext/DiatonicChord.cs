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
    public DiatonicChordQuality Quality { get; }
    public NoteInternal BaseNoteInternal { get; }

    public DiatonicChord(NoteInternal baseNoteInternal, DiatonicChordQuality quality) : base(GetChordStructure(quality))
    {
        BaseNoteInternal = baseNoteInternal ?? throw new ArgumentNullException(nameof(baseNoteInternal));
        Quality = quality;
    }

    public IList<NoteInternal> ChordNotes => GetNoteSequenceFromChordStructure(MainChordStructure, BaseNoteInternal);

    public IEnumerable<IList<NoteInternal>> Inversions()
    {
        return GetChordStructureInversions()
            .Select((chordStructure, index) => GetNoteSequenceFromChordStructure(chordStructure, ChordNotes[index]));
    }

    private IList<NoteInternal> GetNoteSequenceFromChordStructure(ChordStructure chordStructure, NoteInternal baseNoteInternal)
    {
        var result = new List<NoteInternal> {BaseNoteInternal};

        for(var intervalIndex = 0; intervalIndex < chordStructure.OrderedIntervalStructure.Count; intervalIndex++)
        {
            var interval = chordStructure.OrderedIntervalStructure[intervalIndex];
            var referenceNote = result.Last();
            var nextNoteEnharmonics = ServicesManager.GetService<NoteProviderFromNoteBySpan>()
                .GetEnharmonics(referenceNote, interval.ChromaticIndexSpan, OneDimensionalDirection.RIGHT)
                .Notes;
            result.Add(GetPreferredNote(nextNoteEnharmonics, intervalIndex, baseNoteInternal));
        }

        return result;
    }

    private NoteInternal GetPreferredNote(IEnumerable<NoteInternal> enharmonics, int intervalIndex, NoteInternal baseNoteInternal)
    {
        var notes = enharmonics.ToArray();
        return notes.Single(n =>
        {
            var enharmonicNoteQualityIndex = (int)n.QualityInternal;
            
            // the chord is assumed to be constructed from evenly distant notes in thirds
            var expectedNoteQualityIndexBase = (int)baseNoteInternal.QualityInternal + ((intervalIndex + 1) * 2);
            var expectedNoteQualityIndex = ((expectedNoteQualityIndexBase - 1) % Constants.ChromaticContextConstants.DiatonicStepsInOctave) + 1;
            
            return enharmonicNoteQualityIndex == expectedNoteQualityIndex;
        });
    }

    private static ChordStructure GetChordStructure(DiatonicChordQuality quality)
    {
        var mappings = new Dictionary<DiatonicChordQuality, ChordStructure>
        {
            { DiatonicChordQuality.MajorTriad, new ChordStructure(new [] { new IntervalInternal(3, IntervalQualityInternal.Major), new IntervalInternal(3, IntervalQualityInternal.Minor) }) },
            { DiatonicChordQuality.MinorTriad, new ChordStructure(new [] { new IntervalInternal(3, IntervalQualityInternal.Minor), new IntervalInternal(3, IntervalQualityInternal.Major) }) },
            { DiatonicChordQuality.AugmentedTriad, new ChordStructure(new [] { new IntervalInternal(3, IntervalQualityInternal.Major), new IntervalInternal(3, IntervalQualityInternal.Major) }) },
            { DiatonicChordQuality.DiminishedTriad, new ChordStructure(new [] { new IntervalInternal(3, IntervalQualityInternal.Minor), new IntervalInternal(3, IntervalQualityInternal.Minor) }) },
        };

        return mappings[quality];
    }
}