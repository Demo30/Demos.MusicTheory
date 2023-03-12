using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.PublicIntegration;

[TestFixture]
internal class ChordTests : TestBase
{
    internal record ChordConstructionData(Note BaseNote, ChordQuality ChordQuality);

    public static IEnumerable<TestCaseData> ChordConstructionTestCases()
    {
        yield return new TestCaseData(
            new ChordConstructionData(new Note(NoteQuality.C, NoteModifier.Natural, 4), ChordQuality.MajorTriad),
            new List<List<Note>>
            {
                new()
                {
                    new(NoteQuality.C, NoteModifier.Natural, 4),
                    new(NoteQuality.E, NoteModifier.Natural, 4),
                    new(NoteQuality.G, NoteModifier.Natural, 4),    
                }
            }
        );
        
        yield return new TestCaseData(
            new ChordConstructionData(new Note(NoteQuality.C, NoteModifier.Natural, 4), ChordQuality.MinorTriad),
            new List<List<Note>>
            {
                new()
                {
                    new(NoteQuality.C, NoteModifier.Natural, 4),
                    new(NoteQuality.E, NoteModifier.Flat, 4),
                    new(NoteQuality.G, NoteModifier.Natural, 4),    
                }
            }
        );
    }

    [Test]
    [TestCaseSource(nameof(ChordConstructionTestCases))]
    public void ChordConstructionTest(ChordConstructionData chordConstructionData, List<List<Note>> expectedInversions)
    {
        // Given
        var chord = new Chord(chordConstructionData.BaseNote, chordConstructionData.ChordQuality);

        // When
        var chordNotes = chord.ChordNotes;

        // Then
        chordNotes.Should().BeEquivalentTo(expectedInversions.FirstOrDefault(), opts => opts.WithStrictOrdering());
    }
    
    [Test]
    [TestCaseSource(nameof(ChordConstructionTestCases))]
    public void ChordInversionsTest(ChordConstructionData chordConstructionData, List<List<Note>> expectedInversions)
    {
        // Given
        var chord = new Chord(chordConstructionData.BaseNote, chordConstructionData.ChordQuality);

        // When
        var inversions = chord.Inversions.ToList();

        // Then
        var i = 0;
        foreach (var expectedInversion in expectedInversions)
        {
            inversions[i].Should().BeEquivalentTo(expectedInversion);
            i++;
        }
    }

    public static IEnumerable<TestCaseData> GetChordToScalesTestCases()
    {
        yield return new TestCaseData(
            new Chord(new Note(NoteQuality.C, NoteModifier.Natural, 4), ChordQuality.MajorTriad),
            new Scale(NoteQuality.C, NoteModifier.Natural, ScaleQuality.Major));
        
        yield return new TestCaseData(
            new Chord(new Note(NoteQuality.C, NoteModifier.Natural, 4), ChordQuality.MajorTriad),
            new Scale(NoteQuality.A, NoteModifier.Natural, ScaleQuality.Minor));
    }

    [Test]
    [TestCaseSource(nameof(GetChordToScalesTestCases))]
    public void ChordToScalesTest(Chord chord, Scale expectedScale)
    {
        // When
        var result = MusicTheoryService.Instance.GetScalesByChord(chord);
        
        // Then
        result.Should().ContainEquivalentOf(expectedScale);
    }
}
