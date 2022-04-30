using System;
using System.Collections;
using Demos.MusicTheory.ChromaticContext.Constants;
using Demos.MusicTheory.Commons;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.Helpers;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal class ChromaticNoteFullyQualifiedProviderFromChromaticIndex : IChromaticNoteFullyQualifiedProviderFromChromaticIndex
{
    public ChromaticNoteFullyQualifiedEnharmonicCluster GetEnharmonicNoteCluster(int chromaticIndex)
    {
        var (order, baseOffset) = Math.DivRem(chromaticIndex, ChromaticContextConstants.ChromaticStepsFullOctave);

        var notes = GetQualityModifierCartesianProduct()
            .Where(t => BaseChromaticIndexMapper.GetBaseChromaticOffset(t.Item1, t.Item2) == baseOffset)
            .Select(t => new ChromaticNoteFullyQualified(t.Item1, order, t.Item2))
            .ToArray();
        
        return new ChromaticNoteFullyQualifiedEnharmonicCluster(notes);
    }

    private static IEnumerable<(ChromaticNoteQuality, NotationSymbols)> GetQualityModifierCartesianProduct()
    {
        var qualities = Enum.GetValues<ChromaticNoteQuality>().Where(quality => quality != ChromaticNoteQuality.Unknown).ToList();
        var modifiers = GetModifiers();

        return qualities.SelectMany(q => modifiers.Select(m => (q, m)));
    }
    
    private static IEnumerable<NotationSymbols> GetModifiers() => new[]
    {
        NotationSymbols.None,
        NotationSymbols.Sharp,
        NotationSymbols.Flat,
        NotationSymbols.DoubleSharp,
        NotationSymbols.DoubleFlat
    };
}