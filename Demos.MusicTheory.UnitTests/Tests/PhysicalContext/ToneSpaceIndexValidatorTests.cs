using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Abstractions.PhysicalContext;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Contexts.ChromaticContext;
using Demos.MusicTheory.Contexts.PhysicalContext;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext.ChromaticSpace
{
    [TestFixture]
    public class ToneSpaceIndexValidatorTests
    {
        private ToneSpaceFrequencyValidator GetValidator()
        {
            return new ToneSpaceFrequencyValidator();
        }


        [Theory]
        public void ValidationShouldThrowException_OnConflictingFrequencies()
        {
            // Given
            ITone entity = new Contexts.PhysicalContext.Tone(200.2);
            IEnumerable<ITone> entitites = new[]
            {
                new Contexts.PhysicalContext.Tone(200)
            };

            ToneSpaceFrequencyValidator validator = GetValidator();

            // When
            Action act = () => { validator.ValidateEntityCompatibility(entity, entitites); };

            // Then
            act.Should().Throw<MusicalEntityValidatorException>();
        }

        [Theory]
        public void ValidationShouldSucceed_WhenNoConflictingNotes()
        {
            // Given
            ITone entity = new Contexts.PhysicalContext.Tone(600);
            IEnumerable<ITone> entitites = new[]
            {
                new Contexts.PhysicalContext.Tone(200),
                new Contexts.PhysicalContext.Tone(250)
            };

            ToneSpaceFrequencyValidator validator = GetValidator();

            // When
            Action act = () => { validator.ValidateEntityCompatibility(entity, entitites); };

            // Then
            act.Should().NotThrow();
        }

        [Theory]
        public void ValidationShouldSucceed_WhenArrayEmpty()
        {
            // Given
            ITone entity = new Contexts.PhysicalContext.Tone(200);
            IEnumerable<ITone> entitites = Array.Empty<ITone>();

            ToneSpaceFrequencyValidator validator = GetValidator();

            // When
            Action act = () => { validator.ValidateEntityCompatibility(entity, entitites); };

            // Then
            act.Should().NotThrow();
        }

        [Theory]
        public void ThrowOnIncompatibleEntityAdded()
        {
            // Given
            Contexts.ChromaticContext.ChromaticEntity entity = new Contexts.ChromaticContext.ChromaticEntity(2);
            IEnumerable<Contexts.ChromaticContext.ChromaticEntity> entitites = Array.Empty<Contexts.ChromaticContext.ChromaticEntity>();

            ToneSpaceFrequencyValidator validator = GetValidator();

            // When
            Action act = () => { validator.ValidateEntityCompatibility(entity, entitites); };

            // Then
            act.Should().Throw<ArgumentException>();
        }
    }
}
