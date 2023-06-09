﻿using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

internal class NoteInternal : NoteBase
{
    public int Order => OrderBase;

    public NoteQualityInternal QualityInternal => QualityInternalBase;

    public NotationSymbols Modifier => ModifierBase;

    public override int ChromaticContextIndex => GetChromaticIndex();

    public NoteInternal(NoteQualityInternal qualifier, int order, NotationSymbols modifier) : base(qualifier, order, modifier)
    {
    }

    public override bool IsEqualByContent(NoteInternal comparedNoteInternal)
    {
        return
            comparedNoteInternal.OrderBase == OrderBase &&
            comparedNoteInternal.QualityInternalBase == QualityInternalBase &&
            comparedNoteInternal.ModifierBase == ModifierBase;
    }

    public override string ToString() => $"{QualityInternalBase}{OrderBase}{GetModifierString(ModifierBase)}";

    public static bool operator >(NoteInternal a, NoteInternal b) => a.ChromaticContextIndex > b.ChromaticContextIndex;
    
    public static bool operator <(NoteInternal a, NoteInternal b) => a.ChromaticContextIndex < b.ChromaticContextIndex;
    
    public static bool operator ==(NoteInternal a, NoteInternal b) => a.ChromaticContextIndex == b.ChromaticContextIndex;
    
    public static bool operator !=(NoteInternal a, NoteInternal b) => !(a == b);
    
    public static bool operator <=(NoteInternal a, NoteInternal b) => a.ChromaticContextIndex <= b.ChromaticContextIndex;
    
    public static bool operator >=(NoteInternal a, NoteInternal b) => a.ChromaticContextIndex >= b.ChromaticContextIndex;
}