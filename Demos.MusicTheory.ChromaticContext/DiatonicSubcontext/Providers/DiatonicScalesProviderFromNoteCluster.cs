using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

internal class DiatonicScalesProviderFromNoteCluster
{
    private readonly Lazy<IElementaryNoteFromDiatonicScaleKeySignatureProvider> _provider;

    public DiatonicScalesProviderFromNoteCluster() : this(new Lazy<IElementaryNoteFromDiatonicScaleKeySignatureProvider>(ServicesManager.GetService<ElementaryNoteFromDiatonicScaleKeySignatureProvider>))
    {
        
    }

    internal DiatonicScalesProviderFromNoteCluster(Lazy<IElementaryNoteFromDiatonicScaleKeySignatureProvider> provider)
    {
        _provider = provider;
    }

    public IEnumerable<DiatonicScale> GetDiatonicScales(IEnumerable<Note> notes)
    {
        return GetDiatonicScales(notes.Select(n => new ElementaryNote(n.Quality, n.Modifier)));
    }

    public IEnumerable<DiatonicScale> GetDiatonicScales(IEnumerable<ElementaryNote> elementaryNotes)
    {
        var suppliedNotes = elementaryNotes?.ToArray() ?? Array.Empty<ElementaryNote>();
        
        if (!suppliedNotes.Any())
        {
            return Array.Empty<DiatonicScale>();
        }
        
        return DiatonicScaleToSignatureMapper.Map.Keys
            .Select(scale => (scale, notes: _provider.Value.GetChromaticElementaryNotes(DiatonicScaleToSignatureMapper.GetSignature(scale))))
            .Where(x => suppliedNotes.All(suppliedNote => x.notes.Any(suppliedNote.IsEqualByContent)))
            .Select(x => x.scale);
    }
}