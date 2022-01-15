using System.Linq;
using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext;

public class ChromaticSpace : MusicalEntitySpace<IChromaticEntity>, IChromaticSpace
{
    public ChromaticSpace(
        IChromaticSpaceIndexValidator indexValidator)
    {
        AddValidator(indexValidator);
    }

    public IChromaticEntity GetNoteByChromaticIndex(int chromaticIndex)
    {
        return MusicalEntities.FirstOrDefault(note => note.ChromaticContextIndex.Equals(chromaticIndex));
    }
}