using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.PublicIntegration;

[TestFixture]
internal class NotesTests : TestBase
{
    #region Note distance in interval
    public static IEnumerable<TestCaseData> NotesDistanceInIntervalTestCases()
    {
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.B, NoteModifier.Natural, 3), Interval.MinorSecond);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.B, NoteModifier.Sharp, 3), Interval.DiminishedSecond);
        yield return new TestCaseData(new Note(NoteQuality.B, NoteModifier.Sharp, 3), new Note(NoteQuality.C, NoteModifier.Natural, 4), Interval.DiminishedSecond);
        yield return new TestCaseData(new Note(NoteQuality.B, NoteModifier.DoubleSharp, 3), new Note(NoteQuality.C, NoteModifier.Sharp, 4), Interval.DiminishedSecond);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Sharp, 4), new Note(NoteQuality.D, NoteModifier.Flat, 4), Interval.DiminishedSecond);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.D, NoteModifier.DoubleFlat, 4), Interval.DiminishedSecond);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.C, NoteModifier.Natural, 4), Interval.PerfectUnison);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.C, NoteModifier.Sharp, 4), Interval.AugmentedUnison);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.D, NoteModifier.Natural, 4), Interval.MajorSecond);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.E, NoteModifier.Natural, 4), Interval.MajorThird);
    }
    
    [Test]
    [TestCaseSource(nameof(NotesDistanceInIntervalTestCases))]
    public void NotesDistanceInIntervalTest(Note startNote, Note endNote, Interval expectedInterval)
    {
        // When
        var interval = startNote.GetIntervalFromOtherNote(endNote);

        // Then
        interval.Should().BeEquivalentTo(expectedInterval);
    }
    
    #endregion
    
    #region Note distance in semitones
    public static IEnumerable<TestCaseData> NotesDistanceInSemitonesTestCases()
    {
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.B, NoteModifier.Natural, 3), 1);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.B, NoteModifier.Sharp, 3), 0);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.C, NoteModifier.Natural, 4), 0);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.C, NoteModifier.Sharp, 4), 1);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.D, NoteModifier.Natural, 4), 2);
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.E, NoteModifier.Natural, 4), 4);
    }
    
    [Test]
    [TestCaseSource(nameof(NotesDistanceInSemitonesTestCases))]
    public void NotesDistanceInSemitonesTest(Note startNote, Note endNote, int expectedDistance)
    {
        // When
        var distance = startNote.GetSemitoneDistanceFromOtherNote(endNote);

        // Then
        distance.Should().Be(expectedDistance);
    }
    
    #endregion
    
    #region Note enharmonics
    public static IEnumerable<TestCaseData> NotesEnharmonicsTestCases()
    {
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.B, NoteModifier.Sharp, 3));
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Natural, 4), new Note(NoteQuality.D, NoteModifier.DoubleFlat, 4));
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Sharp, 4), new Note(NoteQuality.D, NoteModifier.Flat, 4));
        yield return new TestCaseData(new Note(NoteQuality.C, NoteModifier.Sharp, 4), new Note(NoteQuality.B, NoteModifier.DoubleSharp, 3));
        yield return new TestCaseData(new Note(NoteQuality.D, NoteModifier.Natural, 4), new Note(NoteQuality.C, NoteModifier.DoubleSharp, 4));
        yield return new TestCaseData(new Note(NoteQuality.D, NoteModifier.Natural, 4), new Note(NoteQuality.E, NoteModifier.DoubleFlat, 4));
    }
    
    [Test]
    [TestCaseSource(nameof(NotesEnharmonicsTestCases))]
    public void NoteEnharmonicsTest(Note startNote, Note expectedEnharmonicNote)
    {
        // When
        var enharmonicNotes = startNote.GetEnharmonics();

        // Then
        enharmonicNotes.Should().ContainEquivalentOf(expectedEnharmonicNote);
    }
    
    #endregion
    
    #region Notes by interval

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

    #region Notes by chromatic distance
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
        var enharmonicNotes = startNote.GetEnharmonicNotesBySemitoneDistance(chromaticDistance);

        // Then
        enharmonicNotes.Should().ContainEquivalentOf(endNote);
    }
    
    #endregion

}