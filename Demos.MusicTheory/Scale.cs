﻿using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.Mappers;

namespace Demos.MusicTheory;

public class Scale
{
    private DiatonicScale _diatonicScale;
    
    public Scale(NoteQuality noteQuality, NoteModifier noteModifier, ScaleQuality scaleQuality)
    {
        _diatonicScale = new DiatonicScale(NoteMapper.Map(noteQuality), NoteMapper.Map(noteModifier), ScaleMapper.Map(scaleQuality));
    }

    #region properties

    public NoteQuality Quality => NoteMapper.Map(_diatonicScale.QualityInternal);
    
    public NoteModifier Modifier => NoteMapper.Map(_diatonicScale.Modifier);
    
    public ScaleQuality Type => ScaleMapper.Map(_diatonicScale.DiatonicScaleType);

    #endregion
}