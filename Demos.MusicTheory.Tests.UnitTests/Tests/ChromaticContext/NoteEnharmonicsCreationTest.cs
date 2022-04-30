using System;
using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class NoteEnharmonicsCreationTest : TestBase
{
    private static Note[] GetValidTestCases(int testCaseIndex)
    {
        List<Note[]> validEnharmonicClusters = new()
        {
            new[]
            {
                new Note(NoteQuality.D, 1, Commons.NotationSymbols.None),
                new Note(NoteQuality.C, 1, Commons.NotationSymbols.DoubleSharp),
                new Note(NoteQuality.E, 1, Commons.NotationSymbols.DoubleFlat)
            },
            new[]
            {
                new Note(NoteQuality.C, 1, Commons.NotationSymbols.Sharp),
                new Note(NoteQuality.D, 1, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new Note(NoteQuality.E, 1, Commons.NotationSymbols.None),
                new Note(NoteQuality.F, 1, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new Note(NoteQuality.E, 1, Commons.NotationSymbols.Sharp),
                new Note(NoteQuality.F, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new Note(NoteQuality.F, 1, Commons.NotationSymbols.None),
                new Note(NoteQuality.F, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new Note(NoteQuality.F, 1, Commons.NotationSymbols.None),
                new Note(NoteQuality.G, 1, Commons.NotationSymbols.DoubleFlat)
            },
            new[]
            {
                new Note(NoteQuality.F, 1, Commons.NotationSymbols.Sharp),
                new Note(NoteQuality.G, 1, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new Note(NoteQuality.F, 1, Commons.NotationSymbols.DoubleSharp),
                new Note(NoteQuality.G, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new Note(NoteQuality.B, 1, Commons.NotationSymbols.Sharp),
                new Note(NoteQuality.C, 2, Commons.NotationSymbols.None)
            },
            new[]
            {
                new Note(NoteQuality.B, 1, Commons.NotationSymbols.DoubleSharp),
                new Note(NoteQuality.C, 2, Commons.NotationSymbols.Sharp)
            },
            new[]
            {
                new Note(NoteQuality.B, 1, Commons.NotationSymbols.DoubleSharp),
                new Note(NoteQuality.D, 2, Commons.NotationSymbols.Flat)
            }
        };
        return validEnharmonicClusters[testCaseIndex];
    }

    private static Note[] GetInvalidTestCases(int testCaseIndex)
    {
        List<Note[]> validEnharmonicClusters = new()
        {
            new[]
            {
                new Note(NoteQuality.D, 1, Commons.NotationSymbols.None),
                new Note(NoteQuality.C, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new Note(NoteQuality.C, 1, Commons.NotationSymbols.Sharp),
                new Note(NoteQuality.D, 2, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new Note(NoteQuality.E, 1, Commons.NotationSymbols.Flat),
                new Note(NoteQuality.E, 1, Commons.NotationSymbols.Sharp)
            }
        };
        return validEnharmonicClusters[testCaseIndex];
    }

    [Theory]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    [TestCase(10)]
    public void ValidCreation(int validCombinationTestCaseNumber)
    {
        void ValidCall()
        {
            new NoteEnharmonics(GetValidTestCases(validCombinationTestCaseNumber));
        }

        Assert.DoesNotThrow(ValidCall);
    }

    [Theory]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void InvalidCreation(int invalidCombinationTestCaseNumber)
    {
        void InvalidCall()
        {
            new NoteEnharmonics(GetInvalidTestCases(invalidCombinationTestCaseNumber));
        }

        Assert.Throws<ArgumentException>(InvalidCall);
    }
}