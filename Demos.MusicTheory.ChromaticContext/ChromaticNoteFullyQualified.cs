using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Abstractions.Commons;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Contexts.ChromaticContext;
using System;
using System.Linq;

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

        private readonly ChromaticNoteElementary baseChromaticCharacteristic;

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
            return Modifier == NotationSymbols.None ? "" : Modifier.ToString();
        }

        private int GetChromaticIndex()
        {
            Func<NotationSymbols, int> getModifierNumber = (modifier) =>
            {
                switch (modifier)
                {
                    case NotationSymbols.None: return 0;
                    case NotationSymbols.Sharp: return 1;
                    case NotationSymbols.Flat: return -1;
                    case NotationSymbols.DoubleSharp: return +2;
                    case NotationSymbols.DoubleFlat: return -2;
                    default: return 0;
                }
            };

            const int elementaryStep = 1;
            const int standardStep = elementaryStep * 2;

            int diatonicCorrection =
                (Order * -elementaryStep) + 
                ((int)Quality > (int)ChromaticNoteQuality.E ? -elementaryStep : 0);

            // max index in octave minus one step correction due to diatonic scale
            int stepsInFullOctave =
                standardStep * (int)ChromaticNoteQuality.B - elementaryStep;

            int baseNumberWithoutModifier =
                stepsInFullOctave * Order +
                (standardStep * (int)Quality) +
                diatonicCorrection;

            return baseNumberWithoutModifier + getModifierNumber(Modifier);
        }

        private void CheckConstructionArgumentValidity(ChromaticNoteQuality quality, int octaveOrder, NotationSymbols modifier)
        {
            if (octaveOrder < 0)
            {
                throw new ArgumentException("Octave order cannot be negative.");
            }

            if (quality == default)
            {
                throw new ArgumentException("Chromatic note quality needs to be specified.");
            }

            if (!ChromaticNoteElementary.RelevantNotationSymbols.Contains(modifier))
            {
                throw new ArgumentException("Notation symbol is none of accepted types.");
            }
        }
    }
}
