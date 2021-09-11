using NUnit.Framework;
using System;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext
{
    [TestFixture]
    public class ChromaticNoteCreation
    {
        [Theory]
        [TestCase(ElementaryChromaticNotes.E, 1, NotationSymbols.None, 1)]
        [TestCase(ElementaryChromaticNotes.D, 5, NotationSymbols.Sharp, 20)]
        public void TestChromaticNoteNormalNoteCreation(
            ElementaryChromaticNotes elementaryChromaticNote,
            int octaveOrder,
            NotationSymbols modifier,
            int staffPosition)
        {
            // When
            ChromaticNote chromaticNote = new ChromaticNote(elementaryChromaticNote, octaveOrder, modifier, staffPosition);

            // Then
            Assert.IsNotNull(chromaticNote);
            Assert.AreEqual(elementaryChromaticNote, chromaticNote.ElementaryNote);
            Assert.AreEqual(octaveOrder, chromaticNote.OctaveOrder);
            Assert.AreEqual(modifier, chromaticNote.NoteModifierSymbol);
            Assert.AreEqual(staffPosition, chromaticNote.StaffPositionIndex);
        }

        [TestCase(ElementaryChromaticNotes.E, -1, NotationSymbols.None, 1)] // invalid octave order
        [TestCase(ElementaryChromaticNotes.E, 1, NotationSymbols.WholeNote, 1)] // invalid notation symbol
        [TestCase(ElementaryChromaticNotes.E, 1, NotationSymbols.None, -1)] // invalid staff position number
        public void TestChromaticNoteInvalidNoteCreation(
            ElementaryChromaticNotes elementaryChromaticNote,
            int octaveOrder,
            NotationSymbols modifier,
            int staffPosition)
        {
            // When
            TestDelegate invalidConstruction = () => new ChromaticNote(elementaryChromaticNote, octaveOrder, modifier, staffPosition);

            // Then
            Assert.Throws<ArgumentException>(invalidConstruction);
        }
    }
}
