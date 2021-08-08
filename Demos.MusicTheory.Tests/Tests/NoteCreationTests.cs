using NUnit.Framework;
using System;

namespace Demos.MusicTheory.Tests
{
    [TestFixture]
    public class NoteCreationTests
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
            ChromaticNote chromaticNote = new ChromaticNote(elementaryChromaticNote, octaveOrder, modifier, staffPosition);

            Assert.IsNotNull(chromaticNote);
            Assert.AreEqual(elementaryChromaticNote, chromaticNote.ElementaryNote);
            Assert.AreEqual(octaveOrder, chromaticNote.OctaveOrder);
            Assert.AreEqual(modifier, chromaticNote.NoteModifierSymbol);
            Assert.AreEqual(staffPosition, chromaticNote.StaffPositionIndex);
        }

        [Theory]
        [TestCase(ElementaryChromaticNotes.E, -1, NotationSymbols.None, 1)]
        [TestCase(ElementaryChromaticNotes.E, 1, NotationSymbols.WholeNote, 1)]
        [TestCase(ElementaryChromaticNotes.E, 1, NotationSymbols.None, -1)]
        public void TestChromaticNoteInvalidNoteCreation(
            ElementaryChromaticNotes elementaryChromaticNote,
            int octaveOrder,
            NotationSymbols modifier,
            int staffPosition)
        {
            TestDelegate invalidConstruction = () => new ChromaticNote(elementaryChromaticNote, octaveOrder, modifier, staffPosition);
            Assert.Throws<ArgumentException>(invalidConstruction);
        }
    }
}
