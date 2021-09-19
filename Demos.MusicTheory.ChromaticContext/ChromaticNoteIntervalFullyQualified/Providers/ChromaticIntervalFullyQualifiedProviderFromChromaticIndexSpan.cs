﻿using Demos.MusicTheory.ChromaticContext.Constants;
using System.Collections.Generic;
using static Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.ChromaticNoteIntervalFullyQualifiedBase;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers
{
    public class ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan
    {
        private struct SpanAnalysisReport
        {
            public int ChromaticIndexSpan { get; set; }
            public bool IsBlackKey { get; set; }
            public int DiatonicCorrection { get; set; }
            public int MainIntervalNumber { get; set; }
            public int BaseIntervalNumber { get; set; }
            public int Suboctaves { get; set; }
            public bool IsPerfectType { get; set; }
        }

        public ChromaticNoteIntervalFullyQualified[] GetIntervals(int chromaticIndexSpan)
        {
            List<ChromaticNoteIntervalFullyQualified> enharmonicIntervalCluster = new();

            SpanAnalysisReport report = Analyse(chromaticIndexSpan);

            if (report.IsBlackKey)
            {
                enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(report.MainIntervalNumber, ChromaticNoteIntervalQuality.Augmented));
                enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(report.MainIntervalNumber + 1, ChromaticNoteIntervalQuality.Minor));                
            }
            else
            {
                ChromaticNoteIntervalQuality quality =  report.IsPerfectType?
                    ChromaticNoteIntervalQuality.Perfect :
                    ChromaticNoteIntervalQuality.Major;
                enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(report.MainIntervalNumber, quality));
                enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(report.MainIntervalNumber + 1, ChromaticNoteIntervalQuality.Diminished));
                if (report.IsPerfectType && report.MainIntervalNumber > 1)
                {
                    enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(report.MainIntervalNumber - 1, ChromaticNoteIntervalQuality.Augmented));
                }
            }

            return enharmonicIntervalCluster.ToArray();
        }

        private SpanAnalysisReport Analyse(int chromaticIndexSpan)
        {
            int suboctaves = (chromaticIndexSpan / ChromaticContextConstants.CHROMATIC_STEPS_FULL_OCTAVE);
            int baseChromaticIndexSpan = chromaticIndexSpan - (suboctaves * ChromaticContextConstants.CHROMATIC_STEPS_FULL_OCTAVE);
            int diatonicCorrection = (baseChromaticIndexSpan > 4 ? 1 : 0);
            int baseIntervalBaseNumber =
                (((baseChromaticIndexSpan + diatonicCorrection) / 2) + 1);
            int intervalBaseNumber =
                baseIntervalBaseNumber +
                (suboctaves * ChromaticContextConstants.DIATONIC_STEPS_IN_OCTAVE);
            bool isBlackKey =
                baseChromaticIndexSpan > 0 &&
                (
                    (baseIntervalBaseNumber <= (int)ChromaticNoteQuality.E && !IsOddNumber(baseChromaticIndexSpan)) ||
                    (baseIntervalBaseNumber > (int)ChromaticNoteQuality.E && IsOddNumber(baseChromaticIndexSpan))
                );

            return new SpanAnalysisReport()
            {
                ChromaticIndexSpan = chromaticIndexSpan,
                DiatonicCorrection = diatonicCorrection,
                MainIntervalNumber = intervalBaseNumber,
                BaseIntervalNumber = baseIntervalBaseNumber,
                IsBlackKey = isBlackKey,
                Suboctaves = suboctaves,
                IsPerfectType = IsPerfectType(intervalBaseNumber)
            };
        }

        private bool IsOddNumber(int number) => number % 2 == 0;
    }
}
