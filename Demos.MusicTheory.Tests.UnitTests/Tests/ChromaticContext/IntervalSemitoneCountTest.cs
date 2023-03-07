using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class IntervalSemitoneCountTest
{
    [Theory]
    [TestCase(1, IntervalQuality.Perfect, 0)]
    [TestCase(1, IntervalQuality.Augmented, 1)]
    [TestCase(2, IntervalQuality.Diminished, 0)]
    [TestCase(2, IntervalQuality.Minor, 1)]
    [TestCase(2, IntervalQuality.Major, 2)]
    [TestCase(2, IntervalQuality.Augmented, 3)]
    [TestCase(3, IntervalQuality.Diminished, 2)]
    [TestCase(3, IntervalQuality.Minor, 3)]
    [TestCase(3, IntervalQuality.Major, 4)]
    [TestCase(3, IntervalQuality.Augmented, 5)]
    [TestCase(4, IntervalQuality.Diminished, 4)]
    [TestCase(4, IntervalQuality.Perfect, 5)]
    [TestCase(4, IntervalQuality.Augmented, 6)]
    [TestCase(5, IntervalQuality.Diminished, 6)]
    [TestCase(5, IntervalQuality.Perfect, 7)]
    [TestCase(5, IntervalQuality.Augmented, 8)]
    [TestCase(6, IntervalQuality.Diminished, 7)]
    [TestCase(6, IntervalQuality.Minor, 8)]
    [TestCase(6, IntervalQuality.Major, 9)]
    [TestCase(6, IntervalQuality.Augmented, 10)]
    [TestCase(7, IntervalQuality.Diminished, 9)]
    [TestCase(7, IntervalQuality.Minor, 10)]
    [TestCase(7, IntervalQuality.Major, 11)]
    [TestCase(7, IntervalQuality.Augmented, 12)]
    [TestCase(8, IntervalQuality.Diminished, 11)]
    [TestCase(8, IntervalQuality.Perfect, 12)]
    [TestCase(8, IntervalQuality.Augmented, 13)]
    public void TestSemitoneCountOneOctave(int intervalBaseNumber,
        IntervalQuality intervalChromaticQuality, int expectedSemitoneCount)
    {
        var interval = new Interval(intervalBaseNumber, intervalChromaticQuality);
        Assert.AreEqual(expectedSemitoneCount, interval.SemitoneCount);
    }

    [Theory]
    [TestCase(9, IntervalQuality.Diminished, 12)] // compound second
    [TestCase(9, IntervalQuality.Minor, 13)]
    [TestCase(9, IntervalQuality.Major, 14)]
    [TestCase(9, IntervalQuality.Augmented, 15)]
    [TestCase(10, IntervalQuality.Diminished, 14)] // compound third
    [TestCase(10, IntervalQuality.Minor, 15)]
    [TestCase(10, IntervalQuality.Major, 16)]
    [TestCase(10, IntervalQuality.Augmented, 17)]
    [TestCase(11, IntervalQuality.Diminished, 16)] // compound fourth
    [TestCase(11, IntervalQuality.Perfect, 17)]
    [TestCase(11, IntervalQuality.Augmented, 18)]
    [TestCase(12, IntervalQuality.Diminished, 18)] // compound fifth
    [TestCase(12, IntervalQuality.Perfect, 19)]
    [TestCase(12, IntervalQuality.Augmented, 20)]
    [TestCase(13, IntervalQuality.Diminished, 19)] // compound sixth
    [TestCase(13, IntervalQuality.Minor, 20)]
    [TestCase(13, IntervalQuality.Major, 21)]
    [TestCase(13, IntervalQuality.Augmented, 22)]
    [TestCase(14, IntervalQuality.Diminished, 21)] // compound seventh
    [TestCase(14, IntervalQuality.Minor, 22)]
    [TestCase(14, IntervalQuality.Major, 23)]
    [TestCase(14, IntervalQuality.Augmented, 24)]
    [TestCase(15, IntervalQuality.Diminished, 23)] // compound eigth
    [TestCase(15, IntervalQuality.Perfect, 24)]
    [TestCase(15, IntervalQuality.Augmented, 25)]
    [TestCase(16, IntervalQuality.Diminished, 24)] // double compound second
    [TestCase(16, IntervalQuality.Minor, 25)]
    [TestCase(16, IntervalQuality.Major, 26)]
    [TestCase(16, IntervalQuality.Augmented, 27)]
    public void TestSemitoneCountCompound(int intervalBaseNumber, IntervalQuality intervalChromaticQuality,
        int expectedSemitoneCount)
    {
        var interval = new Interval(intervalBaseNumber, intervalChromaticQuality);
        Assert.AreEqual(expectedSemitoneCount, interval.SemitoneCount);
    }
}