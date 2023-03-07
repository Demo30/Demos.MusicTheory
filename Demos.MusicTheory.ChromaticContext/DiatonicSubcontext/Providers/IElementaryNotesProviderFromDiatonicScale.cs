using System.Collections.Generic;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

internal interface IElementaryNotesProviderFromDiatonicScale
{
    public IEnumerable<ElementaryNote> GetChromaticElementaryNotes(DiatonicScale key);
}