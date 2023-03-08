using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.PublicIntegration;

[TestFixture]
internal class NotesTests : TestBase
{
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
}