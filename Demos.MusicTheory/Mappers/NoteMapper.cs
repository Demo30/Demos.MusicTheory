using System;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.Mappers;

internal static class NoteMapper
{
    public static NoteQualityInternal Map(NoteQuality noteQuality) =>
        noteQuality switch
        {
            NoteQuality.C => NoteQualityInternal.C,
            NoteQuality.D => NoteQualityInternal.D,
            NoteQuality.E => NoteQualityInternal.E,
            NoteQuality.F => NoteQualityInternal.F,
            NoteQuality.G => NoteQualityInternal.G,
            NoteQuality.A => NoteQualityInternal.A,
            NoteQuality.B => NoteQualityInternal.B,
            _ => throw new InvalidOperationException()
        };
    
    public static NoteQuality Map(NoteQualityInternal noteQuality) =>
        noteQuality switch
        {
            NoteQualityInternal.C => NoteQuality.C,
            NoteQualityInternal.D => NoteQuality.D,
            NoteQualityInternal.E => NoteQuality.E,
            NoteQualityInternal.F => NoteQuality.F,
            NoteQualityInternal.G => NoteQuality.G,
            NoteQualityInternal.A => NoteQuality.A,
            NoteQualityInternal.B => NoteQuality.B,
            _ => throw new InvalidOperationException()
        };
    
    public static NotationSymbols Map(NoteModifier noteModifier) =>
        noteModifier switch
        {
            NoteModifier.Natural => NotationSymbols.None,
            NoteModifier.Sharp => NotationSymbols.Sharp,
            NoteModifier.Flat => NotationSymbols.Flat,
            NoteModifier.DoubleFlat => NotationSymbols.DoubleFlat,
            NoteModifier.DoubleSharp => NotationSymbols.DoubleSharp,
            NoteModifier.TripleSharp => NotationSymbols.TripleSharp,
            _ => throw new InvalidOperationException()
        };
    
    public static NoteModifier Map(NotationSymbols noteModifier) =>
        noteModifier switch
        {
            NotationSymbols.None => NoteModifier.Natural,
            NotationSymbols.Sharp => NoteModifier.Sharp,
            NotationSymbols.Flat => NoteModifier.Flat,
            NotationSymbols.DoubleFlat => NoteModifier.DoubleFlat,
            NotationSymbols.DoubleSharp => NoteModifier.DoubleSharp,
            NotationSymbols.TripleSharp => NoteModifier.TripleSharp,
            _ => throw new InvalidOperationException()
        };

    public static Direction Map(OneDimensionalDirection direction) =>
        direction switch
        {
            OneDimensionalDirection.RIGHT => Direction.Right,
            OneDimensionalDirection.LEFT => Direction.Left,
            _ => throw new InvalidOperationException()
        };
    
    public static OneDimensionalDirection Map(Direction direction) =>
        direction switch
        {
            Direction.Right => OneDimensionalDirection.RIGHT,
            Direction.Left => OneDimensionalDirection.LEFT,
            _ => throw new InvalidOperationException()
        };
}