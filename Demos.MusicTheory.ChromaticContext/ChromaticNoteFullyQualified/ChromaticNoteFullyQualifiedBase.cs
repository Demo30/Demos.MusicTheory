using Demos.MusicTheory.Commons;
using System;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.Helpers;
using static Demos.MusicTheory.ChromaticContext.Constants.ChromaticContextConstants;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

public abstract class ChromaticNoteFullyQualifiedBase : IContentEqual<ChromaticNoteFullyQualified>, IChromaticEntity
{
    public abstract int ChromaticContextIndex { get; }
    protected ChromaticNoteQuality QualityBase => _baseChromaticCharacteristic.Quality;
    protected NotationSymbols ModifierBase => _baseChromaticCharacteristic.Modifier;
    protected int OrderBase { get; }

    private readonly ChromaticNoteElementary _baseChromaticCharacteristic;

    public ChromaticNoteFullyQualifiedBase(ChromaticNoteQuality qualifier, int order, NotationSymbols modifier)
    {
        CheckConstructionArgumentValidity(qualifier, order, modifier);
        _baseChromaticCharacteristic = new ChromaticNoteElementary(qualifier, modifier);
        OrderBase = order;
    }

    public abstract bool IsEqualByContent(ChromaticNoteFullyQualified comparedNote);

    protected static string GetModifierString(NotationSymbols modifier) => modifier == NotationSymbols.None ? "" : modifier.ToString();

    protected int GetChromaticIndex() =>
        BaseChromaticIndexMapper.GetBaseChromaticOffset(QualityBase, ModifierBase) +
        (OrderBase * ChromaticStepsFullOctave);

    private static void CheckConstructionArgumentValidity(ChromaticNoteQuality quality, int octaveOrder, NotationSymbols modifier)
    {
        if (octaveOrder < 0)
            throw new ArgumentException("Octave order cannot be negative.");

        if (quality == default)
            throw new ArgumentException("Chromatic note quality needs to be specified.");

        if (!ChromaticNoteElementary.RelevantNotationSymbols.Contains(modifier))
            throw new ArgumentException("Notation symbol is none of accepted types.");
    }
}