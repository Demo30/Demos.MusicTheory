using System;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

public class NoteProviderFromNoteByDiatonicOffset
{
    private readonly Lazy<IElementaryNotesProviderFromDiatonicScale> _provider;

    public NoteProviderFromNoteByDiatonicOffset() : this(new Lazy<IElementaryNotesProviderFromDiatonicScale>(ServicesManager.GetService<ElementaryNotesProviderFromDiatonicScale>))
    {
        
    }
    
    internal NoteProviderFromNoteByDiatonicOffset(Lazy<IElementaryNotesProviderFromDiatonicScale> provider)
    {
        _provider = provider;
    }
    public Note GetNote(DiatonicScale scale, Note referenceNote, int diatonicSteps)
    {
        var scaleElementaryNotes = _provider.Value.GetChromaticElementaryNotes(scale).ToArray();

        var matchingNote = scaleElementaryNotes.SingleOrDefault(x =>
            x.Quality == referenceNote.Quality && x.Modifier == referenceNote.Modifier);

        if (matchingNote is null)
        {
            throw new ArgumentException($"Reference note {referenceNote} incompatible with the given scale {scale}");
        }
        
        var orderDiff = ResolveOrderDifference(referenceNote, diatonicSteps);
        
        var refIndex = Array.IndexOf(scaleElementaryNotes, matchingNote);
        var index = (refIndex + diatonicSteps) % Constants.ChromaticContextConstants.DiatonicStepsInOctave;

        var destinationElementaryNote = scaleElementaryNotes[index];
        
        return new Note(destinationElementaryNote.Quality, referenceNote.Order + orderDiff, destinationElementaryNote.Modifier);
    }

    static int ResolveOrderDifference(Note referenceNote, int diatonicSteps)
    {
        if (diatonicSteps == 0)
        {
            return 0;
        }

        var auxDiff = ((int) referenceNote.Quality + diatonicSteps) /
                Constants.ChromaticContextConstants.DiatonicStepsInOctave;

        return diatonicSteps < 0 ? --auxDiff : auxDiff;

    }
}