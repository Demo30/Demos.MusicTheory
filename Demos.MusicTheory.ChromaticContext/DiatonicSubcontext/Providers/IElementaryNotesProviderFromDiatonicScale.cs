using System.Collections.Generic;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

public interface IElementaryNotesProviderFromDiatonicScale
{
    public IEnumerable<ElementaryNote> GetChromaticElementaryNotes(DiatonicScale key);
}