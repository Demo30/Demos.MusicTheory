using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

internal sealed class ElementaryNotesProviderFromDiatonicScale : IElementaryNotesProviderFromDiatonicScale
{
    private readonly Lazy<ElementaryNoteFromDiatonicScaleKeySignatureProvider> _provider;

    public ElementaryNotesProviderFromDiatonicScale() : this(new Lazy<ElementaryNoteFromDiatonicScaleKeySignatureProvider>(ServicesManager.GetService<ElementaryNoteFromDiatonicScaleKeySignatureProvider>))
    {
        
    }

    internal ElementaryNotesProviderFromDiatonicScale(Lazy<ElementaryNoteFromDiatonicScaleKeySignatureProvider> provider)
    {
        _provider = provider;
    }
    
    public IEnumerable<ElementaryNoteInternal> GetChromaticElementaryNotes(DiatonicScale scale)
    {
        var notes = _provider.Value.GetChromaticElementaryNotes(DiatonicScaleToSignatureMapper.GetSignature(scale))
            .OrderBy(x => (int)x.QualityInternal)
            .ToArray();

        var tonicNoteIndex = Array.IndexOf(notes,
            notes.Single(x => x.QualityInternal == scale.QualityInternal && x.Modifier == scale.Modifier));

        return Enumerable.Range(0, Constants.ChromaticContextConstants.DiatonicStepsInOctave)
            .Aggregate(
            new List<ElementaryNoteInternal>(), (list, index) =>
            {
                list.Add(notes[(index + tonicNoteIndex) % Constants.ChromaticContextConstants.DiatonicStepsInOctave]);
                return list;
            });
    }
}