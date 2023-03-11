using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.PublicIntegration;

internal class ServiceTests : TestBase
{
    public static IEnumerable<TestCaseData> GetTestCases()
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
    [TestCaseSource(nameof(GetTestCases))]
    public void GetElementaryNotesFromScale(Scale scale, IList<NoteQuality> expectedElementaryNotes)
    {
        // Given
        var service = new MusicTheoryService();

        // When
        var results = service.GetElementaryNotesByScale(scale);
        
        // Then
        results.Should().BeEquivalentTo(expectedElementaryNotes);
    }
}