using System;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class InternalCreationTest
{
    [Theory]
    [TestCase(1, IntervalQualityInternal.Perfect)]
    [TestCase(1, IntervalQualityInternal.Augmented)]
    [TestCase(2, IntervalQualityInternal.Minor)]
    [TestCase(2, IntervalQualityInternal.Major)]
    [TestCase(2, IntervalQualityInternal.Augmented)]
    [TestCase(2, IntervalQualityInternal.Diminished)]
    [TestCase(3, IntervalQualityInternal.Minor)]
    [TestCase(3, IntervalQualityInternal.Major)]
    [TestCase(3, IntervalQualityInternal.Augmented)]
    [TestCase(3, IntervalQualityInternal.Diminished)]
    [TestCase(4, IntervalQualityInternal.Perfect)]
    [TestCase(4, IntervalQualityInternal.Augmented)]
    [TestCase(4, IntervalQualityInternal.Diminished)]
    [TestCase(5, IntervalQualityInternal.Perfect)]
    [TestCase(5, IntervalQualityInternal.Augmented)]
    [TestCase(5, IntervalQualityInternal.Diminished)]
    [TestCase(6, IntervalQualityInternal.Minor)]
    [TestCase(6, IntervalQualityInternal.Major)]
    [TestCase(6, IntervalQualityInternal.Augmented)]
    [TestCase(6, IntervalQualityInternal.Diminished)]
    [TestCase(7, IntervalQualityInternal.Minor)]
    [TestCase(7, IntervalQualityInternal.Major)]
    [TestCase(7, IntervalQualityInternal.Augmented)]
    [TestCase(7, IntervalQualityInternal.Diminished)]
    [TestCase(8, IntervalQualityInternal.Perfect)]
    [TestCase(8, IntervalQualityInternal.Augmented)]
    [TestCase(8, IntervalQualityInternal.Diminished)]
    public void ValidCreationArgumentsOneOctave(int intervalBaseNumber,
        IntervalQualityInternal intervalChromaticQualityInternal)
    {
        void ValidCreation()
        {
            new IntervalInternal(intervalBaseNumber, intervalChromaticQualityInternal);
        }

        Assert.DoesNotThrow(ValidCreation);
    }

    [Theory]
    [TestCase(9, IntervalQualityInternal.Minor)]
    [TestCase(9, IntervalQualityInternal.Major)]
    [TestCase(9, IntervalQualityInternal.Augmented)]
    [TestCase(9, IntervalQualityInternal.Diminished)]
    [TestCase(10, IntervalQualityInternal.Minor)]
    [TestCase(10, IntervalQualityInternal.Major)]
    [TestCase(10, IntervalQualityInternal.Augmented)]
    [TestCase(10, IntervalQualityInternal.Diminished)]
    [TestCase(11, IntervalQualityInternal.Perfect)]
    [TestCase(11, IntervalQualityInternal.Augmented)]
    [TestCase(11, IntervalQualityInternal.Diminished)]
    [TestCase(15, IntervalQualityInternal.Perfect)]
    [TestCase(15, IntervalQualityInternal.Augmented)]
    [TestCase(15, IntervalQualityInternal.Diminished)]
    [TestCase(16, IntervalQualityInternal.Minor)]
    [TestCase(16, IntervalQualityInternal.Major)]
    [TestCase(16, IntervalQualityInternal.Augmented)]
    [TestCase(16, IntervalQualityInternal.Diminished)]
    public void ValidCreationArgumentsCompoundOctave(int intervalBaseNumber,
        IntervalQualityInternal intervalChromaticQualityInternal)
    {
        void ValidCreation()
        {
            new IntervalInternal(intervalBaseNumber, intervalChromaticQualityInternal);
        }

        Assert.DoesNotThrow(ValidCreation);
    }

    [Theory]
    [TestCase(0, IntervalQualityInternal.Perfect)]
    [TestCase(0, IntervalQualityInternal.Minor)]
    [TestCase(0, IntervalQualityInternal.Major)]
    [TestCase(0, IntervalQualityInternal.Augmented)]
    [TestCase(0, IntervalQualityInternal.Diminished)]
    [TestCase(1, IntervalQualityInternal.Diminished)]
    [TestCase(1, IntervalQualityInternal.Major)]
    [TestCase(1, IntervalQualityInternal.Minor)]
    [TestCase(2, IntervalQualityInternal.Perfect)]
    [TestCase(3, IntervalQualityInternal.Perfect)]
    [TestCase(4, IntervalQualityInternal.Major)]
    [TestCase(4, IntervalQualityInternal.Minor)]
    public void InvalidCreationArgumentsOneOctave(int intervalBaseNumber,
        IntervalQualityInternal intervalChromaticQualityInternal)
    {
        void InvalidCreation()
        {
            new IntervalInternal(intervalBaseNumber, intervalChromaticQualityInternal);
        }

        Assert.Throws(Is.InstanceOf<Exception>(), InvalidCreation);
    }
}