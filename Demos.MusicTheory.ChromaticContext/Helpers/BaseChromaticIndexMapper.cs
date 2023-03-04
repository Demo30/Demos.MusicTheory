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

    private static Dictionary<(NoteQuality, NotationSymbols), (int baseOffset, int orderCorrection)> _map;

    public static void InitializeMapper()
    {
        _map = GetBaseChromaticIndexMap();
    }

    public static (int baseOffset, int orderCorrection) GetBaseChromaticOffset(NoteQuality quality, NotationSymbols modifier, bool wrapAround = true)
    {
        return IsInitialized
            ? _map[(quality, modifier)]
            : throw new ServiceInitializationException();
    }

    private static Dictionary<(NoteQuality, NotationSymbols), (int baseOffset, int orderCorrection)> GetBaseChromaticIndexMap()
    {
        var map = new Dictionary<(NoteQuality, NotationSymbols), (int baseOffset, int orderCorrection)>();

        var qualities = Enum.GetValues<NoteQuality>().Where(quality => quality != NoteQuality.Unknown).ToList();
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

    private static (int baseOffset, int orderCorrection) GetBaseOffset(NoteQuality quality, NotationSymbols modifier)
    {
        var baseOffset = GetBasicOffset(quality) - GetQualityOffsetCorrection(quality) + GetModifierCorrection(modifier);

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

    private static int GetQualityOffsetCorrection(NoteQuality quality)
    {
        return (int) quality > (int) NoteQuality.E ? ChromaticStepsElementaryStep : 0;
    }

    private static int GetBasicOffset(NoteQuality quality)
    {
        return (2 * ChromaticStepsElementaryStep) * ((int) quality - 1);
    }

    public static int BaseIndex => 0;

    private static readonly Dictionary<NotationSymbols, int> ModifierChromaticCorrection = new()
    {
        {NotationSymbols.None, 0},
        {NotationSymbols.Sharp, 1},
        {NotationSymbols.Flat, -1},
        {NotationSymbols.DoubleSharp, +2},
        {NotationSymbols.DoubleFlat, -2}
    };
}