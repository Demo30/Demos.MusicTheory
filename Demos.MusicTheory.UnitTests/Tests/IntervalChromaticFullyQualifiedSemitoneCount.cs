using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualifiedInterval;
using NUnit.Framework;

namespace Demos.MusicTheory.UnitTests.Tests
{
    [TestFixture]
    public class IntervalChromaticFullyQualifiedSemitoneCount
    {
        [Theory]
        [TestCase(1, ChromaticNoteIntervalQuality.Perfect, 0)]
        [TestCase(1, ChromaticNoteIntervalQuality.Augmented, 1)]
        [TestCase(2, ChromaticNoteIntervalQuality.Diminished, 0)]
        [TestCase(2, ChromaticNoteIntervalQuality.Minor, 1)]
        [TestCase(2, ChromaticNoteIntervalQuality.Major, 2)]
        [TestCase(2, ChromaticNoteIntervalQuality.Augmented, 3)]
        [TestCase(3, ChromaticNoteIntervalQuality.Diminished, 2)]
        [TestCase(3, ChromaticNoteIntervalQuality.Minor, 3)]
        [TestCase(3, ChromaticNoteIntervalQuality.Major, 4)]
        [TestCase(3, ChromaticNoteIntervalQuality.Augmented, 5)]
        [TestCase(4, ChromaticNoteIntervalQuality.Diminished, 4)]
        [TestCase(4, ChromaticNoteIntervalQuality.Perfect, 5)]
        [TestCase(4, ChromaticNoteIntervalQuality.Augmented, 6)]
        [TestCase(5, ChromaticNoteIntervalQuality.Diminished, 6)]
        [TestCase(5, ChromaticNoteIntervalQuality.Perfect, 7)]
        [TestCase(5, ChromaticNoteIntervalQuality.Augmented, 8)]
        [TestCase(6, ChromaticNoteIntervalQuality.Diminished, 7)]
        [TestCase(6, ChromaticNoteIntervalQuality.Minor, 8)]
        [TestCase(6, ChromaticNoteIntervalQuality.Major, 9)]
        [TestCase(6, ChromaticNoteIntervalQuality.Augmented, 10)]
        [TestCase(7, ChromaticNoteIntervalQuality.Diminished, 9)]
        [TestCase(7, ChromaticNoteIntervalQuality.Minor, 10)]
        [TestCase(7, ChromaticNoteIntervalQuality.Major, 11)]
        [TestCase(7, ChromaticNoteIntervalQuality.Augmented, 12)]
        [TestCase(8, ChromaticNoteIntervalQuality.Diminished, 11)]
        [TestCase(8, ChromaticNoteIntervalQuality.Perfect, 12)]
        [TestCase(8, ChromaticNoteIntervalQuality.Augmented, 13)]
        public void TestSemitoneCountOneOctave(int intervalBaseNumber, ChromaticNoteIntervalQuality intervalChromaticQuality, int expectedSemitoneCount)
        {
            var interval = new ChromaticNoteFullyQualifiedInterval(intervalBaseNumber, intervalChromaticQuality);
            Assert.AreEqual(expectedSemitoneCount, interval.SemitoneCount);
        }

        [Theory]
        [TestCase(9, ChromaticNoteIntervalQuality.Diminished, 12)] // compound second
        [TestCase(9, ChromaticNoteIntervalQuality.Minor, 13)]
        [TestCase(9, ChromaticNoteIntervalQuality.Major, 14)]
        [TestCase(9, ChromaticNoteIntervalQuality.Augmented, 15)]
        [TestCase(10, ChromaticNoteIntervalQuality.Diminished, 14)] // compound third
        [TestCase(10, ChromaticNoteIntervalQuality.Minor, 15)]
        [TestCase(10, ChromaticNoteIntervalQuality.Major, 16)]
        [TestCase(10, ChromaticNoteIntervalQuality.Augmented, 17)]
        [TestCase(11, ChromaticNoteIntervalQuality.Diminished, 16)] // compound fourth
        [TestCase(11, ChromaticNoteIntervalQuality.Perfect, 17)]
        [TestCase(11, ChromaticNoteIntervalQuality.Augmented, 18)]
        [TestCase(12, ChromaticNoteIntervalQuality.Diminished, 18)] // compound fifth
        [TestCase(12, ChromaticNoteIntervalQuality.Perfect, 19)]
        [TestCase(12, ChromaticNoteIntervalQuality.Augmented, 20)]
        [TestCase(13, ChromaticNoteIntervalQuality.Diminished, 19)] // compound sixth
        [TestCase(13, ChromaticNoteIntervalQuality.Minor, 20)]
        [TestCase(13, ChromaticNoteIntervalQuality.Major, 21)]
        [TestCase(13, ChromaticNoteIntervalQuality.Augmented, 22)]
        [TestCase(14, ChromaticNoteIntervalQuality.Diminished, 21)] // compound seventh
        [TestCase(14, ChromaticNoteIntervalQuality.Minor, 22)]
        [TestCase(14, ChromaticNoteIntervalQuality.Major, 23)]
        [TestCase(14, ChromaticNoteIntervalQuality.Augmented, 24)]
        [TestCase(15, ChromaticNoteIntervalQuality.Diminished, 23)] // compound eigth
        [TestCase(15, ChromaticNoteIntervalQuality.Perfect, 24)]
        [TestCase(15, ChromaticNoteIntervalQuality.Augmented, 25)]
        [TestCase(16, ChromaticNoteIntervalQuality.Diminished, 24)] // double compound second
        [TestCase(16, ChromaticNoteIntervalQuality.Minor, 25)]
        [TestCase(16, ChromaticNoteIntervalQuality.Major, 26)]
        [TestCase(16, ChromaticNoteIntervalQuality.Augmented, 27)]
        public void TestSemitoneCountCompound(int intervalBaseNumber, ChromaticNoteIntervalQuality intervalChromaticQuality, int expectedSemitoneCount)
        {
            var interval = new ChromaticNoteFullyQualifiedInterval(intervalBaseNumber, intervalChromaticQuality);
            Assert.AreEqual(expectedSemitoneCount, interval.SemitoneCount);
        }
    }
}
