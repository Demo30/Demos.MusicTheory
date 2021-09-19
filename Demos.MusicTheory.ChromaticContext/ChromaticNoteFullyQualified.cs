using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Abstractions.Commons;
using Demos.MusicTheory.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using static Demos.MusicTheory.ChromaticContext.Constants.ChromaticContextConstants;

namespace Demos.MusicTheory.ChromaticContext
{
    public class ChromaticNoteFullyQualified : 
        IContentEqual<ChromaticNoteFullyQualified>,
        IChromaticEntity
    {
        public ChromaticNoteQuality Quality => baseChromaticCharacteristic.Quality;
        public NotationSymbols Modifier => baseChromaticCharacteristic.Modifier;
        public int Order { get; init; }
        public int ChromaticContextIndex => GetChromaticIndex();

        private int QualityChromaticOffset => (int)Quality;
        private readonly ChromaticNoteElementary baseChromaticCharacteristic;
        private static readonly Dictionary<NotationSymbols, int> _modifierChromaticCorrection = new()
        {
            { NotationSymbols.None, 0 },
            { NotationSymbols.Sharp, 1 },
            { NotationSymbols.Flat, -1 },
            { NotationSymbols.DoubleSharp, +2 },
            { NotationSymbols.DoubleFlat, -2 }
        };

        public ChromaticNoteFullyQualified(ChromaticNoteQuality qualifier, int order, NotationSymbols modifier)
        {
            CheckConstructionArgumentValidity(qualifier, order, modifier);
            baseChromaticCharacteristic = new ChromaticNoteElementary(qualifier, modifier);
            Order = order;
        }

        public bool IsEqualByContent(ChromaticNoteFullyQualified comparedNote)
        {
            return
                comparedNote.Order == Order &&
                comparedNote.Quality == Quality &&
                comparedNote.Modifier == Modifier;
        }

        public override string ToString()
        {
            return $"{Quality}{Order}{GetModifierString(Modifier)}";
        }

        private string GetModifierString(NotationSymbols modifier)
        {
            return modifier == NotationSymbols.None ? "" : modifier.ToString();
        }

        private int GetChromaticIndex()
        {
            Func<NotationSymbols, int> getChromaticOffsetModification = (modifier)
                => _modifierChromaticCorrection.ContainsKey(modifier) ? _modifierChromaticCorrection[modifier] : 0;

            int chromaticOffset =
                (Order * CHROMATIC_STEPS_FULL_OCTAVE) +
                GetBaseChromaticOffset() +
                getChromaticOffsetModification(Modifier);

            return chromaticOffset;
        }

        private int GetBaseChromaticOffset() =>
            ((2 * CHROMATIC_STEPS_ELEMENTARY_STEP) * QualityChromaticOffset) -
            ((QualityChromaticOffset > (int)ChromaticNoteQuality.E) ? CHROMATIC_STEPS_ELEMENTARY_STEP : 0);

        private void CheckConstructionArgumentValidity(ChromaticNoteQuality quality, int octaveOrder, NotationSymbols modifier)
        {
            if (octaveOrder < 0)
                throw new ArgumentException("Octave order cannot be negative.");

            if (quality == default)
                throw new ArgumentException("Chromatic note quality needs to be specified.");

            if (!ChromaticNoteElementary.RelevantNotationSymbols.Contains(modifier))
                throw new ArgumentException("Notation symbol is none of accepted types.");
        }
    }
}
