using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.PublicIntegration;

[TestFixture]
internal class NotesTests : TestBase
{
    #region NotesByIntervalTest

    public static IEnumerable<TestCaseData> NotesByIntervalTestCases()
    {
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), Interval.PerfectUnison, new Note(NoteQuality.C, NoteModifier.Natural, 4));
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), Interval.MinorSecond, new Note(NoteQuality.C, NoteModifier.Sharp, 4));
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), Interval.MajorSecond, new Note(NoteQuality.D, NoteModifier.Natural, 4));
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), Interval.MajorThird, new Note(NoteQuality.E, NoteModifier.Natural, 4));
    }
    
    [Test]
    [TestCaseSource(nameof(NotesByIntervalTestCases))]
    public void NotesByIntervalTest(Note startNote, Interval interval, Note endNote)
    {
        // When
        var enharmonicNotes = startNote.GetEnharmonicNotesByInterval(interval);

        // Then
        enharmonicNotes.Should().ContainEquivalentOf(endNote);
    }

    #endregion

    #region NotesByChromaticDistance
    public static IEnumerable<TestCaseData> NotesByChromaticDistanceTestCases()
    {
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), 0, new Note(NoteQuality.C, NoteModifier.Natural, 4));
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), 1, new Note(NoteQuality.C, NoteModifier.Sharp, 4));
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), 2, new Note(NoteQuality.D, NoteModifier.Natural, 4));
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), 4, new Note(NoteQuality.E, NoteModifier.Natural, 4));
    }
    
    [Test]
    [TestCaseSource(nameof(NotesByChromaticDistanceTestCases))]
    public void NotesByChromaticDistanceTest(Note startNote, int chromaticDistance, Note endNote)
    {
        // When
        var enharmonicNotes = startNote.GetEnharmonicNotesByChromaticDistance(chromaticDistance);

        // Then
        enharmonicNotes.Should().ContainEquivalentOf(endNote);
    }
    
    #endregion

}