using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.PublicIntegration;

[TestFixture]
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
    
    public static IEnumerable<TestCaseData> GetNotesByScaleTestCases()
    {
        yield return new TestCaseData(
            new Scale(NoteQuality.C, NoteModifier.Natural, ScaleQuality.Major),
            5u,
            new List<Note>
            {
                new(NoteQuality.C, NoteModifier.Natural, 5),
                new(NoteQuality.D, NoteModifier.Natural, 5),
                new(NoteQuality.E, NoteModifier.Natural, 5),
                new(NoteQuality.F, NoteModifier.Natural, 5),
                new(NoteQuality.G, NoteModifier.Natural, 5),
                new(NoteQuality.A, NoteModifier.Natural, 5),
                new(NoteQuality.B, NoteModifier.Natural, 5),
            });
        
        yield return new TestCaseData(
            new Scale(NoteQuality.C, NoteModifier.Sharp, ScaleQuality.Minor),
            5u,
            new List<Note>
            {
                new(NoteQuality.C, NoteModifier.Sharp, 5),
                new(NoteQuality.D, NoteModifier.Sharp, 5),
                new(NoteQuality.E, NoteModifier.Natural, 5),
                new(NoteQuality.F, NoteModifier.Sharp, 5),
                new(NoteQuality.G, NoteModifier.Sharp, 5),
                new(NoteQuality.A, NoteModifier.Natural, 5),
                new(NoteQuality.B, NoteModifier.Natural, 5),
            });
    }

    [Test]
    [TestCaseSource(nameof(GetNotesByScaleTestCases))]
    public void GetNotesByScaleTest(Scale scale, uint order, List<Note> expectedNotes)
    {
        // Given
        var service = new MusicTheoryService();

        // When
        var results = service.GetNotesByScale(scale, order);
        
        // Then
        results.Should().BeEquivalentTo(expectedNotes, options => options.WithStrictOrdering());
    }
    
    public static IEnumerable<TestCaseData> GetNoteByDiatonicStepsWithinScaleTestCases()
    {
        yield return new TestCaseData(
            new Scale(NoteQuality.C, NoteModifier.Natural, ScaleQuality.Major),
            new Note(NoteQuality.C, NoteModifier.Natural, 5),
            2,
            new Note(NoteQuality.E, NoteModifier.Natural, 5));
    }

    [Test]
    [TestCaseSource(nameof(GetNoteByDiatonicStepsWithinScaleTestCases))]
    public void GetNoteByDiatonicStepsWithinScaleTest(Scale scale, Note referenceNote, int diatonicSteps, Note expectedNote)
    {
        // Given
        var service = new MusicTheoryService();

        // When
        var result = service.GetNoteByDiatonicStepsFromNoteWithinScale(scale, referenceNote, diatonicSteps);
        
        // Then
        result.Should().BeEquivalentTo(expectedNote);
    }
    
    public static IEnumerable<TestCaseData> GetScalesByElementaryNotesTestCases()
    {
        yield return new TestCaseData(
            new List<(NoteQuality, NoteModifier)>
            {
                (NoteQuality.C, NoteModifier.Natural),
                (NoteQuality.D, NoteModifier.Natural),
                (NoteQuality.E, NoteModifier.Natural),
                (NoteQuality.F, NoteModifier.Natural),
                (NoteQuality.G, NoteModifier.Natural),
                (NoteQuality.A, NoteModifier.Natural),
                (NoteQuality.B, NoteModifier.Natural),
            },
            new List<Scale>
            {
                new(NoteQuality.C, NoteModifier.Natural, ScaleQuality.Major),
                new(NoteQuality.A, NoteModifier.Natural, ScaleQuality.Minor)
            });
        
        yield return new TestCaseData(
            new List<(NoteQuality, NoteModifier)>
            {
                (NoteQuality.C, NoteModifier.Natural),
                (NoteQuality.E, NoteModifier.Natural),
            },
            new List<Scale>
            {
                new(NoteQuality.C, NoteModifier.Natural, ScaleQuality.Major),
                new(NoteQuality.A, NoteModifier.Natural, ScaleQuality.Minor),
                new(NoteQuality.G, NoteModifier.Natural, ScaleQuality.Major),
                new(NoteQuality.F, NoteModifier.Natural, ScaleQuality.Major),
                new(NoteQuality.E, NoteModifier.Natural, ScaleQuality.Minor),
                new(NoteQuality.D, NoteModifier.Natural, ScaleQuality.Minor),
            });
    }
    
    [Test]
    [TestCaseSource(nameof(GetScalesByElementaryNotesTestCases))]
    public void GetScalesByElementaryNotesTest(List<(NoteQuality quality, NoteModifier modifier)> elementaryNotes, List<Scale> expectedScales)
    {
        // Given
        var service = new MusicTheoryService();
    
        // When
        var result = service.GetScalesByElementaryNotes(elementaryNotes);
        
        // Then
        result.Should().BeEquivalentTo(expectedScales);
    }
    
    public static IEnumerable<TestCaseData> GetScalesByNotesTestCases()
    {
        yield return new TestCaseData(
            new List<Note>
            {
                new (NoteQuality.C, NoteModifier.Natural, 4),
                new (NoteQuality.D, NoteModifier.Natural, 4),
                new (NoteQuality.E, NoteModifier.Natural, 4),
                new (NoteQuality.F, NoteModifier.Natural, 4),
                new (NoteQuality.G, NoteModifier.Natural, 4),
                new (NoteQuality.G, NoteModifier.Natural, 4),
                new (NoteQuality.B, NoteModifier.Natural, 4),
            },
            new List<Scale>
            {
                new(NoteQuality.C, NoteModifier.Natural, ScaleQuality.Major),
                new(NoteQuality.A, NoteModifier.Natural, ScaleQuality.Minor)
            });
        
        yield return new TestCaseData(
            new List<Note>
            {
                new (NoteQuality.C, NoteModifier.Natural, 4),
                new (NoteQuality.E, NoteModifier.Natural, 4),
            },
            new List<Scale>
            {
                new(NoteQuality.C, NoteModifier.Natural, ScaleQuality.Major),
                new(NoteQuality.A, NoteModifier.Natural, ScaleQuality.Minor),
                new(NoteQuality.G, NoteModifier.Natural, ScaleQuality.Major),
                new(NoteQuality.F, NoteModifier.Natural, ScaleQuality.Major),
                new(NoteQuality.E, NoteModifier.Natural, ScaleQuality.Minor),
                new(NoteQuality.D, NoteModifier.Natural, ScaleQuality.Minor),
            });
    }
    
    [Test]
    [TestCaseSource(nameof(GetScalesByNotesTestCases))]
    public void GetScalesByNotesTest(List<Note> notes, List<Scale> expectedScales)
    {
        // Given
        var service = new MusicTheoryService();
    
        // When
        var result = service.GetScalesByNotes(notes);
        
        // Then
        result.Should().BeEquivalentTo(expectedScales);
    }
}