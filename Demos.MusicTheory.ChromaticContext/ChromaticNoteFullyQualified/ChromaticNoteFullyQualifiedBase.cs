using Demos.MusicTheory.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using static Demos.MusicTheory.ChromaticContext.Constants.ChromaticContextConstants;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

public abstract class ChromaticNoteFullyQualifiedBase : IContentEqual<ChromaticNoteFullyQualified>,
    IChromaticEntity
{
    protected ChromaticNoteQuality QualityBase => _baseChromaticCharacteristic.Quality;
    protected NotationSymbols ModifierBase => _baseChromaticCharacteristic.Modifier;
    protected int OrderBase { get; }
    public abstract int ChromaticContextIndex { get; }

    private int QualityChromaticOffset => (int)QualityBase;
    private readonly ChromaticNoteElementary _baseChromaticCharacteristic;
    private static readonly Dictionary<NotationSymbols, int> ModifierChromaticCorrection = new()
    {
        { NotationSymbols.None, 0 },
        { NotationSymbols.Sharp, 1 },
        { NotationSymbols.Flat, -1 },
        { NotationSymbols.DoubleSharp, +2 },
        { NotationSymbols.DoubleFlat, -2 }
    };

    public ChromaticNoteFullyQualifiedBase(ChromaticNoteQuality qualifier, int order, NotationSymbols modifier)
    {
        CheckConstructionArgumentValidity(qualifier, order, modifier);
        _baseChromaticCharacteristic = new ChromaticNoteElementary(qualifier, modifier);
        OrderBase = order;
    }

    public abstract bool IsEqualByContent(ChromaticNoteFullyQualified comparedNote);

    protected string GetModifierString(NotationSymbols modifier)
    {
        return modifier == NotationSymbols.None ? "" : modifier.ToString();
    }

    protected int GetChromaticIndex()
    {
        Func<NotationSymbols, int> getChromaticOffsetModification = (modifier)
            => ModifierChromaticCorrection.ContainsKey(modifier) ? ModifierChromaticCorrection[modifier] : 0;

        int chromaticOffset =
            (OrderBase * ChromaticStepsFullOctave) +
            GetBaseChromaticOffset() +
            getChromaticOffsetModification(ModifierBase);

        return chromaticOffset;
    }

    private int GetBaseChromaticOffset() =>
        ((2 * ChromaticStepsElementaryStep) * QualityChromaticOffset) -
        ((QualityChromaticOffset > (int)ChromaticNoteQuality.E) ? ChromaticStepsElementaryStep : 0);

    private void CheckConstructionArgumentValidity(ChromaticNoteQuality quality, int octaveOrder, NotationSymbols modifier)
    {
        if (octaveOrder < 0)
            throw new ArgumentException("Octave order cannot be negative.");

        if (quality == default)
            throw new ArgumentException("Chromatic note quality needs to be specified.");

        if (!ChromaticNoteElementary.RelevantNotationSymbols.Contains(modifier))
            throw new ArgumentException("Notation symbol is none of accepted types.");
    }
}