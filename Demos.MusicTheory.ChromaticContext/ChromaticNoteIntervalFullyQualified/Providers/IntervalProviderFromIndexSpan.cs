using Demos.MusicTheory.ChromaticContext.Constants;
using System.Collections.Generic;
using System.Linq;
using static Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.
    NoteIntervalBase;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;

public class IntervalProviderFromIndexSpan
{
    public IntervalEnharmonics GetIntervals(int chromaticIndexSpan)
    {
        var enharmonicIntervalCluster = new List<Interval>();
        var analysis = Analyse(chromaticIndexSpan);

        if (analysis.IsBlackKey)
        {
            enharmonicIntervalCluster.Add(new Interval(analysis.MainDiatonicScaleDegree, IntervalQuality.Augmented));

            var nextDiatonicScaleDegree = analysis.MainDiatonicScaleDegree + 1;
            var quality = IsPerfectType(nextDiatonicScaleDegree) ? IntervalQuality.Diminished : IntervalQuality.Minor;

            enharmonicIntervalCluster.Add(new Interval(nextDiatonicScaleDegree, quality));
        }
        else
        {
            var quality = analysis.IsPerfectType
                ? IntervalQuality.Perfect
                : IntervalQuality.Major;

            enharmonicIntervalCluster.Add(new Interval(analysis.MainDiatonicScaleDegree, quality));

            if (analysis.BaseDiatonicScaleDegree != (int) NoteQuality.F)
                enharmonicIntervalCluster.Add(new Interval(analysis.MainDiatonicScaleDegree + 1,
                    IntervalQuality.Diminished));

            if (new[] {(int) NoteQuality.C, (int) NoteQuality.F}.Contains(analysis
                    .BaseDiatonicScaleDegree) && analysis.MainDiatonicScaleDegree > 1)
                enharmonicIntervalCluster.Add(new Interval(analysis.MainDiatonicScaleDegree - 1,
                    IntervalQuality.Augmented));
        }

        return new IntervalEnharmonics(enharmonicIntervalCluster.ToArray());
    }

    private static SpanAnalysisReport Analyse(int chromaticIndexSpan)
    {
        var subOctaves = chromaticIndexSpan / ChromaticContextConstants.ChromaticStepsFullOctave;
        var baseChromaticIndexSpan =
            chromaticIndexSpan - subOctaves * ChromaticContextConstants.ChromaticStepsFullOctave;
        var diatonicCorrection = baseChromaticIndexSpan > 4 ? 1 : 0;
        var baseIntervalBaseNumber = (baseChromaticIndexSpan + diatonicCorrection) / 2 + 1;
        var intervalBaseNumber = baseIntervalBaseNumber + subOctaves * ChromaticContextConstants.DiatonicStepsInOctave;
        var isBlackKey =
            baseChromaticIndexSpan > 0 &&
            (
                baseIntervalBaseNumber <= (int) NoteQuality.E && !IsOddNumber(baseChromaticIndexSpan) ||
                baseIntervalBaseNumber > (int) NoteQuality.E && IsOddNumber(baseChromaticIndexSpan)
            );

        return new SpanAnalysisReport
        {
            ChromaticIndexSpan = chromaticIndexSpan,
            DiatonicCorrection = diatonicCorrection,
            MainDiatonicScaleDegree = intervalBaseNumber,
            BaseDiatonicScaleDegree = baseIntervalBaseNumber,
            IsBlackKey = isBlackKey,
            SubOctaves = subOctaves,
            IsPerfectType = IsPerfectType(intervalBaseNumber)
        };
    }

    private static bool IsOddNumber(int number)
    {
        return number % 2 == 0;
    }

    private struct SpanAnalysisReport
    {
        public int ChromaticIndexSpan { get; set; }
        public bool IsBlackKey { get; set; }
        public int DiatonicCorrection { get; set; }
        public int MainDiatonicScaleDegree { get; set; }
        public int BaseDiatonicScaleDegree { get; set; }
        public int SubOctaves { get; set; }
        public bool IsPerfectType { get; set; }
    }
}