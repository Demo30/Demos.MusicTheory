using NUnit.Framework;

namespace Demos.MusicTheory.UnitTests.Tests
{
    [TestFixture]
    public class IntervalChromaticFullyQualifiedSemitoneCount
    {
        [Theory]
        [TestCase(1, IntervalChromaticQuality.Perfect, 0)]
        [TestCase(1, IntervalChromaticQuality.Augmented, 1)]
        [TestCase(2, IntervalChromaticQuality.Diminished, 0)]
        [TestCase(2, IntervalChromaticQuality.Minor, 1)]
        [TestCase(2, IntervalChromaticQuality.Major, 2)]
        [TestCase(2, IntervalChromaticQuality.Augmented, 3)]
        [TestCase(3, IntervalChromaticQuality.Diminished, 2)]
        [TestCase(3, IntervalChromaticQuality.Minor, 3)]
        [TestCase(3, IntervalChromaticQuality.Major, 4)]
        [TestCase(3, IntervalChromaticQuality.Augmented, 5)]
        [TestCase(4, IntervalChromaticQuality.Diminished, 4)]
        [TestCase(4, IntervalChromaticQuality.Perfect, 5)]
        [TestCase(4, IntervalChromaticQuality.Augmented, 6)]
        [TestCase(5, IntervalChromaticQuality.Diminished, 6)]
        [TestCase(5, IntervalChromaticQuality.Perfect, 7)]
        [TestCase(5, IntervalChromaticQuality.Augmented, 8)]
        [TestCase(6, IntervalChromaticQuality.Diminished, 7)]
        [TestCase(6, IntervalChromaticQuality.Minor, 8)]
        [TestCase(6, IntervalChromaticQuality.Major, 9)]
        [TestCase(6, IntervalChromaticQuality.Augmented, 10)]
        [TestCase(7, IntervalChromaticQuality.Diminished, 9)]
        [TestCase(7, IntervalChromaticQuality.Minor, 10)]
        [TestCase(7, IntervalChromaticQuality.Major, 11)]
        [TestCase(7, IntervalChromaticQuality.Augmented, 12)]
        [TestCase(8, IntervalChromaticQuality.Diminished, 11)]
        [TestCase(8, IntervalChromaticQuality.Perfect, 12)]
        [TestCase(8, IntervalChromaticQuality.Augmented, 13)]
        public void TestSemitoneCountOneOctave(int intervalBaseNumber, IntervalChromaticQuality intervalChromaticQuality, int expectedSemitoneCount)
        {
            var interval = new IntervalChromaticFullyQualified(intervalBaseNumber, intervalChromaticQuality);
            Assert.AreEqual(expectedSemitoneCount, interval.GetSemitoneCount());
        }

        [Theory]
        [TestCase(9, IntervalChromaticQuality.Diminished, 12)] // compound second
        [TestCase(9, IntervalChromaticQuality.Minor, 13)]
        [TestCase(9, IntervalChromaticQuality.Major, 14)]
        [TestCase(9, IntervalChromaticQuality.Augmented, 15)]
        [TestCase(10, IntervalChromaticQuality.Diminished, 14)] // compound third
        [TestCase(10, IntervalChromaticQuality.Minor, 15)]
        [TestCase(10, IntervalChromaticQuality.Major, 16)]
        [TestCase(10, IntervalChromaticQuality.Augmented, 17)]
        [TestCase(11, IntervalChromaticQuality.Diminished, 16)] // compound fourth
        [TestCase(11, IntervalChromaticQuality.Perfect, 17)]
        [TestCase(11, IntervalChromaticQuality.Augmented, 18)]
        [TestCase(12, IntervalChromaticQuality.Diminished, 18)] // compound fifth
        [TestCase(12, IntervalChromaticQuality.Perfect, 19)]
        [TestCase(12, IntervalChromaticQuality.Augmented, 20)]
        [TestCase(13, IntervalChromaticQuality.Diminished, 19)] // compound sixth
        [TestCase(13, IntervalChromaticQuality.Minor, 20)]
        [TestCase(13, IntervalChromaticQuality.Major, 21)]
        [TestCase(13, IntervalChromaticQuality.Augmented, 22)]
        [TestCase(14, IntervalChromaticQuality.Diminished, 21)] // compound seventh
        [TestCase(14, IntervalChromaticQuality.Minor, 22)]
        [TestCase(14, IntervalChromaticQuality.Major, 23)]
        [TestCase(14, IntervalChromaticQuality.Augmented, 24)]
        [TestCase(15, IntervalChromaticQuality.Diminished, 23)] // compound eigth
        [TestCase(15, IntervalChromaticQuality.Perfect, 24)]
        [TestCase(15, IntervalChromaticQuality.Augmented, 25)]
        [TestCase(16, IntervalChromaticQuality.Diminished, 24)] // double compound second
        [TestCase(16, IntervalChromaticQuality.Minor, 25)]
        [TestCase(16, IntervalChromaticQuality.Major, 26)]
        [TestCase(16, IntervalChromaticQuality.Augmented, 27)]
        public void TestSemitoneCountCompound(int intervalBaseNumber, IntervalChromaticQuality intervalChromaticQuality, int expectedSemitoneCount)
        {
            var interval = new IntervalChromaticFullyQualified(intervalBaseNumber, intervalChromaticQuality);
            Assert.AreEqual(expectedSemitoneCount, interval.GetSemitoneCount());
        }
    }
}
