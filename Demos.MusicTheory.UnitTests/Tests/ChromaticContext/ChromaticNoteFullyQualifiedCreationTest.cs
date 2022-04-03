using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Commons;
using NUnit.Framework;
using System;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteFullyQualifiedCreationTest : TestBase
{
    [Theory]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 2)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.Sharp, 3)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.DoubleSharp, 4)]
    [TestCase(ChromaticNoteQuality.D, 0, NotationSymbols.Flat, 3)]
    [TestCase(ChromaticNoteQuality.D, 0, NotationSymbols.None, 4)]
    [TestCase(ChromaticNoteQuality.D, 0, NotationSymbols.Sharp, 5)]
    [TestCase(ChromaticNoteQuality.B, 0, NotationSymbols.None, 13)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.Flat, 13)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, 14)]
    [TestCase(ChromaticNoteQuality.E, 1, NotationSymbols.None, 18)]
    [TestCase(ChromaticNoteQuality.D, 5, NotationSymbols.Sharp, 65)]
    public void TestChromaticNoteNormalNoteCreation(
        ChromaticNoteQuality elementaryChromaticNote,
        int octaveOrder,
        NotationSymbols modifier,
        int chromaticIndex)
    {
        // When
        var chromaticNote = new ChromaticNoteFullyQualified(elementaryChromaticNote, octaveOrder, modifier);

        // Then
        Assert.IsNotNull(chromaticNote);
        Assert.AreEqual(elementaryChromaticNote, chromaticNote.Quality);
        Assert.AreEqual(chromaticIndex, chromaticNote.ChromaticContextIndex);
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
        void InvalidConstruction() => new ChromaticNoteFullyQualified(elementaryChromaticNote, octaveOrder, modifier);

        // Then
        Assert.Throws<ArgumentException>(InvalidConstruction);
    }
}