using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Services;
using static Demos.MusicTheory.ChromaticContext.Constants.ChromaticContextConstants;

namespace Demos.MusicTheory.ChromaticContext.Helpers;

// TODO: should probably get re-implemented as Singleton to prevent different results in tests based on whether base test is used or not... teardown singleton instance after test run...
internal static class BaseChromaticIndexMapper
{
    private static bool IsInitialized => _map is not null;

    private static Dictionary<(NoteQualityInternal, NotationSymbols), (int baseOffset, int orderCorrection)> _map;

    public static void InitializeMapper()
    {
        _map = GetBaseChromaticIndexMap();
    }

    public static (int baseOffset, int orderCorrection) GetBaseChromaticOffset(NoteQualityInternal qualityInternal, NotationSymbols modifier, bool wrapAround = true)
    {
        return IsInitialized
            ? _map[(qualityInternal, modifier)]
            : throw new ServiceInitializationException();
    }

    private static Dictionary<(NoteQualityInternal, NotationSymbols), (int baseOffset, int orderCorrection)> GetBaseChromaticIndexMap()
    {
        var map = new Dictionary<(NoteQualityInternal, NotationSymbols), (int baseOffset, int orderCorrection)>();

        var qualities = Enum.GetValues<NoteQualityInternal>().Where(quality => quality != NoteQualityInternal.Unknown).ToList();
        var modifiers = ModifierChromaticCorrection.Keys.ToList();

        qualities
            .SelectMany(q => modifiers.Select(m => (Quality: q, Modifier: m)))
            .ToList()
            .ForEach(t =>
            {
                var key = (t.Quality, t.Modifier);
                var value = GetBaseOffset(t.Quality, t.Modifier);
                map.Add(key, value);
            });

        return map;
    }

    private static (int baseOffset, int orderCorrection) GetBaseOffset(NoteQualityInternal qualityInternal, NotationSymbols modifier)
    {
        var baseOffset = GetBasicOffset(qualityInternal) - GetQualityOffsetCorrection(qualityInternal) + GetModifierCorrection(modifier);

        var baseOffsetWithinOneScaleRange =
            baseOffset >= BaseIndex && baseOffset < ChromaticStepsFullOctave;
        
        if (baseOffsetWithinOneScaleRange)
        {
            return (baseOffset, 0);
        }
        
        // wrapping around
        if (baseOffset < BaseIndex)
        {
            baseOffset += ChromaticStepsFullOctave; // max single order correction assumed
            return (baseOffset, +1);
        }
        
        baseOffset -= ChromaticStepsFullOctave; // max single order correction assumed
        return (baseOffset, -1);
    }

    private static int GetModifierCorrection(NotationSymbols modifier)
    {
        return ModifierChromaticCorrection.ContainsKey(modifier)
            ? ModifierChromaticCorrection[modifier]
            : throw new InvalidOperationException("Invalid modifier supplied.");
    }

    private static int GetQualityOffsetCorrection(NoteQualityInternal qualityInternal)
    {
        return (int) qualityInternal > (int) NoteQualityInternal.E ? ChromaticStepsElementaryStep : 0;
    }

    private static int GetBasicOffset(NoteQualityInternal qualityInternal)
    {
        return (2 * ChromaticStepsElementaryStep) * ((int) qualityInternal - 1);
    }

    public static int BaseIndex => 0;

    private static readonly Dictionary<NotationSymbols, int> ModifierChromaticCorrection = new()
    {
        {NotationSymbols.None, 0},
        {NotationSymbols.Sharp, 1},
        {NotationSymbols.Flat, -1},
        {NotationSymbols.DoubleSharp, +2},
        {NotationSymbols.DoubleFlat, -2},
        {NotationSymbols.TripleSharp, +3},
    };
}