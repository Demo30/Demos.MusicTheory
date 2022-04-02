using Demos.MusicTheory.Commons;
using NUnit.Framework;
using System;
using Demos.MusicTheory.ChromaticContext;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext.ChromaticSpace;

[TestFixture]
public class ChromaticSpaceIndexValidatorTests
{
    [Theory]
    public void ValidationShouldThrowException_OnConflictingIndexes()
    {
        // Given
        var entity = new ChromaticEntity(2);
        var entities = new[]
        {
            new ChromaticEntity(1),
            new ChromaticEntity(2),
            new ChromaticEntity(3)
        };

        var indexValidator = new ChromaticSpaceIndexValidator();

        // When
        void Call() => indexValidator.ValidateEntityCompatibility(entity, entities);

        // Then
        Assert.Throws<MusicalEntityValidatorException>(Call);
    }

    [Theory]
    public void ValidationShouldSucceed_WhenNoConflictingNotes()
    {
        // Given
        var entity = new ChromaticEntity(2);
        var entities = new[]
        {
            new ChromaticEntity(1),
            new ChromaticEntity(3),
            new ChromaticEntity(4)
        };

        var indexValidator = new ChromaticSpaceIndexValidator();

        // When
        void Call() => indexValidator.ValidateEntityCompatibility(entity, entities);

        // Then
        Assert.DoesNotThrow(Call);
    }

    [Theory]
    [Test]
    public void ValidationShouldSucceed_WhenArrayEmpty()
    {
        // Given
        var entity = new ChromaticEntity(2);
        var entities = Array.Empty<IChromaticEntity>();

        var indexValidator = new ChromaticSpaceIndexValidator();

        // When
        void Call() => indexValidator.ValidateEntityCompatibility(entity, entities);

        // Then
        Assert.DoesNotThrow(Call);
    }

    [Theory]
    public void ThrowOnIncompatibleEntityAdded()
    {
        // Given
        var entity = new Contexts.PhysicalContext.Tone(440);
        var entities = Array.Empty<Contexts.PhysicalContext.Tone>();

        var indexValidator = new ChromaticSpaceIndexValidator();

        // When
        void Call() => indexValidator.ValidateEntityCompatibility(entity, entities);

        // Then
        Assert.Throws<ArgumentException>(Call);
    }
}