using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext
{
    public class ChromaticNoteFullyQualified : ChromaticNoteFullyQualifiedBase
    {
        public int Order => OrderBase;

        public ChromaticNoteQuality Quality => QualityBase;

        public NotationSymbols Modifier => ModifierBase;

        public override int ChromaticContextIndex => GetChromaticIndex();


        public ChromaticNoteFullyQualified(ChromaticNoteQuality qualifier, int order, NotationSymbols modifier) : base(qualifier, order, modifier)
        {

        }

        public override bool IsEqualByContent(ChromaticNoteFullyQualified comparedNote)
        {
            return
                comparedNote.OrderBase == OrderBase &&
                comparedNote.QualityBase == QualityBase &&
                comparedNote.ModifierBase == ModifierBase;
        }

        public override string ToString()
        {
            return $"{QualityBase}{OrderBase}{GetModifierString(ModifierBase)}";
        }
    }
}
