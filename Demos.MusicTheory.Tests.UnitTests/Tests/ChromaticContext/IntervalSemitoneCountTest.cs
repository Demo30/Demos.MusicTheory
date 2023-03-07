using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class IntervalSemitoneCountTest
{
    [Theory]
    [TestCase(1, IntervalQualityInternal.Perfect, 0)]
    [TestCase(1, IntervalQualityInternal.Augmented, 1)]
    [TestCase(2, IntervalQualityInternal.Diminished, 0)]
    [TestCase(2, IntervalQualityInternal.Minor, 1)]
    [TestCase(2, IntervalQualityInternal.Major, 2)]
    [TestCase(2, IntervalQualityInternal.Augmented, 3)]
    [TestCase(3, IntervalQualityInternal.Diminished, 2)]
    [TestCase(3, IntervalQualityInternal.Minor, 3)]
    [TestCase(3, IntervalQualityInternal.Major, 4)]
    [TestCase(3, IntervalQualityInternal.Augmented, 5)]
    [TestCase(4, IntervalQualityInternal.Diminished, 4)]
    [TestCase(4, IntervalQualityInternal.Perfect, 5)]
    [TestCase(4, IntervalQualityInternal.Augmented, 6)]
    [TestCase(5, IntervalQualityInternal.Diminished, 6)]
    [TestCase(5, IntervalQualityInternal.Perfect, 7)]
    [TestCase(5, IntervalQualityInternal.Augmented, 8)]
    [TestCase(6, IntervalQualityInternal.Diminished, 7)]
    [TestCase(6, IntervalQualityInternal.Minor, 8)]
    [TestCase(6, IntervalQualityInternal.Major, 9)]
    [TestCase(6, IntervalQualityInternal.Augmented, 10)]
    [TestCase(7, IntervalQualityInternal.Diminished, 9)]
    [TestCase(7, IntervalQualityInternal.Minor, 10)]
    [TestCase(7, IntervalQualityInternal.Major, 11)]
    [TestCase(7, IntervalQualityInternal.Augmented, 12)]
    [TestCase(8, IntervalQualityInternal.Diminished, 11)]
    [TestCase(8, IntervalQualityInternal.Perfect, 12)]
    [TestCase(8, IntervalQualityInternal.Augmented, 13)]
    public void TestSemitoneCountOneOctave(int intervalBaseNumber,
        IntervalQualityInternal intervalChromaticQualityInternal, int expectedSemitoneCount)
    {
        var interval = new IntervalInternal(intervalBaseNumber, intervalChromaticQualityInternal);
        Assert.AreEqual(expectedSemitoneCount, interval.SemitoneCount);
    }

    [Theory]
    [TestCase(9, IntervalQualityInternal.Diminished, 12)] // compound second
    [TestCase(9, IntervalQualityInternal.Minor, 13)]
    [TestCase(9, IntervalQualityInternal.Major, 14)]
    [TestCase(9, IntervalQualityInternal.Augmented, 15)]
    [TestCase(10, IntervalQualityInternal.Diminished, 14)] // compound third
    [TestCase(10, IntervalQualityInternal.Minor, 15)]
    [TestCase(10, IntervalQualityInternal.Major, 16)]
    [TestCase(10, IntervalQualityInternal.Augmented, 17)]
    [TestCase(11, IntervalQualityInternal.Diminished, 16)] // compound fourth
    [TestCase(11, IntervalQualityInternal.Perfect, 17)]
    [TestCase(11, IntervalQualityInternal.Augmented, 18)]
    [TestCase(12, IntervalQualityInternal.Diminished, 18)] // compound fifth
    [TestCase(12, IntervalQualityInternal.Perfect, 19)]
    [TestCase(12, IntervalQualityInternal.Augmented, 20)]
    [TestCase(13, IntervalQualityInternal.Diminished, 19)] // compound sixth
    [TestCase(13, IntervalQualityInternal.Minor, 20)]
    [TestCase(13, IntervalQualityInternal.Major, 21)]
    [TestCase(13, IntervalQualityInternal.Augmented, 22)]
    [TestCase(14, IntervalQualityInternal.Diminished, 21)] // compound seventh
    [TestCase(14, IntervalQualityInternal.Minor, 22)]
    [TestCase(14, IntervalQualityInternal.Major, 23)]
    [TestCase(14, IntervalQualityInternal.Augmented, 24)]
    [TestCase(15, IntervalQualityInternal.Diminished, 23)] // compound eigth
    [TestCase(15, IntervalQualityInternal.Perfect, 24)]
    [TestCase(15, IntervalQualityInternal.Augmented, 25)]
    [TestCase(16, IntervalQualityInternal.Diminished, 24)] // double compound second
    [TestCase(16, IntervalQualityInternal.Minor, 25)]
    [TestCase(16, IntervalQualityInternal.Major, 26)]
    [TestCase(16, IntervalQualityInternal.Augmented, 27)]
    public void TestSemitoneCountCompound(int intervalBaseNumber, IntervalQualityInternal intervalChromaticQualityInternal,
        int expectedSemitoneCount)
    {
        var interval = new IntervalInternal(intervalBaseNumber, intervalChromaticQualityInternal);
        Assert.AreEqual(expectedSemitoneCount, interval.SemitoneCount);
    }
}