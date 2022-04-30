namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

/// <summary>
/// Ordinary interval between two fully qualified chromatic notes
/// </summary>
public class Interval : NoteIntervalBase
{
    public int SemitoneCount => ChromaticIndexSpan;

    /// <summary>
    /// Starting from 0. One full octave has one sub-octave.
    /// </summary>
    public int SubOctaves => ChromaticIndexSpanCounter.GetSubOctaves(DiatonicScaleDegree);

    /// <summary>
    /// Subtracts compound octave intervals from the overall base number.
    /// </summary>
    /// <returns></returns>
    public int SimpleBaseNumber => ChromaticIndexSpanCounter.GetSimpleBaseNumber(DiatonicScaleDegree);

    public Interval(int diatonicScaleDegree, IntervalQuality quality) : base(diatonicScaleDegree, quality)
    {
    }
}