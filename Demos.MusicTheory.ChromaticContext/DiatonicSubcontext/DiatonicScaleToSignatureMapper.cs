using System.Collections.Generic;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

internal static class DiatonicScaleToSignatureMapper
{
    private static Dictionary<DiatonicScale, KeySignatures> _map;
    
    public static Dictionary<DiatonicScale, KeySignatures> Map
    {
        get { return _map ??= GetMap; }
    }
    public static KeySignatures GetSignature(DiatonicScale scale)
    {
        return Map[scale];
    }

    private static Dictionary<DiatonicScale, KeySignatures> GetMap => new()
    {
        {new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Simple},
        {new DiatonicScale(NoteQualityInternal.G, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps1},
        {new DiatonicScale(NoteQualityInternal.D, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps2},
        {new DiatonicScale(NoteQualityInternal.A, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps3},
        {new DiatonicScale(NoteQualityInternal.E, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps4},
        {new DiatonicScale(NoteQualityInternal.B, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps5},
        {new DiatonicScale(NoteQualityInternal.C, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats7},
        {new DiatonicScale(NoteQualityInternal.F, NotationSymbols.Sharp, DiatonicScaleType.Major), KeySignatures.Sharps6},
        {new DiatonicScale(NoteQualityInternal.G, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats6},
        {new DiatonicScale(NoteQualityInternal.C, NotationSymbols.Sharp, DiatonicScaleType.Major), KeySignatures.Sharps7},
        {new DiatonicScale(NoteQualityInternal.D, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats5},
        {new DiatonicScale(NoteQualityInternal.A, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats4},
        {new DiatonicScale(NoteQualityInternal.E, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats3},
        {new DiatonicScale(NoteQualityInternal.B, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats2},
        {new DiatonicScale(NoteQualityInternal.F, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Flats1},
        {new DiatonicScale(NoteQualityInternal.A, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Simple},
        {new DiatonicScale(NoteQualityInternal.E, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Sharps1},
        {new DiatonicScale(NoteQualityInternal.B, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Sharps2},
        {new DiatonicScale(NoteQualityInternal.F, NotationSymbols.Sharp, DiatonicScaleType.Minor), KeySignatures.Sharps3},
        {new DiatonicScale(NoteQualityInternal.C, NotationSymbols.Sharp, DiatonicScaleType.Minor), KeySignatures.Sharps4},
        {new DiatonicScale(NoteQualityInternal.G, NotationSymbols.Sharp, DiatonicScaleType.Minor), KeySignatures.Sharps5},
        {new DiatonicScale(NoteQualityInternal.D, NotationSymbols.Sharp, DiatonicScaleType.Minor), KeySignatures.Sharps6},
        {new DiatonicScale(NoteQualityInternal.E, NotationSymbols.Flat, DiatonicScaleType.Minor), KeySignatures.Flats6},
        {new DiatonicScale(NoteQualityInternal.B, NotationSymbols.Flat, DiatonicScaleType.Minor), KeySignatures.Flats5},
        {new DiatonicScale(NoteQualityInternal.F, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Flats4},
        {new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Flats3},
        {new DiatonicScale(NoteQualityInternal.G, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Flats2},
        {new DiatonicScale(NoteQualityInternal.D, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Flats1},
        {new DiatonicScale(NoteQualityInternal.A, NotationSymbols.Flat, DiatonicScaleType.Minor), KeySignatures.Flats7},
        {new DiatonicScale(NoteQualityInternal.A, NotationSymbols.Sharp, DiatonicScaleType.Minor), KeySignatures.Sharps7}
    };
}