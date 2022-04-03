using System;
using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteEnharmonicClusterCreationTest : TestBase
{
    private static ChromaticNoteFullyQualified[] GetValidTestCases(int testCaseIndex)
    {
        List<ChromaticNoteFullyQualified[]> validEnharmonicClusters = new()
        {
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 1, Commons.NotationSymbols.None),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.C, 1, Commons.NotationSymbols.DoubleSharp),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.E, 1, Commons.NotationSymbols.DoubleFlat)
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.C, 1, Commons.NotationSymbols.Sharp),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 1, Commons.NotationSymbols.Flat),
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.E, 1, Commons.NotationSymbols.None),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.F, 1, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.E, 1, Commons.NotationSymbols.Sharp),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.F, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.F, 1, Commons.NotationSymbols.None),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.F, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.F, 1, Commons.NotationSymbols.None),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.G, 1, Commons.NotationSymbols.DoubleFlat)
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.F, 1, Commons.NotationSymbols.Sharp),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.G, 1, Commons.NotationSymbols.Flat)
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.F, 1, Commons.NotationSymbols.DoubleSharp),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.G, 1, Commons.NotationSymbols.None)
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.B, 1, Commons.NotationSymbols.Sharp),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.C, 2, Commons.NotationSymbols.None)
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.B, 1, Commons.NotationSymbols.DoubleSharp),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.C, 2, Commons.NotationSymbols.Sharp)
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.B, 1, Commons.NotationSymbols.DoubleSharp),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 2, Commons.NotationSymbols.Flat)
            },
        };
        return validEnharmonicClusters[testCaseIndex];
    }

    private static ChromaticNoteFullyQualified[] GetInvalidTestCases(int testCaseIndex)
    {
        List<ChromaticNoteFullyQualified[]> validEnharmonicClusters = new()
        {
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 1, Commons.NotationSymbols.None),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.C, 1, Commons.NotationSymbols.None),
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.C, 1, Commons.NotationSymbols.Sharp),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 2, Commons.NotationSymbols.Flat),
            },
            new[]
            {
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.E, 1, Commons.NotationSymbols.Flat),
                new ChromaticNoteFullyQualified(ChromaticNoteQuality.E, 1, Commons.NotationSymbols.Sharp)
            },
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
        void ValidCall() => new ChromaticNoteEnharmonicCluster(GetValidTestCases(validCombinationTestCaseNumber));
        
        Assert.DoesNotThrow(ValidCall);
    }

    [Theory]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void InvalidCreation(int invalidCombinationTestCaseNumber)
    {
        void InvalidCall() => new ChromaticNoteEnharmonicCluster(GetInvalidTestCases(invalidCombinationTestCaseNumber));

        Assert.Throws<ArgumentException>(InvalidCall);
    }
}