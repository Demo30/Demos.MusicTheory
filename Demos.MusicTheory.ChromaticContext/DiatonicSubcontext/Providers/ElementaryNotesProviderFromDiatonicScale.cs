using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

public sealed class ElementaryNotesProviderFromDiatonicScale : IElementaryNotesProviderFromDiatonicScale
{
    private readonly IElementaryNoteFromDiatonicScaleKeySignatureProvider _provider;

    public ElementaryNotesProviderFromDiatonicScale() : this(ServicesManager.GetService<ElementaryNoteFromDiatonicScaleKeySignatureProvider>())
    {
        
    }

    internal ElementaryNotesProviderFromDiatonicScale(IElementaryNoteFromDiatonicScaleKeySignatureProvider provider)
    {
        _provider = provider;
    }
    
    public IEnumerable<ElementaryNote> GetChromaticElementaryNotes(DiatonicScale scale)
    {
        var notes = _provider.GetChromaticElementaryNotes(DiatonicScaleToSignatureMapper.GetSignature(scale))
            .OrderBy(x => (int)x.Quality)
            .ToArray();

        var tonicNoteIndex = Array.IndexOf(notes,
            notes.Single(x => x.Quality == scale.Quality && x.Modifier == scale.Modifier));

        return Enumerable.Range(0, Constants.ChromaticContextConstants.DiatonicStepsInOctave)
            .Aggregate(
            new List<ElementaryNote>(), (list, index) =>
            {
                list.Add(notes[(index + tonicNoteIndex) % Constants.ChromaticContextConstants.DiatonicStepsInOctave]);
                return list;
            });
    }
}