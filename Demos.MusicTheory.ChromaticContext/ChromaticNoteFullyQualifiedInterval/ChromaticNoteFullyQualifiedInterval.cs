namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualifiedInterval
{
    /// <summary>
    /// Ordinary interval between two fully qualified chromatic notes
    /// </summary>
    public class ChromaticNoteFullyQualifiedInterval : ChromaticNoteFullyQualifiedIntervalBase
    {
        public int ChromaticIndexRange => _chromaticIndexRangeCounter.GetChromaticIndexRange();

        public int SemitoneCount => ChromaticIndexRange;

        /// <summary>
        /// Starting from 0. One full octave has one suboctave.
        /// </summary>
        public int Suboctaves => _chromaticIndexRangeCounter.GetSuboctaves(IntervalBaseNumber);

        /// <summary>
        /// Subtracts compound octave intervals from the overal base number.
        /// </summary>
        /// <returns></returns>
        public int SimpleBaseNumber => _chromaticIndexRangeCounter.GetSimpleBaseNumber(IntervalBaseNumber);

        public ChromaticNoteFullyQualifiedInterval(int intervalBaseNumber, ChromaticNoteIntervalQuality quality) : base(intervalBaseNumber, quality)
        {

        }
    }
}
