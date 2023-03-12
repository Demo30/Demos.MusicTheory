using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.PublicIntegration;

internal class ServiceTests : TestBase
{
    public static IEnumerable<TestCaseData> GetElementaryNotesFromScaleTestCases()
    {
        yield return new TestCaseData(
            new Scale(NoteQuality.C, NoteModifier.Natural, ScaleQuality.Major),
            new List<NoteQuality>
            {
                NoteQuality.C,
                NoteQuality.D,
                NoteQuality.E,
                NoteQuality.F,
                NoteQuality.G,
                NoteQuality.A,
                NoteQuality.B
            });
    }

    [Test]
    [TestCaseSource(nameof(GetElementaryNotesFromScaleTestCases))]
    public void GetElementaryNotesFromScale(Scale scale, IList<NoteQuality> expectedElementaryNotes)
    {
        // Given
        var service = new MusicTheoryService();

        // When
        var results = service.GetElementaryNotesByScale(scale);
        
        // Then
        results.Should().BeEquivalentTo(expectedElementaryNotes);
    }
    
    
    public static IEnumerable<TestCaseData> GetEnharmonicNotesFromMidiIndexTestCases()
    {
        yield return new TestCaseData(
            21,
            new Note(NoteQuality.A, NoteModifier.Natural, 0));
        
        yield return new TestCaseData(
            60,
            new Note(NoteQuality.C, NoteModifier.Natural, 4));
    }

    [Test]
    [TestCaseSource(nameof(GetEnharmonicNotesFromMidiIndexTestCases))]
    public void GetEnharmonicNotesFromMidiIndex(int midiIndex, Note expectedNote)
    {
        // Given
        var service = new MusicTheoryService();

        // When
        var results = service.GetEnharmonicNotesFromMidiIndex(midiIndex);
        
        // Then
        results.Should().ContainEquivalentOf(expectedNote);
    }
}