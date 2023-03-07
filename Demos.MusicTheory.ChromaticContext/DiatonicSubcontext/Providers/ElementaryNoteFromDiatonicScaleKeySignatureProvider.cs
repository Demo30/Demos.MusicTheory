using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

/// <summary>
/// Given a diatonic scale key signature, a collection of base characteristics is returned.
/// </summary>
internal sealed class ElementaryNoteFromDiatonicScaleKeySignatureProvider : IElementaryNoteFromDiatonicScaleKeySignatureProvider
{
    public IEnumerable<ElementaryNoteInternal> GetChromaticElementaryNotes(KeySignatures key)
    {
        var characteristics = GetFlatsAndSharps(key).ToList();

        var qualitiesWithoutModification = Enum.GetValues<NoteQualityInternal>()
            .Where(quality => !characteristics.Any(ch => ch.QualityInternal.Equals(quality)))
            .Where(quality => quality != NoteQualityInternal.Unknown)
            .Select(quality => new ElementaryNoteInternal(quality, NotationSymbols.None));

        characteristics.AddRange(qualitiesWithoutModification);

        return characteristics.ToArray();
    }

    private static IEnumerable<ElementaryNoteInternal> GetFlatsAndSharps(KeySignatures key)
    {
        var baseCharacteristics = new List<ElementaryNoteInternal>();

        switch (key)
        {
            case KeySignatures.Simple:
                break;
            case KeySignatures.Flats1:
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.B, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats2:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats1));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.E, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats3:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats2));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.A, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats4:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats3));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.D, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats5:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats4));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.G, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats6:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats5));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.C, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats7:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats6));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.F, NotationSymbols.Flat));
                break;
            case KeySignatures.Sharps1:
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.F, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps2:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps1));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.C, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps3:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps2));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.G, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps4:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps3));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.D, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps5:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps4));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.A, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps6:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps5));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.E, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps7:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps6));
                baseCharacteristics.Add(new ElementaryNoteInternal(NoteQualityInternal.B, NotationSymbols.Sharp));
                break;
            default:
                throw new Exception();
        }

        return baseCharacteristics;
    }
}