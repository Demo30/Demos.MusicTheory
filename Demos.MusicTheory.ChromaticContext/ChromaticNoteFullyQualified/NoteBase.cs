using Demos.MusicTheory.Commons;
using System;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.Helpers;
using static Demos.MusicTheory.ChromaticContext.Constants.ChromaticContextConstants;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

internal abstract class NoteBase : IContentEqual<NoteInternal>, IChromaticEntity
{
    public abstract int ChromaticContextIndex { get; }
    protected NoteQualityInternal QualityInternalBase => _baseElementaryCharacteristic.QualityInternal;
    protected NotationSymbols ModifierBase => _baseElementaryCharacteristic.Modifier;
    protected int OrderBase { get; }

    private readonly ElementaryNoteInternal _baseElementaryCharacteristic;

    protected NoteBase(NoteQualityInternal qualifier, int order, NotationSymbols modifier)
    {
        CheckConstructionArgumentValidity(qualifier, order, modifier);
        _baseElementaryCharacteristic = new ElementaryNoteInternal(qualifier, modifier);
        OrderBase = order;
    }

    public abstract bool IsEqualByContent(NoteInternal comparedNoteInternal);

    protected static string GetModifierString(NotationSymbols modifier)
    {
        return modifier == NotationSymbols.None ? "" : modifier.ToString();
    }

    protected int GetChromaticIndex()
    {
        var baseOffsetInfo = BaseChromaticIndexMapper.GetBaseChromaticOffset(QualityInternalBase, ModifierBase);
        return baseOffsetInfo.baseOffset +
               (OrderBase - baseOffsetInfo.orderCorrection) * ChromaticStepsFullOctave;
    }

    private static void CheckConstructionArgumentValidity(NoteQualityInternal qualityInternal, int octaveOrder,
        NotationSymbols modifier)
    {
        if (octaveOrder < 0)
            throw new ArgumentException("Octave order cannot be negative.");

        if (qualityInternal == default)
            throw new ArgumentException("Chromatic note quality needs to be specified.");

        if (!ElementaryNoteInternal.RelevantNotationSymbols.Contains(modifier))
            throw new ArgumentException("Notation symbol is none of accepted types.");
    }
}