using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

internal class Note : NoteBase
{
    public int Order => OrderBase;

    public NoteQuality Quality => QualityBase;

    public NotationSymbols Modifier => ModifierBase;

    public override int ChromaticContextIndex => GetChromaticIndex();

    public Note(NoteQuality qualifier, int order, NotationSymbols modifier) : base(qualifier, order,
        modifier)
    {
    }

    public override bool IsEqualByContent(Note comparedNote)
    {
        return
            comparedNote.OrderBase == OrderBase &&
            comparedNote.QualityBase == QualityBase &&
            comparedNote.ModifierBase == ModifierBase;
    }

    public override string ToString()
    {
        return $"{QualityBase}{OrderBase}{GetModifierString(ModifierBase)}";
    }
}