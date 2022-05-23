using System;
using System.Collections;
using Demos.MusicTheory.ChromaticContext.Constants;
using Demos.MusicTheory.Commons;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.Helpers;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal class NoteProviderFromIndex : INoteProviderFromIndex
{
    public NoteEnharmonics GetEnharmonics(int chromaticIndex)
    {
        var (order, baseOffset) = Math.DivRem(chromaticIndex, ChromaticContextConstants.ChromaticStepsFullOctave);

        var notes = GetQualityModifierCartesianProduct()
            .Where(t => BaseChromaticIndexMapper.GetBaseChromaticOffset(t.Item1, t.Item2) == baseOffset)
            .Select(t => new Note(t.Item1, order, t.Item2))
            .ToArray();

        return new NoteEnharmonics(notes);
    }

    private static IEnumerable<(NoteQuality, NotationSymbols)> GetQualityModifierCartesianProduct()
    {
        var qualities = Enum.GetValues<NoteQuality>().Where(quality => quality != NoteQuality.Unknown)
            .ToList();
        var modifiers = GetModifiers();

        return qualities.SelectMany(q => modifiers.Select(m => (q, m)));
    }

    private static IEnumerable<NotationSymbols> GetModifiers()
    {
        return new[]
        {
            NotationSymbols.None,
            NotationSymbols.Sharp,
            NotationSymbols.Flat,
            NotationSymbols.DoubleSharp,
            NotationSymbols.DoubleFlat
        };
    }
}