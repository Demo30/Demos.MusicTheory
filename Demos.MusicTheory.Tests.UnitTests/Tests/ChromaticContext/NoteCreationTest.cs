using System;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Commons;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class NoteCreationTest : TestBase
{
    [Theory]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 2)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.Sharp, 3)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.DoubleSharp, 4)]
    [TestCase(NoteQuality.D, 0, NotationSymbols.Flat, 3)]
    [TestCase(NoteQuality.D, 0, NotationSymbols.None, 4)]
    [TestCase(NoteQuality.D, 0, NotationSymbols.Sharp, 5)]
    [TestCase(NoteQuality.B, 0, NotationSymbols.None, 13)]
    [TestCase(NoteQuality.C, 1, NotationSymbols.Flat, 13)]
    [TestCase(NoteQuality.C, 1, NotationSymbols.None, 14)]
    [TestCase(NoteQuality.E, 1, NotationSymbols.None, 18)]
    [TestCase(NoteQuality.D, 5, NotationSymbols.Sharp, 65)]
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