using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Contexts.ChromaticContext;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext.ChromaticSpace
{
    [TestFixture]
    public class ChromaticSpaceTests
    {
        [Theory]
        public void ValidatorsRunOnEntityAdd()
        {
            // Given
            var chromaticNote = new Contexts.ChromaticContext.ChromaticEntity(1);

            Mock<IChromaticSpaceIndexValidator> indexValidator = new Mock<IChromaticSpaceIndexValidator>();
            indexValidator
                .Setup(mock => mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticNote>(),
                    It.IsAny<IChromaticNote[]>()))
                .Returns(true);

            Contexts.ChromaticContext.ChromaticSpace chromaticSpace = new Contexts.ChromaticContext.ChromaticSpace(indexValidator.Object);

            // When
            chromaticSpace.AddMusicalEntity(chromaticNote);

            // Then
            indexValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticNote>(),
                    It.IsAny<IChromaticNote[]>()),
                Times.Once);
        }

        [Theory]
        public void EvaluatesValidatorOnAdd()
        {
            // Given
            var chromaticNote = new Contexts.ChromaticContext.ChromaticEntity(1);

            Mock<IChromaticSpaceIndexValidator> indexValidator = new Mock<IChromaticSpaceIndexValidator>();
            indexValidator
                .Setup(mock => mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticNote>(),
                    It.IsAny<IChromaticNote[]>()))
                .Returns(false);

            Contexts.ChromaticContext.ChromaticSpace chromaticSpace = new Contexts.ChromaticContext.ChromaticSpace(indexValidator.Object);

            // When
            Action act = () => { chromaticSpace.AddMusicalEntity(chromaticNote); };

            // Then
            act.Should().Throw<MusicalEntityValidatorException>();
            indexValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticNote>(),
                    It.IsAny<IChromaticNote[]>()),
                Times.Once);
        }

        [Theory]
        public void ChromaticNotesAdded()
        {
            // Given
            IEnumerable<IChromaticNote> notes = new[]
            {
                new Contexts.ChromaticContext.ChromaticEntity(1),
                new Contexts.ChromaticContext.ChromaticEntity(2),
                new Contexts.ChromaticContext.ChromaticEntity(3),
                new Contexts.ChromaticContext.ChromaticEntity(5)
            };
            int numberOfNotes = notes.Count();

            Mock<IChromaticSpaceIndexValidator> indexValidator = new Mock<IChromaticSpaceIndexValidator>();
            indexValidator
                .Setup(mock => mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticNote>(),
                    It.IsAny<IChromaticNote[]>()))
                .Returns(true);

            Contexts.ChromaticContext.ChromaticSpace chromaticSpace = new Contexts.ChromaticContext.ChromaticSpace(indexValidator.Object);

            // When
            notes.ToList().ForEach(note => chromaticSpace.AddMusicalEntity(note));

            // Then
            indexValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticNote>(),
                    It.IsAny<IChromaticNote[]>()),
                Times.Exactly(numberOfNotes));
            chromaticSpace.MusicalEntities.Should().HaveCount(numberOfNotes);
        }
    }
}
