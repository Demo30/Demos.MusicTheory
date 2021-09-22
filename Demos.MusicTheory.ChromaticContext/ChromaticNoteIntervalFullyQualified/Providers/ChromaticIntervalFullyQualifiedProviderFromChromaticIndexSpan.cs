using Demos.MusicTheory.ChromaticContext.Constants;
using System.Collections.Generic;
using System.Linq;
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
            public int MainDiatonicScaleDegree { get; set; }
            public int BaseDiatonicScaleDegree { get; set; }
            public int Suboctaves { get; set; }
            public bool IsPerfectType { get; set; }
        }

        public ChromaticNoteIntervalFullyQualifiedCluster GetIntervals(int chromaticIndexSpan)
        {
            // I'm sure there can be a nicer cleaner implementeation... all these ifs ain't no good :)

            List<ChromaticNoteIntervalFullyQualified> enharmonicIntervalCluster = new();

            SpanAnalysisReport report = Analyse(chromaticIndexSpan);

            if (report.IsBlackKey)
            {
                enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(report.MainDiatonicScaleDegree, ChromaticNoteIntervalQuality.Augmented));

                int nextDiatonicScaleDegree = report.MainDiatonicScaleDegree + 1;
                var quality = IsPerfectType(nextDiatonicScaleDegree) ?
                    ChromaticNoteIntervalQuality.Diminished :
                    ChromaticNoteIntervalQuality.Minor;
                    
                enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(nextDiatonicScaleDegree, quality));
            }
            else
            {
                ChromaticNoteIntervalQuality quality =  report.IsPerfectType?
                    ChromaticNoteIntervalQuality.Perfect :
                    ChromaticNoteIntervalQuality.Major;
                enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(report.MainDiatonicScaleDegree, quality));
                
                if (report.BaseDiatonicScaleDegree != (int)ChromaticNoteQuality.F)
                {
                    enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(report.MainDiatonicScaleDegree + 1, ChromaticNoteIntervalQuality.Diminished));
                }
                if ((new int[] { (int)ChromaticNoteQuality.C, (int)ChromaticNoteQuality.F }).Contains(report.BaseDiatonicScaleDegree) && report.MainDiatonicScaleDegree > 1)
                {
                    enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(report.MainDiatonicScaleDegree - 1, ChromaticNoteIntervalQuality.Augmented));
                }
            }

            return new ChromaticNoteIntervalFullyQualifiedCluster(enharmonicIntervalCluster.ToArray());
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
                MainDiatonicScaleDegree = intervalBaseNumber,
                BaseDiatonicScaleDegree = baseIntervalBaseNumber,
                IsBlackKey = isBlackKey,
                Suboctaves = suboctaves,
                IsPerfectType = IsPerfectType(intervalBaseNumber)
            };
        }

        private bool IsOddNumber(int number) => number % 2 == 0;
    }
}
