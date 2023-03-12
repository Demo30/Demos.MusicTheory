using System;
using System.Net.NetworkInformation;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

namespace Demos.MusicTheory.Mappers;

internal static class ScaleMapper
{
    public static Scale Map(DiatonicScale diatonicScale) => new(
        NoteMapper.Map(diatonicScale.QualityInternal),
        NoteMapper.Map(diatonicScale.Modifier),
        Map(diatonicScale.DiatonicScaleType));
    
    public static DiatonicScale Map(Scale scale) => new(
        NoteMapper.Map(scale.Quality),
        NoteMapper.Map(scale.Modifier),
        Map(scale.Type));
    
    public static ScaleQuality Map (DiatonicScaleType diatonicScaleType)
    {
        return diatonicScaleType switch
        {
            DiatonicScaleType.Major => ScaleQuality.Major,
            DiatonicScaleType.Minor => ScaleQuality.Minor,
            _ => throw new InvalidOperationException()
        };
    }
    
    public static DiatonicScaleType Map (ScaleQuality scaleQuality)
    {
        return scaleQuality switch
        {
            ScaleQuality.Major => DiatonicScaleType.Major,
            ScaleQuality.Minor => DiatonicScaleType.Minor,
            _ => throw new InvalidOperationException()
        };
    }
}