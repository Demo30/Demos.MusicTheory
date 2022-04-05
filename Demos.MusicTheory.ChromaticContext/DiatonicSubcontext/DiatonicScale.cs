using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

public record DiatonicScale(
    ChromaticNoteQuality Quality,
    NotationSymbols Modifier,
    DiatonicScaleType DiatonicScaleType);