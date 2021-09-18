using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Commons;
using System.Linq;

namespace Demos.MusicTheory.Contexts.ChromaticContext
{
    public class ChromaticSpace : MusicalEntitySpace<IChromaticNote>, IChromaticSpace
    {
        public ChromaticSpace(
            IChromaticSpaceIndexValidator indexValidator)
        {
            AddValidator(indexValidator);
        }

        public IChromaticNote GetNoteByChromaticIndex(int chromaticIndex)
        {
            return MusicalEntities.FirstOrDefault(note => note.ChromaticContextIndex.Equals(chromaticIndex));
        }
    }
}
