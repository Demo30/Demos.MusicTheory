using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

/// <summary>
/// Given a diatonic scale key signature, a collection of base characteristics is returned.
/// </summary>
public sealed class
    ElementaryNoteFromDiatonicScaleKeySignatureProvider : IElementaryNoteFromDiatonicScaleKeySignatureProvider
{
    public IEnumerable<ElementaryNote> GetChromaticElementaryNotes(KeySignatures key)
    {
        var characteristics = GetFlatsAndSharps(key).ToList();

        var qualitiesWithoutModification = Enum.GetValues<NoteQuality>()
            .Where(quality => !characteristics.Any(ch => ch.Quality.Equals(quality)))
            .Where(quality => quality != NoteQuality.Unknown)
            .Select(quality => new ElementaryNote(quality, NotationSymbols.None));

        characteristics.AddRange(qualitiesWithoutModification);

        return characteristics.ToArray();
    }

    private static IEnumerable<ElementaryNote> GetFlatsAndSharps(KeySignatures key)
    {
        var baseCharacteristics = new List<ElementaryNote>();

        switch (key)
        {
            case KeySignatures.Simple:
                break;
            case KeySignatures.Flats1:
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.B, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats2:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats1));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.E, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats3:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats2));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.A, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats4:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats3));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.D, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats5:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats4));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.G, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats6:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats5));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.C, NotationSymbols.Flat));
                break;
            case KeySignatures.Flats7:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats6));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.F, NotationSymbols.Flat));
                break;
            case KeySignatures.Sharps1:
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.F, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps2:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps1));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.C, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps3:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps2));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.G, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps4:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps3));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.D, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps5:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps4));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.A, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps6:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps5));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.E, NotationSymbols.Sharp));
                break;
            case KeySignatures.Sharps7:
                baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps6));
                baseCharacteristics.Add(new ElementaryNote(NoteQuality.B, NotationSymbols.Sharp));
                break;
            default:
                throw new Exception();
        }

        return baseCharacteristics;
    }
}