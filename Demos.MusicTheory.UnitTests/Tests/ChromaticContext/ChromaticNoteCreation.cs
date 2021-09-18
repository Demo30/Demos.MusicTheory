using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.Commons;
using NUnit.Framework;
using System;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext
{
    [TestFixture]
    public class ChromaticNoteCreation
    {
        [Theory]
        [TestCase(ChromaticNoteQuality.E, 1, NotationSymbols.None)]
        [TestCase(ChromaticNoteQuality.D, 5, NotationSymbols.Sharp)]
        public void TestChromaticNoteNormalNoteCreation(
            ChromaticNoteQuality elementaryChromaticNote,
            int octaveOrder,
            NotationSymbols modifier)
        {
            // When
            ChromaticNoteFullyQualified chromaticNote = new ChromaticNoteFullyQualified(elementaryChromaticNote, octaveOrder, modifier);

            // Then
            Assert.IsNotNull(chromaticNote);
            Assert.AreEqual(elementaryChromaticNote, chromaticNote.Quality);
            Assert.AreEqual(octaveOrder, chromaticNote.Order);
            Assert.AreEqual(modifier, chromaticNote.Modifier);
        }

        [TestCase(ChromaticNoteQuality.E, -1, NotationSymbols.None)] // invalid octave order
        [TestCase(ChromaticNoteQuality.E, 1, NotationSymbols.WholeNote)] // invalid notation symbol
        public void TestChromaticNoteInvalidNoteCreation(
            ChromaticNoteQuality elementaryChromaticNote,
            int octaveOrder,
            NotationSymbols modifier)
        {
            // When
            TestDelegate invalidConstruction = () => new ChromaticNoteFullyQualified(elementaryChromaticNote, octaveOrder, modifier);

            // Then
            Assert.Throws<ArgumentException>(invalidConstruction);
        }
    }
}
