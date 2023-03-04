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
        var (order, baseOffset) = Math.DivRem(chromaticIndex, BaseChromaticIndexMapper.BaseIndex + ChromaticContextConstants.ChromaticStepsFullOctave);

        var notes = GetQualityModifierCartesianProduct()
            .Select(noteInfo =>
            {
                var baseOffsetInfo = BaseChromaticIndexMapper.GetBaseChromaticOffset(noteInfo.NoteQuality, noteInfo.NotationSymbol);
                return (noteInfo.NoteQuality, Modifier: noteInfo.NotationSymbol, BaseOffset: baseOffsetInfo.baseOffset, Order: order + baseOffsetInfo.orderCorrection);
            })
            .Where(t => t.BaseOffset == baseOffset)
            .Where(t => t.Order >= 0)
            .Select(t => new Note(t.NoteQuality, t.Order, t.Modifier))
            .ToArray();

        return new NoteEnharmonics(notes);
    }

    private static IEnumerable<(NoteQuality NoteQuality, NotationSymbols NotationSymbol)> GetQualityModifierCartesianProduct()
    {
        var qualities = Enum.GetValues<NoteQuality>().Where(quality => quality != NoteQuality.Unknown)
            .ToList();
        var modifiers = GetModifiers();

        return qualities.SelectMany(q => modifiers.Select(m => (NoteQuality: q, NotationSymbol: m)));
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