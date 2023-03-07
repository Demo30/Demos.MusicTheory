using System;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

internal class NoteProviderFromNoteByDiatonicOffset
{
    private readonly Lazy<IElementaryNotesProviderFromDiatonicScale> _provider;

    public NoteProviderFromNoteByDiatonicOffset() : this(new Lazy<IElementaryNotesProviderFromDiatonicScale>(ServicesManager.GetService<ElementaryNotesProviderFromDiatonicScale>))
    {
        
    }
    
    internal NoteProviderFromNoteByDiatonicOffset(Lazy<IElementaryNotesProviderFromDiatonicScale> provider)
    {
        _provider = provider;
    }
    public NoteInternal GetNote(DiatonicScale scale, NoteInternal referenceNoteInternal, int diatonicSteps)
    {
        var scaleElementaryNotes = _provider.Value.GetChromaticElementaryNotes(scale).ToArray();

        var matchingNote = scaleElementaryNotes.SingleOrDefault(x =>
            x.QualityInternal == referenceNoteInternal.QualityInternal && x.Modifier == referenceNoteInternal.Modifier);

        if (matchingNote is null)
        {
            throw new ArgumentException($"Reference note {referenceNoteInternal} incompatible with the given scale {scale}");
        }
        
        var orderDiff = ResolveOrderDifference(referenceNoteInternal, diatonicSteps);
        
        var refIndex = Array.IndexOf(scaleElementaryNotes, matchingNote);
        var index = (refIndex + diatonicSteps) % Constants.ChromaticContextConstants.DiatonicStepsInOctave;

        var destinationElementaryNote = scaleElementaryNotes[index];
        
        return new NoteInternal(destinationElementaryNote.QualityInternal, referenceNoteInternal.Order + orderDiff, destinationElementaryNote.Modifier);
    }

    static int ResolveOrderDifference(NoteInternal referenceNoteInternal, int diatonicSteps)
    {
        if (diatonicSteps == 0)
        {
            return 0;
        }

        var auxDiff = ((int) referenceNoteInternal.QualityInternal + diatonicSteps) /
                Constants.ChromaticContextConstants.DiatonicStepsInOctave;

        return diatonicSteps < 0 ? --auxDiff : auxDiff;

    }
}