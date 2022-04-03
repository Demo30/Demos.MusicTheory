using System.Linq;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext.ChromaticSpace;

[TestFixture]
public class ChromaticSpaceTest
{
    [Theory]
    public void ValidatorsRunOnEntityAdd()
    {
        // Given
        var chromaticNote = new ChromaticEntity(1);

        var indexValidator = new Mock<IChromaticSpaceIndexValidator>();
        indexValidator
            .Setup(mock => mock.ValidateEntityCompatibility(
                It.IsAny<IChromaticEntity>(),
                It.IsAny<IChromaticEntity[]>()))
            .Returns(true);

        var chromaticSpace = new MusicTheory.ChromaticContext.ChromaticSpace(indexValidator.Object);

        // When
        chromaticSpace.AddMusicalEntity(chromaticNote);

        // Then
        indexValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticEntity>(),
                    It.IsAny<IChromaticEntity[]>()),
            Times.Once);
    }

    [Theory]
    public void EvaluatesValidatorOnAdd()
    {
        // Given
        var chromaticNote = new ChromaticEntity(1);

        var indexValidator = new Mock<IChromaticSpaceIndexValidator>();
        indexValidator
            .Setup(mock => mock.ValidateEntityCompatibility(
                It.IsAny<IChromaticEntity>(),
                It.IsAny<IChromaticEntity[]>()))
            .Returns(false);

        var chromaticSpace = new MusicTheory.ChromaticContext.ChromaticSpace(indexValidator.Object);

        // When
        void Call() => chromaticSpace.AddMusicalEntity(chromaticNote);

        // Then
        Assert.Throws<MusicalEntityValidatorException>(Call);
        
        indexValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticEntity>(),
                    It.IsAny<IChromaticEntity[]>()),
            Times.Once);
    }

    [Theory]
    public void ChromaticNotesAdded()
    {
        // Given
        var notes = new[]
        {
            new ChromaticEntity(1),
            new ChromaticEntity(2),
            new ChromaticEntity(3),
            new ChromaticEntity(5)
        };
        var numberOfNotes = notes.Length;

        var indexValidator = new Mock<IChromaticSpaceIndexValidator>();
        indexValidator
            .Setup(mock => mock.ValidateEntityCompatibility(
                It.IsAny<IChromaticEntity>(),
                It.IsAny<IChromaticEntity[]>()))
            .Returns(true);

        var chromaticSpace = new MusicTheory.ChromaticContext.ChromaticSpace(indexValidator.Object);

        // When
        notes.ToList().ForEach(note => chromaticSpace.AddMusicalEntity(note));

        // Then
        indexValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticEntity>(),
                    It.IsAny<IChromaticEntity[]>()),
            Times.Exactly(numberOfNotes));
        chromaticSpace.MusicalEntities.Should().HaveCount(numberOfNotes);
    }
}