using System.Collections.Generic;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

public static class DiatonicScaleToSignatureMapper
{
    public static KeySignatures GetSignature(DiatonicScale scale) => Map[scale];
    
    private static Dictionary<DiatonicScale, KeySignatures> Map => new()
    {
        { new DiatonicScale(ChromaticNoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Simple },
        { new DiatonicScale(ChromaticNoteQuality.G, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps1 },
        { new DiatonicScale(ChromaticNoteQuality.D, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps2 },
        { new DiatonicScale(ChromaticNoteQuality.A, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps3 },
        { new DiatonicScale(ChromaticNoteQuality.E, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps4 },
        { new DiatonicScale(ChromaticNoteQuality.B, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Sharps5 },
        { new DiatonicScale(ChromaticNoteQuality.C, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats7 },
        { new DiatonicScale(ChromaticNoteQuality.F, NotationSymbols.Sharp,  DiatonicScaleType.Major), KeySignatures.Sharps6 },
        { new DiatonicScale(ChromaticNoteQuality.G, NotationSymbols.Flat,  DiatonicScaleType.Major), KeySignatures.Flats6 },
        { new DiatonicScale(ChromaticNoteQuality.C, NotationSymbols.Sharp, DiatonicScaleType.Major), KeySignatures.Sharps7 },
        { new DiatonicScale(ChromaticNoteQuality.D, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats5 },
        { new DiatonicScale(ChromaticNoteQuality.A, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats4 },
        { new DiatonicScale(ChromaticNoteQuality.E, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats3 },
        { new DiatonicScale(ChromaticNoteQuality.B, NotationSymbols.Flat, DiatonicScaleType.Major), KeySignatures.Flats2 },
        { new DiatonicScale(ChromaticNoteQuality.F, NotationSymbols.None, DiatonicScaleType.Major), KeySignatures.Flats1 },
        { new DiatonicScale(ChromaticNoteQuality.A, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Simple },
        { new DiatonicScale(ChromaticNoteQuality.E, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Sharps1 },
        { new DiatonicScale(ChromaticNoteQuality.B, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Sharps2 },
        { new DiatonicScale(ChromaticNoteQuality.F, NotationSymbols.Sharp, DiatonicScaleType.Minor), KeySignatures.Sharps3 },
        { new DiatonicScale(ChromaticNoteQuality.C, NotationSymbols.Sharp, DiatonicScaleType.Minor), KeySignatures.Sharps4 },
        { new DiatonicScale(ChromaticNoteQuality.G, NotationSymbols.Sharp, DiatonicScaleType.Minor), KeySignatures.Sharps5 },
        { new DiatonicScale(ChromaticNoteQuality.D, NotationSymbols.Sharp, DiatonicScaleType.Minor), KeySignatures.Sharps6 },
        { new DiatonicScale(ChromaticNoteQuality.E, NotationSymbols.Flat,  DiatonicScaleType.Minor), KeySignatures.Flats6 },
        { new DiatonicScale(ChromaticNoteQuality.B, NotationSymbols.Flat,  DiatonicScaleType.Minor), KeySignatures.Flats5 },
        { new DiatonicScale(ChromaticNoteQuality.F, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Flats4 },
        { new DiatonicScale(ChromaticNoteQuality.C, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Flats3 },
        { new DiatonicScale(ChromaticNoteQuality.G, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Flats2 },
        { new DiatonicScale(ChromaticNoteQuality.D, NotationSymbols.None, DiatonicScaleType.Minor), KeySignatures.Flats1 },
    };
}