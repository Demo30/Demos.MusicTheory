using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

internal sealed class ElementaryNotesProviderFromDiatonicScale : IElementaryNotesProviderFromDiatonicScale
{
    private readonly Lazy<IElementaryNoteFromDiatonicScaleKeySignatureProvider> _provider;

    public ElementaryNotesProviderFromDiatonicScale() : this(new Lazy<IElementaryNoteFromDiatonicScaleKeySignatureProvider>(ServicesManager.GetService<ElementaryNoteFromDiatonicScaleKeySignatureProvider>))
    {
        
    }

    internal ElementaryNotesProviderFromDiatonicScale(Lazy<IElementaryNoteFromDiatonicScaleKeySignatureProvider> provider)
    {
        _provider = provider;
    }
    
    public IEnumerable<ElementaryNote> GetChromaticElementaryNotes(DiatonicScale scale)
    {
        var notes = _provider.Value.GetChromaticElementaryNotes(DiatonicScaleToSignatureMapper.GetSignature(scale))
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