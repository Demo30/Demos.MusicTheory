using Demos.MusicTheory.ChromaticContext.Constants;
using System.Collections.Generic;
using System.Linq;
using static Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.ChromaticNoteIntervalFullyQualifiedBase;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;

public class ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan
{
    public ChromaticNoteIntervalFullyQualifiedEnharmonicCluster GetIntervals(int chromaticIndexSpan)
    {
        var enharmonicIntervalCluster = new List<ChromaticNoteIntervalFullyQualified>();
        var analysis = Analyse(chromaticIndexSpan);

        if (analysis.IsBlackKey)
        {
            enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(analysis.MainDiatonicScaleDegree, ChromaticNoteIntervalQuality.Augmented));

            var nextDiatonicScaleDegree = analysis.MainDiatonicScaleDegree + 1;
            var quality = IsPerfectType(nextDiatonicScaleDegree) ?
                ChromaticNoteIntervalQuality.Diminished :
                ChromaticNoteIntervalQuality.Minor;
                    
            enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(nextDiatonicScaleDegree, quality));
        }
        else
        {
            var quality =  analysis.IsPerfectType
                ? ChromaticNoteIntervalQuality.Perfect
                : ChromaticNoteIntervalQuality.Major;
            
            enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(analysis.MainDiatonicScaleDegree, quality));
                
            if (analysis.BaseDiatonicScaleDegree != (int)ChromaticNoteQuality.F)
            {
                enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(analysis.MainDiatonicScaleDegree + 1, ChromaticNoteIntervalQuality.Diminished));
            }
            
            if ((new[] { (int)ChromaticNoteQuality.C, (int)ChromaticNoteQuality.F }).Contains(analysis.BaseDiatonicScaleDegree) && analysis.MainDiatonicScaleDegree > 1)
            {
                enharmonicIntervalCluster.Add(new ChromaticNoteIntervalFullyQualified(analysis.MainDiatonicScaleDegree - 1, ChromaticNoteIntervalQuality.Augmented));
            }
        }

        return new ChromaticNoteIntervalFullyQualifiedEnharmonicCluster(enharmonicIntervalCluster.ToArray());
    }

    private static SpanAnalysisReport Analyse(int chromaticIndexSpan)
    {
        var subOctaves = chromaticIndexSpan / ChromaticContextConstants.ChromaticStepsFullOctave;
        var baseChromaticIndexSpan = chromaticIndexSpan - (subOctaves * ChromaticContextConstants.ChromaticStepsFullOctave);
        var diatonicCorrection = baseChromaticIndexSpan > 4 ? 1 : 0;
        var baseIntervalBaseNumber = ((baseChromaticIndexSpan + diatonicCorrection) / 2) + 1;
        var intervalBaseNumber = baseIntervalBaseNumber + (subOctaves * ChromaticContextConstants.DiatonicStepsInOctave);
        var isBlackKey =
            baseChromaticIndexSpan > 0 &&
            (
                (baseIntervalBaseNumber <= (int)ChromaticNoteQuality.E && !IsOddNumber(baseChromaticIndexSpan)) ||
                (baseIntervalBaseNumber > (int)ChromaticNoteQuality.E && IsOddNumber(baseChromaticIndexSpan))
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

    private static bool IsOddNumber(int number) => number % 2 == 0;
    
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