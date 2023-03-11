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

    // TODO: I am currently unable to support interval such as AAAA3: https://music.stackexchange.com/questions/42513/what-is-the-interval-from-c-double-flat-to-e-double-sharp-called
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
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None)),
            new IntervalInternal(1, IntervalQualityInternal.Perfect));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.D, 4, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.D, 4, NotationSymbols.None)),
            new IntervalInternal(1, IntervalQualityInternal.Perfect));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.D, 3, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.D, 4, NotationSymbols.None)),
            new IntervalInternal(8, IntervalQualityInternal.Perfect));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.C, 3, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.D, 4, NotationSymbols.None)),
            new IntervalInternal(9, IntervalQualityInternal.Major));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.D, 4, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None)),
            new IntervalInternal(2, IntervalQualityInternal.Major));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.Sharp),
                new NoteInternal(NoteQualityInternal.D, 4, NotationSymbols.Sharp)),
            new IntervalInternal(2, IntervalQualityInternal.Major));

        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.A, 3, NotationSymbols.Sharp),
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None)),
            new IntervalInternal(3, IntervalQualityInternal.Diminished));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.A, 3, NotationSymbols.Sharp)),
            new IntervalInternal(3, IntervalQualityInternal.Diminished));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.A, 3, NotationSymbols.None),
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.Flat)),
            new IntervalInternal(3, IntervalQualityInternal.Diminished));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.Flat),
                new NoteInternal(NoteQualityInternal.A, 3, NotationSymbols.None)),
            new IntervalInternal(3, IntervalQualityInternal.Diminished));
        
        yield return new TestCaseData(
            new NoteRangeInternal(
                new NoteInternal(NoteQualityInternal.A, 3, NotationSymbols.DoubleSharp),
                new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.Flat)),
            new IntervalInternal(3, IntervalQualityInternal.Diminished));
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