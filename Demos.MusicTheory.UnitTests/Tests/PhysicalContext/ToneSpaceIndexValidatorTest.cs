using System;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Contexts.PhysicalContext;
using Demos.MusicTheory.PhysicalContext;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.UnitTests.Tests.PhysicalContext;

[TestFixture]
public class ToneSpaceIndexValidatorTest
{
    private ToneSpaceFrequencyValidator GetValidator()
    {
        return new ToneSpaceFrequencyValidator();
    }


    [Theory]
    public void ValidationShouldThrowException_OnConflictingFrequencies()
    {
        // Given
        var entity = new Tone(200.2);
        var entities = new[]
        {
            new Tone(200)
        };

        var validator = GetValidator();

        // When
        var act = () => { validator.ValidateEntityCompatibility(entity, entities); };

        // Then
        act.Should().Throw<MusicalEntityValidatorException>();
    }

    [Theory]
    public void ValidationShouldSucceed_WhenNoConflictingNotes()
    {
        // Given
        var entity = new Tone(600);
        var entities = new[]
        {
            new Tone(200),
            new Tone(250)
        };

        var validator = GetValidator();

        // When
        var act = () => { validator.ValidateEntityCompatibility(entity, entities); };

        // Then
        act.Should().NotThrow();
    }

    [Theory]
    public void ValidationShouldSucceed_WhenArrayEmpty()
    {
        // Given
        var entity = new Tone(200);
        var entities = Array.Empty<ITone>();

        var validator = GetValidator();

        // When
        var act = () => { validator.ValidateEntityCompatibility(entity, entities); };

        // Then
        act.Should().NotThrow();
    }

    [Theory]
    public void ThrowOnIncompatibleEntityAdded()
    {
        // Given
        var entity = new ChromaticEntity(2);
        var entities = Array.Empty<ChromaticEntity>();

        var validator = GetValidator();

        // When
        var act = () => { validator.ValidateEntityCompatibility(entity, entities); };

        // Then
        act.Should().Throw<ArgumentException>();
    }
}