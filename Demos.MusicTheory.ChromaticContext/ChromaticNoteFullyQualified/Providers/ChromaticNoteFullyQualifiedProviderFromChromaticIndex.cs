using System;
using Demos.MusicTheory.ChromaticContext.Constants;
using Demos.MusicTheory.Commons;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.Helpers;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal class ChromaticNoteFullyQualifiedProviderFromChromaticIndex : IChromaticNoteFullyQualifiedProviderFromChromaticIndex
{
    public ChromaticNoteEnharmonicCluster GetEnharmonicNoteCluster(int chromaticIndex)
    {
        var baseOffset = chromaticIndex % ChromaticContextConstants.ChromaticStepsFullOctave;
        var order = chromaticIndex / ChromaticContextConstants.ChromaticStepsFullOctave;
        
        var qualities = Enum.GetValues<ChromaticNoteQuality>().Where(quality => quality != ChromaticNoteQuality.Unknown).ToList();
        var modifiers = GetModifiers();

        var matches = qualities
            .SelectMany(q => modifiers.Select(m => (q, m)))
            .Where(t => BaseChromaticIndexMapper.GetBaseChromaticOffset(t.q, t.m) == baseOffset);
        var notes = matches.Select(t => new ChromaticNoteFullyQualified(t.q, order, t.m)).ToArray();
        
        return new ChromaticNoteEnharmonicCluster(notes);
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