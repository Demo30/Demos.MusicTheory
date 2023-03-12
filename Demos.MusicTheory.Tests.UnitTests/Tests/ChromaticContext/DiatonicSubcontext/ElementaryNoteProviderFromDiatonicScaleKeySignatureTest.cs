using System.Linq;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext.DiatonicSubcontext;

[TestFixture]
internal class ElementaryNoteProviderFromDiatonicScaleKeySignatureTest
{
    [Theory]
    [TestCase(KeySignatures.Simple, new[] {"C", "D", "E", "F", "G", "A", "B"})]
    [TestCase(KeySignatures.Sharps1, new[] {"G", "A", "B", "C", "D", "E", "FSharp"})]
    [TestCase(KeySignatures.Sharps2, new[] {"D", "E", "FSharp", "G", "A", "B", "CSharp"})]
    [TestCase(KeySignatures.Sharps3, new[] {"A", "B", "CSharp", "D", "E", "FSharp", "GSharp"})]
    [TestCase(KeySignatures.Sharps4, new[] {"E", "FSharp", "GSharp", "A", "B", "CSharp", "DSharp"})]
    [TestCase(KeySignatures.Sharps5, new[] {"B", "CSharp", "DSharp", "E", "FSharp", "GSharp", "ASharp"})]
    [TestCase(KeySignatures.Sharps6, new[] {"FSharp", "GSharp", "ASharp", "B", "CSharp", "DSharp", "ESharp"})]
    [TestCase(KeySignatures.Sharps7, new[] {"CSharp", "DSharp", "ESharp", "FSharp", "GSharp", "ASharp", "BSharp"})]
    [TestCase(KeySignatures.Flats1, new[] {"F", "G", "A", "BFlat", "C", "D", "E"})]
    [TestCase(KeySignatures.Flats2, new[] {"BFlat", "C", "D", "EFlat", "F", "G", "A"})]
    [TestCase(KeySignatures.Flats3, new[] {"EFlat", "F", "G", "AFlat", "BFlat", "C", "D"})]
    [TestCase(KeySignatures.Flats4, new[] {"AFlat", "BFlat", "C", "DFlat", "EFlat", "F", "G"})]
    [TestCase(KeySignatures.Flats5, new[] {"DFlat", "EFlat", "F", "GFlat", "AFlat", "BFlat", "C"})]
    [TestCase(KeySignatures.Flats6, new[] {"GFlat", "AFlat", "BFlat", "CFlat", "DFlat", "EFlat", "F"})]
    [TestCase(KeySignatures.Flats7, new[] {"CFlat", "DFlat", "EFlat", "FFlat", "GFlat", "AFlat", "BFlat"})]
    public void ShouldReturnElementaryNotesForGivenDiatonicScaleSignature(KeySignatures key, string[] outputs)
    {
        // Given
        var provider = new ElementaryNoteFromDiatonicScaleKeySignatureProvider();

        // When
        var notes = provider.GetChromaticElementaryNotes(key).ToList();

        // Then
        notes.Should().HaveCount(7);
        foreach (var note in notes) outputs.Should().Contain(note.ToString()); // not checking order here
    }
}