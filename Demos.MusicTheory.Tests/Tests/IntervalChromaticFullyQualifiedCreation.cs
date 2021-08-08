using NUnit.Framework;
using System;

namespace Demos.MusicTheory.Tests.Tests
{
    [TestFixture]
    public class IntervalChromaticFullyQualifiedCreation
    {
        [Theory]
        [TestCase(1, IntervalChromaticQuality.Perfect)]
        [TestCase(1, IntervalChromaticQuality.Augmented)]
        [TestCase(2, IntervalChromaticQuality.Minor)]
        [TestCase(2, IntervalChromaticQuality.Major)]
        [TestCase(2, IntervalChromaticQuality.Augmented)]
        [TestCase(2, IntervalChromaticQuality.Diminished)]
        [TestCase(3, IntervalChromaticQuality.Minor)]
        [TestCase(3, IntervalChromaticQuality.Major)]
        [TestCase(3, IntervalChromaticQuality.Augmented)]
        [TestCase(3, IntervalChromaticQuality.Diminished)]
        [TestCase(4, IntervalChromaticQuality.Perfect)]
        [TestCase(4, IntervalChromaticQuality.Augmented)]
        [TestCase(4, IntervalChromaticQuality.Diminished)]
        [TestCase(5, IntervalChromaticQuality.Perfect)]
        [TestCase(5, IntervalChromaticQuality.Augmented)]
        [TestCase(5, IntervalChromaticQuality.Diminished)]
        [TestCase(6, IntervalChromaticQuality.Minor)]
        [TestCase(6, IntervalChromaticQuality.Major)]
        [TestCase(6, IntervalChromaticQuality.Augmented)]
        [TestCase(6, IntervalChromaticQuality.Diminished)]
        [TestCase(7, IntervalChromaticQuality.Minor)]
        [TestCase(7, IntervalChromaticQuality.Major)]
        [TestCase(7, IntervalChromaticQuality.Augmented)]
        [TestCase(7, IntervalChromaticQuality.Diminished)]
        [TestCase(8, IntervalChromaticQuality.Perfect)]
        [TestCase(8, IntervalChromaticQuality.Augmented)]
        [TestCase(8, IntervalChromaticQuality.Diminished)]
        public void ValidCreationArgumentsOneOctave(int intervalBaseNumber, IntervalChromaticQuality intervalChromaticQuality)
        {
            TestDelegate validCreation = () => new IntervalChromaticFullyQualified(intervalBaseNumber, intervalChromaticQuality);
            Assert.DoesNotThrow(validCreation);
        }

        [Theory]
        [TestCase(9, IntervalChromaticQuality.Minor)]
        [TestCase(9, IntervalChromaticQuality.Major)]
        [TestCase(9, IntervalChromaticQuality.Augmented)]
        [TestCase(9, IntervalChromaticQuality.Diminished)]
        [TestCase(10, IntervalChromaticQuality.Minor)]
        [TestCase(10, IntervalChromaticQuality.Major)]
        [TestCase(10, IntervalChromaticQuality.Augmented)]
        [TestCase(10, IntervalChromaticQuality.Diminished)]
        [TestCase(11, IntervalChromaticQuality.Perfect)]
        [TestCase(11, IntervalChromaticQuality.Augmented)]
        [TestCase(11, IntervalChromaticQuality.Diminished)]
        [TestCase(15, IntervalChromaticQuality.Perfect)]
        [TestCase(15, IntervalChromaticQuality.Augmented)]
        [TestCase(15, IntervalChromaticQuality.Diminished)]
        [TestCase(16, IntervalChromaticQuality.Minor)]
        [TestCase(16, IntervalChromaticQuality.Major)]
        [TestCase(16, IntervalChromaticQuality.Augmented)]
        [TestCase(16, IntervalChromaticQuality.Diminished)]
        public void ValidCreationArgumentsCompoundOctave(int intervalBaseNumber, IntervalChromaticQuality intervalChromaticQuality)
        {
            TestDelegate validCreation = () => new IntervalChromaticFullyQualified(intervalBaseNumber, intervalChromaticQuality);
            Assert.DoesNotThrow(validCreation);
        }

        [Theory]
        [TestCase(0, IntervalChromaticQuality.Perfect)]
        [TestCase(0, IntervalChromaticQuality.Minor)]
        [TestCase(0, IntervalChromaticQuality.Major)]
        [TestCase(0, IntervalChromaticQuality.Augmented)]
        [TestCase(0, IntervalChromaticQuality.Diminished)]
        [TestCase(1, IntervalChromaticQuality.Diminished)]
        [TestCase(1, IntervalChromaticQuality.Major)]
        [TestCase(1, IntervalChromaticQuality.Minor)]
        [TestCase(2, IntervalChromaticQuality.Perfect)]
        [TestCase(3, IntervalChromaticQuality.Perfect)]
        [TestCase(4, IntervalChromaticQuality.Major)]
        [TestCase(4, IntervalChromaticQuality.Minor)]
        public void InvalidCreationArgumentsOneOctave(int intervalBaseNumber, IntervalChromaticQuality intervalChromaticQuality)
        {
            TestDelegate invalidCreation = () => new IntervalChromaticFullyQualified(intervalBaseNumber, intervalChromaticQuality);
            Assert.Throws(Is.InstanceOf<Exception>(), invalidCreation);
        }

    }
}
