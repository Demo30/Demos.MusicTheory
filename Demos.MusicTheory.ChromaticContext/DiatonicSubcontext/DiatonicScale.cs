using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

// If need arises to change this to class, don't forget to check equalities... and implement IEqualByContent
public record DiatonicScale(
    NoteQuality Quality,
    NotationSymbols Modifier,
    DiatonicScaleType DiatonicScaleType);