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
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 0)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.Sharp, 1)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.DoubleSharp, 2)]
    [TestCase(NoteQualityInternal.D, 0, NotationSymbols.Flat, 1)]
    [TestCase(NoteQualityInternal.D, 0, NotationSymbols.None, 2)]
    [TestCase(NoteQualityInternal.D, 0, NotationSymbols.Sharp, 3)]
    [TestCase(NoteQualityInternal.B, 0, NotationSymbols.None, 11)]
    [TestCase(NoteQualityInternal.C, 1, NotationSymbols.Flat, 11)]
    [TestCase(NoteQualityInternal.C, 1, NotationSymbols.None, 12)]
    [TestCase(NoteQualityInternal.E, 1, NotationSymbols.None, 16)]
    [TestCase(NoteQualityInternal.D, 5, NotationSymbols.Sharp, 63)]
    public void TestChromaticNoteNormalNoteCreation(
        NoteQualityInternal elementaryNote,
        int octaveOrder,
        NotationSymbols modifier,
        int chromaticIndex)
    {
        // When
        var chromaticNote = new NoteInternal(elementaryNote, octaveOrder, modifier);

        // Then
        Assert.IsNotNull(chromaticNote);
        Assert.AreEqual(elementaryNote, chromaticNote.QualityInternal);
        Assert.AreEqual(chromaticIndex, chromaticNote.ChromaticContextIndex);
        Assert.AreEqual(modifier, chromaticNote.Modifier);
    }

    [TestCase(NoteQualityInternal.E, -1, NotationSymbols.None)] // invalid octave order
    [TestCase(NoteQualityInternal.E, 1, NotationSymbols.WholeNote)] // invalid notation symbol
    public void TestChromaticNoteInvalidNoteCreation(
        NoteQualityInternal elementaryNote,
        int octaveOrder,
        NotationSymbols modifier)
    {
        // When
        void InvalidConstruction()
        {
            new NoteInternal(elementaryNote, octaveOrder, modifier);
        }

        // Then
        Assert.Throws<ArgumentException>(InvalidConstruction);
    }
}