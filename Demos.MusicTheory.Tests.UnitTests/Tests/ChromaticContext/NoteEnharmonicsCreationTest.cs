using System;
using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class NoteEnharmonicsCreationTest : TestBase
{
    private static NoteInternal[] GetValidTestCases(int testCaseIndex)
    {
        List<NoteInternal[]> validEnharmonicClusters = new()
        {
            new[]
            {
                new NoteInternal(NoteQualityInternal.D, 1, Commons.NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.C, 1, Commons.NotationSymbols.DoubleSharp),
                new NoteInternal(NoteQualityInternal.E, 1, Commons.NotationSymbols.DoubleFlat)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.C, 1, Commons.NotationSymbols.Sharp),
                new NoteInternal(NoteQualityInternal.D, 1, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.E, 1, Commons.NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.F, 1, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.E, 1, Commons.NotationSymbols.Sharp),
                new NoteInternal(NoteQualityInternal.F, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.F, 1, Commons.NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.F, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.F, 1, Commons.NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.G, 1, Commons.NotationSymbols.DoubleFlat)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.F, 1, Commons.NotationSymbols.Sharp),
                new NoteInternal(NoteQualityInternal.G, 1, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.F, 1, Commons.NotationSymbols.DoubleSharp),
                new NoteInternal(NoteQualityInternal.G, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.B, 1, Commons.NotationSymbols.Sharp),
                new NoteInternal(NoteQualityInternal.C, 2, Commons.NotationSymbols.None)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.B, 1, Commons.NotationSymbols.DoubleSharp),
                new NoteInternal(NoteQualityInternal.C, 2, Commons.NotationSymbols.Sharp)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.B, 1, Commons.NotationSymbols.DoubleSharp),
                new NoteInternal(NoteQualityInternal.D, 2, Commons.NotationSymbols.Flat)
            }
        };
        return validEnharmonicClusters[testCaseIndex];
    }

    private static NoteInternal[] GetInvalidTestCases(int testCaseIndex)
    {
        List<NoteInternal[]> validEnharmonicClusters = new()
        {
            new[]
            {
                new NoteInternal(NoteQualityInternal.D, 1, Commons.NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.C, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.C, 1, Commons.NotationSymbols.Sharp),
                new NoteInternal(NoteQualityInternal.D, 2, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new NoteInternal(NoteQualityInternal.E, 1, Commons.NotationSymbols.Flat),
                new NoteInternal(NoteQualityInternal.E, 1, Commons.NotationSymbols.Sharp)
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
            new NoteEnharmonicsInternal(GetValidTestCases(validCombinationTestCaseNumber));
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
            new NoteEnharmonicsInternal(GetInvalidTestCases(invalidCombinationTestCaseNumber));
        }

        Assert.Throws<ArgumentException>(InvalidCall);
    }
}