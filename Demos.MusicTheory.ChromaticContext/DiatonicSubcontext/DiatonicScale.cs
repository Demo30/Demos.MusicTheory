using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

public record DiatonicScale(
    NoteQuality Quality,
    NotationSymbols Modifier,
    DiatonicScaleType DiatonicScaleType);