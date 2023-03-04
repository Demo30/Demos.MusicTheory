using Demos.MusicTheory.Commons;
using System;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.Helpers;
using static Demos.MusicTheory.ChromaticContext.Constants.ChromaticContextConstants;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

public abstract class NoteBase : IContentEqual<Note>, IChromaticEntity
{
    public abstract int ChromaticContextIndex { get; }
    protected NoteQuality QualityBase => _baseElementaryCharacteristic.Quality;
    protected NotationSymbols ModifierBase => _baseElementaryCharacteristic.Modifier;
    protected int OrderBase { get; }

    private readonly ElementaryNote _baseElementaryCharacteristic;

    protected NoteBase(NoteQuality qualifier, int order, NotationSymbols modifier)
    {
        CheckConstructionArgumentValidity(qualifier, order, modifier);
        _baseElementaryCharacteristic = new ElementaryNote(qualifier, modifier);
        OrderBase = order;
    }

    public abstract bool IsEqualByContent(Note comparedNote);

    protected static string GetModifierString(NotationSymbols modifier)
    {
        return modifier == NotationSymbols.None ? "" : modifier.ToString();
    }

    protected int GetChromaticIndex()
    {
        var baseOffsetInfo = BaseChromaticIndexMapper.GetBaseChromaticOffset(QualityBase, ModifierBase);
        return baseOffsetInfo.baseOffset +
               (OrderBase - baseOffsetInfo.orderCorrection) * ChromaticStepsFullOctave;
    }

    private static void CheckConstructionArgumentValidity(NoteQuality quality, int octaveOrder,
        NotationSymbols modifier)
    {
        if (octaveOrder < 0)
            throw new ArgumentException("Octave order cannot be negative.");

        if (quality == default)
            throw new ArgumentException("Chromatic note quality needs to be specified.");

        if (!ElementaryNote.RelevantNotationSymbols.Contains(modifier))
            throw new ArgumentException("Notation symbol is none of accepted types.");
    }
}