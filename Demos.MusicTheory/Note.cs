﻿using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Mappers;

namespace Demos.MusicTheory;

public class Note
{
    private NoteInternal _noteInternal;

    internal NoteInternal NoteInternal => _noteInternal;
    
    public Note(NoteQuality noteQuality, NoteModifier noteModifier = NoteModifier.Natural, uint order = 4)
    {
        _noteInternal = new NoteInternal(NoteMapper.Map(noteQuality), (int)order, NoteMapper.Map(noteModifier));
    }

    #region Note properties

    public uint Order => (uint)_noteInternal.Order;

    public NoteQuality NoteQuality => NoteMapper.Map(_noteInternal.QualityInternal);
    
    public NoteModifier NoteModifier => NoteMapper.Map(_noteInternal.Modifier);

    #endregion

    #region Note services

    public IEnumerable<Note> GetEnharmonicNotesByChromaticDistance(int chromaticDistance, Direction direction = Direction.Right) =>
        MusicTheoryService.Instance.GetNotesByChromaticDistance(this, chromaticDistance, direction);

    public IEnumerable<Note> GetEnharmonicNotesByInterval(Interval interval, Direction direction = Direction.Right) =>
        MusicTheoryService.Instance.GetNotesByInterval(this, interval, direction);


    #endregion
}