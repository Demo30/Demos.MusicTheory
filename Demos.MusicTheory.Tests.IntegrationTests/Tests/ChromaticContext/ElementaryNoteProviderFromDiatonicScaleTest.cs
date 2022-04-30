using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class ElementaryNoteProviderFromDiatonicScale : TestBase
{
    private ElementaryNotesProviderFromDiatonicScale _provider = null!;

    [SetUp]
    public void SetUp()
    {
        Services.ServicesManager.ServicesProvider.RegisterService(() => new ElementaryNoteFromDiatonicScaleKeySignatureProvider());
        _provider = new ElementaryNotesProviderFromDiatonicScale();
    }
    
    [Theory]
    [TestCaseSource(nameof(GetTestCases))]
    public void ShouldReturnElementaryNotesForGivenDiatonicScaleSignature(DiatonicScale scale, string[] outputs)
    {
        // Given

        // When
        var notes = _provider.GetChromaticElementaryNotes(scale).ToList();

        // Then
        notes.Should().HaveCount(7);
        for (var i = 0; i < notes.Count; i++) outputs[i].Should().BeEquivalentTo(notes[i].ToString()); // order matters
    }

    static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
            new[] {"C", "D", "E", "F", "G", "A", "B"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.A, NotationSymbols.None, DiatonicScaleType.Minor),
            new[] {"A", "B", "C", "D", "E", "F", "G"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.G, NotationSymbols.None, DiatonicScaleType.Major),
            new[] {"G", "A", "B", "C", "D", "E", "FSharp"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.E, NotationSymbols.None, DiatonicScaleType.Minor),
            new[] {"E", "FSharp", "G", "A", "B", "C", "D"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.D, NotationSymbols.None, DiatonicScaleType.Major),
            new[] {"D", "E", "FSharp", "G", "A", "B", "CSharp"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.B, NotationSymbols.None, DiatonicScaleType.Minor),
            new[] {"B", "CSharp", "D", "E", "FSharp", "G", "A"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.A, NotationSymbols.None, DiatonicScaleType.Major),
            new[] {"A", "B", "CSharp", "D", "E", "FSharp", "GSharp"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.F, NotationSymbols.Sharp, DiatonicScaleType.Minor),
            new[] {"FSharp", "GSharp", "A", "B", "CSharp", "D", "E"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.E, NotationSymbols.None, DiatonicScaleType.Major),
            new[] {"E", "FSharp", "GSharp", "A", "B", "CSharp", "DSharp"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.C, NotationSymbols.Sharp, DiatonicScaleType.Minor),
            new[] {"CSharp", "DSharp", "E", "FSharp", "GSharp", "A", "B"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.B, NotationSymbols.None, DiatonicScaleType.Major),
            new[] {"B", "CSharp", "DSharp", "E", "FSharp", "GSharp", "ASharp"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.G, NotationSymbols.Sharp, DiatonicScaleType.Minor),
            new[] {"GSharp", "ASharp", "B", "CSharp", "DSharp", "E", "FSharp"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.F, NotationSymbols.Sharp, DiatonicScaleType.Major),
            new[] {"FSharp", "GSharp", "ASharp", "B", "CSharp", "DSharp", "ESharp"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.D, NotationSymbols.Sharp, DiatonicScaleType.Minor),
            new[] {"DSharp", "ESharp", "FSharp", "GSharp", "ASharp", "B", "CSharp"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.C, NotationSymbols.Sharp, DiatonicScaleType.Major),
            new[] {"CSharp", "DSharp", "ESharp", "FSharp", "GSharp", "ASharp", "BSharp"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.A, NotationSymbols.Sharp, DiatonicScaleType.Minor), // TODO: A#?
            new[] {"ASharp", "BSharp", "CSharp", "DSharp", "ESharp", "FSharp", "GSharp"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.F, NotationSymbols.None, DiatonicScaleType.Major),
            new[] {"F", "G", "A", "BFlat", "C", "D", "E"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.D, NotationSymbols.None, DiatonicScaleType.Minor),
            new[] {"D", "E", "F", "G", "A", "BFlat", "C"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.B, NotationSymbols.Flat, DiatonicScaleType.Major),
            new[] {"BFlat", "C", "D", "EFlat", "F", "G", "A"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.G, NotationSymbols.None, DiatonicScaleType.Minor),
            new[] {"G", "A", "BFlat", "C", "D", "EFlat", "F"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.E, NotationSymbols.Flat, DiatonicScaleType.Major),
            new[] {"EFlat", "F", "G", "AFlat", "BFlat", "C", "D"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Minor),
            new[] {"C", "D", "EFlat", "F", "G", "AFlat", "BFlat"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.A, NotationSymbols.Flat, DiatonicScaleType.Major),
            new[] {"AFlat", "BFlat", "C", "DFlat", "EFlat", "F", "G"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.F, NotationSymbols.None, DiatonicScaleType.Minor),
            new[] {"F", "G", "AFlat", "BFlat", "C", "DFlat", "EFlat"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.D, NotationSymbols.Flat, DiatonicScaleType.Major),
            new[] {"DFlat", "EFlat", "F", "GFlat", "AFlat", "BFlat", "C"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.B, NotationSymbols.Flat, DiatonicScaleType.Minor),
            new[] {"BFlat", "C", "DFlat", "EFlat", "F", "GFlat", "AFlat"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.G, NotationSymbols.Flat, DiatonicScaleType.Major),
            new[] {"GFlat", "AFlat", "BFlat", "CFlat", "DFlat", "EFlat", "F"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.E, NotationSymbols.Flat, DiatonicScaleType.Minor),
            new[] {"EFlat", "F", "GFlat", "AFlat", "BFlat", "CFlat", "DFlat"});
        
        yield return new TestCaseData(new DiatonicScale(NoteQuality.C, NotationSymbols.Flat, DiatonicScaleType.Major),
            new[] {"CFlat", "DFlat", "EFlat", "FFlat", "GFlat", "AFlat", "BFlat"});
        yield return new TestCaseData(new DiatonicScale(NoteQuality.A, NotationSymbols.Flat, DiatonicScaleType.Minor), // TODO: Ab??
            new[] {"AFlat", "BFlat", "CFlat", "DFlat", "EFlat", "FFlat", "GFlat"});
        


    }
}