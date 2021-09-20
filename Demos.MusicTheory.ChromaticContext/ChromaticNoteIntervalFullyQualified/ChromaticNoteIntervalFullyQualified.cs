namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified
{
    /// <summary>
    /// Ordinary interval between two fully qualified chromatic notes
    /// </summary>
    public class ChromaticNoteIntervalFullyQualified : ChromaticNoteIntervalFullyQualifiedBase
    {
        public int SemitoneCount => ChromaticIndexSpan;

        /// <summary>
        /// Starting from 0. One full octave has one suboctave.
        /// </summary>
        public int Suboctaves => _chromaticIndexSpanCounter.GetSuboctaves(DiatonicScaleDegree);

        /// <summary>
        /// Subtracts compound octave intervals from the overal base number.
        /// </summary>
        /// <returns></returns>
        public int SimpleBaseNumber => _chromaticIndexSpanCounter.GetSimpleBaseNumber(DiatonicScaleDegree);

        public ChromaticNoteIntervalFullyQualified(int diatonicScaleDegree, ChromaticNoteIntervalQuality quality) : base(diatonicScaleDegree, quality)
        {

        }
    }
}
