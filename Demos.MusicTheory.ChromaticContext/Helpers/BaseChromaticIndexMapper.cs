using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Services;
using static Demos.MusicTheory.ChromaticContext.Constants.ChromaticContextConstants;

namespace Demos.MusicTheory.ChromaticContext.Helpers;

// TODO: should probably get re-implemented as Singleton to prevent different results in tests based on whether base test is used or not... teardown singleton instance after test run...
internal static class BaseChromaticIndexMapper
{
    private static bool IsInitialized => _map is not null;
    
    private static Dictionary<(ChromaticNoteQuality, NotationSymbols), int> _map;

    public static void InitializeMapper()
    {
        _map = GetBaseChromaticIndexMap();
    }

    public static int GetBaseChromaticOffset(ChromaticNoteQuality quality, NotationSymbols modifier) => IsInitialized 
        ? _map[(quality, modifier)]
        : throw new ServiceInitializationException();
    
    private static Dictionary<(ChromaticNoteQuality, NotationSymbols), int> GetBaseChromaticIndexMap()
    {
        var map = new Dictionary<(ChromaticNoteQuality, NotationSymbols), int>();
        
        var qualities = Enum.GetValues<ChromaticNoteQuality>().Where(quality => quality != ChromaticNoteQuality.Unknown).ToList();
        var modifiers = ModifierChromaticCorrection.Keys.ToList();
        
        qualities
            .SelectMany(q => modifiers.Select(m => (q, m)))
            .ToList()
            .ForEach(t => map.Add((t.q, t.m), GetBaseOffset(t.q, t.m)));
        
        return map;
    }

    private static int GetBaseOffset(ChromaticNoteQuality quality, NotationSymbols modifier) =>
        GetBasicOffset(quality) - GetQualityOffsetCorrection(quality) + GetModifierCorrection(modifier);
    
    private static int GetModifierCorrection(NotationSymbols modifier) => ModifierChromaticCorrection.ContainsKey(modifier)
        ? ModifierChromaticCorrection[modifier]
        : throw new InvalidOperationException("Invalid modifier supplied.");
    
    private static int GetQualityOffsetCorrection(ChromaticNoteQuality quality) => (int)quality > (int)ChromaticNoteQuality.E ? ChromaticStepsElementaryStep : 0;
    
    private static int GetBasicOffset(ChromaticNoteQuality quality) => 2 * ChromaticStepsElementaryStep * (int)quality;
    
    private static readonly Dictionary<NotationSymbols, int> ModifierChromaticCorrection = new()
    {
        { NotationSymbols.None, 0 },
        { NotationSymbols.Sharp, 1 },
        { NotationSymbols.Flat, -1 },
        { NotationSymbols.DoubleSharp, +2 },
        { NotationSymbols.DoubleFlat, -2 }
    };
}