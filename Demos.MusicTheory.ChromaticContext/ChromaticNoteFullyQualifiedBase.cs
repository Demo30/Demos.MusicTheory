﻿using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Abstractions.Commons;
using Demos.MusicTheory.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using static Demos.MusicTheory.ChromaticContext.Constants.ChromaticContextConstants;

namespace Demos.MusicTheory.ChromaticContext
{
    public abstract class ChromaticNoteFullyQualifiedBase : 
        IContentEqual<ChromaticNoteFullyQualified>,
        IChromaticEntity
    {
        protected ChromaticNoteQuality QualityBase => baseChromaticCharacteristic.Quality;
        protected NotationSymbols ModifierBase => baseChromaticCharacteristic.Modifier;
        protected int OrderBase { get; init; }
        public abstract int ChromaticContextIndex { get; }

        private int QualityChromaticOffset => (int)QualityBase;
        private readonly ChromaticNoteElementary baseChromaticCharacteristic;
        private static readonly Dictionary<NotationSymbols, int> _modifierChromaticCorrection = new()
        {
            { NotationSymbols.None, 0 },
            { NotationSymbols.Sharp, 1 },
            { NotationSymbols.Flat, -1 },
            { NotationSymbols.DoubleSharp, +2 },
            { NotationSymbols.DoubleFlat, -2 }
        };

        public ChromaticNoteFullyQualifiedBase(ChromaticNoteQuality qualifier, int order, NotationSymbols modifier)
        {
            CheckConstructionArgumentValidity(qualifier, order, modifier);
            baseChromaticCharacteristic = new ChromaticNoteElementary(qualifier, modifier);
            OrderBase = order;
        }

        public abstract bool IsEqualByContent(ChromaticNoteFullyQualified comparedNote);

        protected string GetModifierString(NotationSymbols modifier)
        {
            return modifier == NotationSymbols.None ? "" : modifier.ToString();
        }

        protected int GetChromaticIndex()
        {
            Func<NotationSymbols, int> getChromaticOffsetModification = (modifier)
                => _modifierChromaticCorrection.ContainsKey(modifier) ? _modifierChromaticCorrection[modifier] : 0;

            int chromaticOffset =
                (OrderBase * CHROMATIC_STEPS_FULL_OCTAVE) +
                GetBaseChromaticOffset() +
                getChromaticOffsetModification(ModifierBase);

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
