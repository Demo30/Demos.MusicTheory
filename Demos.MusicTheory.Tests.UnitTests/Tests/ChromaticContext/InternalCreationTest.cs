using System;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class InternalCreationTest
{
    [Theory]
    [TestCase(1, IntervalQuality.Perfect)]
    [TestCase(1, IntervalQuality.Augmented)]
    [TestCase(2, IntervalQuality.Minor)]
    [TestCase(2, IntervalQuality.Major)]
    [TestCase(2, IntervalQuality.Augmented)]
    [TestCase(2, IntervalQuality.Diminished)]
    [TestCase(3, IntervalQuality.Minor)]
    [TestCase(3, IntervalQuality.Major)]
    [TestCase(3, IntervalQuality.Augmented)]
    [TestCase(3, IntervalQuality.Diminished)]
    [TestCase(4, IntervalQuality.Perfect)]
    [TestCase(4, IntervalQuality.Augmented)]
    [TestCase(4, IntervalQuality.Diminished)]
    [TestCase(5, IntervalQuality.Perfect)]
    [TestCase(5, IntervalQuality.Augmented)]
    [TestCase(5, IntervalQuality.Diminished)]
    [TestCase(6, IntervalQuality.Minor)]
    [TestCase(6, IntervalQuality.Major)]
    [TestCase(6, IntervalQuality.Augmented)]
    [TestCase(6, IntervalQuality.Diminished)]
    [TestCase(7, IntervalQuality.Minor)]
    [TestCase(7, IntervalQuality.Major)]
    [TestCase(7, IntervalQuality.Augmented)]
    [TestCase(7, IntervalQuality.Diminished)]
    [TestCase(8, IntervalQuality.Perfect)]
    [TestCase(8, IntervalQuality.Augmented)]
    [TestCase(8, IntervalQuality.Diminished)]
    public void ValidCreationArgumentsOneOctave(int intervalBaseNumber,
        IntervalQuality intervalChromaticQuality)
    {
        void ValidCreation()
        {
            new Interval(intervalBaseNumber, intervalChromaticQuality);
        }

        Assert.DoesNotThrow(ValidCreation);
    }

    [Theory]
    [TestCase(9, IntervalQuality.Minor)]
    [TestCase(9, IntervalQuality.Major)]
    [TestCase(9, IntervalQuality.Augmented)]
    [TestCase(9, IntervalQuality.Diminished)]
    [TestCase(10, IntervalQuality.Minor)]
    [TestCase(10, IntervalQuality.Major)]
    [TestCase(10, IntervalQuality.Augmented)]
    [TestCase(10, IntervalQuality.Diminished)]
    [TestCase(11, IntervalQuality.Perfect)]
    [TestCase(11, IntervalQuality.Augmented)]
    [TestCase(11, IntervalQuality.Diminished)]
    [TestCase(15, IntervalQuality.Perfect)]
    [TestCase(15, IntervalQuality.Augmented)]
    [TestCase(15, IntervalQuality.Diminished)]
    [TestCase(16, IntervalQuality.Minor)]
    [TestCase(16, IntervalQuality.Major)]
    [TestCase(16, IntervalQuality.Augmented)]
    [TestCase(16, IntervalQuality.Diminished)]
    public void ValidCreationArgumentsCompoundOctave(int intervalBaseNumber,
        IntervalQuality intervalChromaticQuality)
    {
        void ValidCreation()
        {
            new Interval(intervalBaseNumber, intervalChromaticQuality);
        }

        Assert.DoesNotThrow(ValidCreation);
    }

    [Theory]
    [TestCase(0, IntervalQuality.Perfect)]
    [TestCase(0, IntervalQuality.Minor)]
    [TestCase(0, IntervalQuality.Major)]
    [TestCase(0, IntervalQuality.Augmented)]
    [TestCase(0, IntervalQuality.Diminished)]
    [TestCase(1, IntervalQuality.Diminished)]
    [TestCase(1, IntervalQuality.Major)]
    [TestCase(1, IntervalQuality.Minor)]
    [TestCase(2, IntervalQuality.Perfect)]
    [TestCase(3, IntervalQuality.Perfect)]
    [TestCase(4, IntervalQuality.Major)]
    [TestCase(4, IntervalQuality.Minor)]
    public void InvalidCreationArgumentsOneOctave(int intervalBaseNumber,
        IntervalQuality intervalChromaticQuality)
    {
        void InvalidCreation()
        {
            new Interval(intervalBaseNumber, intervalChromaticQuality);
        }

        Assert.Throws(Is.InstanceOf<Exception>(), InvalidCreation);
    }
}