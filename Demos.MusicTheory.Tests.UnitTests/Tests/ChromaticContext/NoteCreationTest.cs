using System;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Commons;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class NoteCreationTest : TestBase
{
    [Theory]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 0)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.Sharp, 1)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.DoubleSharp, 2)]
    [TestCase(NoteQuality.D, 0, NotationSymbols.Flat, 1)]
    [TestCase(NoteQuality.D, 0, NotationSymbols.None, 2)]
    [TestCase(NoteQuality.D, 0, NotationSymbols.Sharp, 3)]
    [TestCase(NoteQuality.B, 0, NotationSymbols.None, 11)]
    [TestCase(NoteQuality.C, 1, NotationSymbols.Flat, 11)]
    [TestCase(NoteQuality.C, 1, NotationSymbols.None, 12)]
    [TestCase(NoteQuality.E, 1, NotationSymbols.None, 16)]
    [TestCase(NoteQuality.D, 5, NotationSymbols.Sharp, 63)]
    public void TestChromaticNoteNormalNoteCreation(
        NoteQuality elementaryNote,
        int octaveOrder,
        NotationSymbols modifier,
        int chromaticIndex)
    {
        // When
        var chromaticNote = new Note(elementaryNote, octaveOrder, modifier);

        // Then
        Assert.IsNotNull(chromaticNote);
        Assert.AreEqual(elementaryNote, chromaticNote.Quality);
        Assert.AreEqual(chromaticIndex, chromaticNote.ChromaticContextIndex);
        Assert.AreEqual(modifier, chromaticNote.Modifier);
    }

    [TestCase(NoteQuality.E, -1, NotationSymbols.None)] // invalid octave order
    [TestCase(NoteQuality.E, 1, NotationSymbols.WholeNote)] // invalid notation symbol
    public void TestChromaticNoteInvalidNoteCreation(
        NoteQuality elementaryNote,
        int octaveOrder,
        NotationSymbols modifier)
    {
        // When
        void InvalidConstruction()
        {
            new Note(elementaryNote, octaveOrder, modifier);
        }

        // Then
        Assert.Throws<ArgumentException>(InvalidConstruction);
    }
}