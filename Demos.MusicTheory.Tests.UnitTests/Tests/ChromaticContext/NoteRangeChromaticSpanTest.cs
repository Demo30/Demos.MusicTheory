using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class NoteRangeChromaticSpanTest : TestBase
{

    public static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(new NoteRangeInternal(
             new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
    new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None)
             ), 0);
        
        yield return new TestCaseData(new NoteRangeInternal(
            new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
            new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.Sharp)
        ), 1);
        
        yield return new TestCaseData(new NoteRangeInternal(
            new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
            new NoteInternal(NoteQualityInternal.D, 4, NotationSymbols.None)
        ), 2);
        
        yield return new TestCaseData(new NoteRangeInternal(
            new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
            new NoteInternal(NoteQualityInternal.C, 5, NotationSymbols.None)
        ), 12);
        
        yield return new TestCaseData(new NoteRangeInternal(
            new NoteInternal(NoteQualityInternal.B, 4, NotationSymbols.None),
            new NoteInternal(NoteQualityInternal.C, 5, NotationSymbols.None)
        ), 1);
    } 
    
    
    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void Test(NoteRangeInternal range, int expectedChromaticSpan)
    {
        // Then
        range.ChromaticIndexSpan.Should().Be(expectedChromaticSpan);
    }
}