using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

internal class IntervalProviderFromNoteRangeTest : TestBase
{
    [OneTimeSetUp]
    public void Setup()
    {
        ServicesManager.ServicesProvider.RegisterService(() => new IntervalProviderFromIndexSpan());        
    }

    public static IEnumerable<TestCaseData> GetTestCaseData()
    {
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.Sharp)),
            new IntervalInternal(1, IntervalQualityInternal.Augmented));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.D, 4, NotationSymbols.None)),
            new IntervalInternal(2, IntervalQualityInternal.Major));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.D, 4, NotationSymbols.Sharp)),
            new IntervalInternal(2, IntervalQualityInternal.Augmented));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.B, 3, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None)),
            new IntervalInternal(2, IntervalQualityInternal.Minor));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.B, 4, NotationSymbols.None)),
            new IntervalInternal(7, IntervalQualityInternal.Major));
    }
    
    [Theory]
    [TestCaseSource(nameof(GetTestCaseData))]
    public void ValidResults(NoteRangeInternal range, IntervalInternal expectedInterval)
    {
        // Given
        var provider = new IntervalProviderFromNoteRange();

        // When
        var result = provider.GetInterval(range);

        // Then
        result.Should().BeEquivalentTo(expectedInterval);
    }
}