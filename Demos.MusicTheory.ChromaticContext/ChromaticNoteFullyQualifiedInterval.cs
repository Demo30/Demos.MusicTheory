namespace Demos.MusicTheory
{
    /// <summary>
    /// Ordinary interval between two fully qualified chromatic notes
    /// </summary>
    public class ChromaticNoteFullyQualifiedInterval : ChromaticNoteFullyQualifiedIntervalBase
    {
        public int SemitoneCount => _semitoneCounter.GetSemitoneCount();

        /// <summary>
        /// Starting from 0. One octave has one suboctave.
        /// </summary>
        public int Suboctaves => _semitoneCounter.GetSuboctaves(IntervalBaseNumber);

        /// <summary>
        /// Subtracts compound octave intervals from the overal base number.
        /// </summary>
        /// <returns></returns>
        public int SimpleBaseNumber => _semitoneCounter.GetSimpleBaseNumber(IntervalBaseNumber);

        public ChromaticNoteFullyQualifiedInterval(int intervalBaseNumber, ChromaticNoteIntervalQuality quality) : base(intervalBaseNumber, quality)
        {

        }
    }
}
