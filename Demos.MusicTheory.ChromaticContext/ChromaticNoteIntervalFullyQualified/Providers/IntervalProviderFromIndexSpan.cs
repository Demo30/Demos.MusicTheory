using Demos.MusicTheory.ChromaticContext.Constants;
using System.Collections.Generic;
using System.Linq;
using static Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.IntervalInternal;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;

internal class IntervalProviderFromIndexSpan
{
    public IntervalEnharmonicsInternal GetIntervals(int chromaticIndexSpan)
    {
        var intervalEnharmonics = new List<IntervalInternal>();
        var analysis = Analyse(chromaticIndexSpan);

        if (analysis.IsBlackKey)
        {
            intervalEnharmonics.Add(new IntervalInternal(analysis.MainDiatonicScaleDegree, IntervalQualityInternal.Augmented));

            var nextDiatonicScaleDegree = analysis.MainDiatonicScaleDegree + 1;
            var quality = IsPerfectType(nextDiatonicScaleDegree) ? IntervalQualityInternal.Diminished : IntervalQualityInternal.Minor;

            intervalEnharmonics.Add(new IntervalInternal(nextDiatonicScaleDegree, quality));
        }
        else
        {
            var quality = analysis.IsPerfectType
                ? IntervalQualityInternal.Perfect
                : IntervalQualityInternal.Major;

            intervalEnharmonics.Add(new IntervalInternal(analysis.MainDiatonicScaleDegree, quality));

            if (analysis.BaseDiatonicScaleDegree != (int) NoteQualityInternal.F)
                intervalEnharmonics.Add(new IntervalInternal(analysis.MainDiatonicScaleDegree + 1,
                    IntervalQualityInternal.Diminished));

            if (new[] {(int) NoteQualityInternal.C, (int) NoteQualityInternal.F}.Contains(analysis
                    .BaseDiatonicScaleDegree) && analysis.MainDiatonicScaleDegree > 1)
                intervalEnharmonics.Add(new IntervalInternal(analysis.MainDiatonicScaleDegree - 1,
                    IntervalQualityInternal.Augmented));
        }

        return new IntervalEnharmonicsInternal(intervalEnharmonics.ToArray());
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
                baseIntervalBaseNumber <= (int) NoteQualityInternal.E && !IsOddNumber(baseChromaticIndexSpan) ||
                baseIntervalBaseNumber > (int) NoteQualityInternal.E && IsOddNumber(baseChromaticIndexSpan)
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

    private record SpanAnalysisReport
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