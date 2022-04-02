using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using NUnit.Framework;
using System;
using Demos.MusicTheory.ChromaticContext;

namespace Demos.MusicTheory.UnitTests.Tests
{
    [TestFixture]
    public class IntervalChromaticFullyQualifiedCreation
    {
        [Theory]
        [TestCase(1, ChromaticNoteIntervalQuality.Perfect)]
        [TestCase(1, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(2, ChromaticNoteIntervalQuality.Minor)]
        [TestCase(2, ChromaticNoteIntervalQuality.Major)]
        [TestCase(2, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(2, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(3, ChromaticNoteIntervalQuality.Minor)]
        [TestCase(3, ChromaticNoteIntervalQuality.Major)]
        [TestCase(3, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(3, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(4, ChromaticNoteIntervalQuality.Perfect)]
        [TestCase(4, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(4, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(5, ChromaticNoteIntervalQuality.Perfect)]
        [TestCase(5, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(5, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(6, ChromaticNoteIntervalQuality.Minor)]
        [TestCase(6, ChromaticNoteIntervalQuality.Major)]
        [TestCase(6, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(6, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(7, ChromaticNoteIntervalQuality.Minor)]
        [TestCase(7, ChromaticNoteIntervalQuality.Major)]
        [TestCase(7, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(7, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(8, ChromaticNoteIntervalQuality.Perfect)]
        [TestCase(8, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(8, ChromaticNoteIntervalQuality.Diminished)]
        public void ValidCreationArgumentsOneOctave(int intervalBaseNumber, ChromaticNoteIntervalQuality intervalChromaticQuality)
        {
            void ValidCreation() => new ChromaticNoteIntervalFullyQualified(intervalBaseNumber, intervalChromaticQuality);
            Assert.DoesNotThrow(ValidCreation);
        }

        [Theory]
        [TestCase(9, ChromaticNoteIntervalQuality.Minor)]
        [TestCase(9, ChromaticNoteIntervalQuality.Major)]
        [TestCase(9, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(9, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(10, ChromaticNoteIntervalQuality.Minor)]
        [TestCase(10, ChromaticNoteIntervalQuality.Major)]
        [TestCase(10, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(10, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(11, ChromaticNoteIntervalQuality.Perfect)]
        [TestCase(11, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(11, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(15, ChromaticNoteIntervalQuality.Perfect)]
        [TestCase(15, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(15, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(16, ChromaticNoteIntervalQuality.Minor)]
        [TestCase(16, ChromaticNoteIntervalQuality.Major)]
        [TestCase(16, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(16, ChromaticNoteIntervalQuality.Diminished)]
        public void ValidCreationArgumentsCompoundOctave(int intervalBaseNumber, ChromaticNoteIntervalQuality intervalChromaticQuality)
        {
            void ValidCreation() => new ChromaticNoteIntervalFullyQualified(intervalBaseNumber, intervalChromaticQuality);
            Assert.DoesNotThrow(ValidCreation);
        }

        [Theory]
        [TestCase(0, ChromaticNoteIntervalQuality.Perfect)]
        [TestCase(0, ChromaticNoteIntervalQuality.Minor)]
        [TestCase(0, ChromaticNoteIntervalQuality.Major)]
        [TestCase(0, ChromaticNoteIntervalQuality.Augmented)]
        [TestCase(0, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(1, ChromaticNoteIntervalQuality.Diminished)]
        [TestCase(1, ChromaticNoteIntervalQuality.Major)]
        [TestCase(1, ChromaticNoteIntervalQuality.Minor)]
        [TestCase(2, ChromaticNoteIntervalQuality.Perfect)]
        [TestCase(3, ChromaticNoteIntervalQuality.Perfect)]
        [TestCase(4, ChromaticNoteIntervalQuality.Major)]
        [TestCase(4, ChromaticNoteIntervalQuality.Minor)]
        public void InvalidCreationArgumentsOneOctave(int intervalBaseNumber, ChromaticNoteIntervalQuality intervalChromaticQuality)
        {
            void InvalidCreation() => new ChromaticNoteIntervalFullyQualified(intervalBaseNumber, intervalChromaticQuality);
            Assert.Throws(Is.InstanceOf<Exception>(), InvalidCreation);
        }

    }
}
