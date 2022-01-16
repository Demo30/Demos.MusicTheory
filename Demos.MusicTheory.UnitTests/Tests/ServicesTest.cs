using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.Services;
using Demos.MusicTheory.Setup;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.UnitTests.Tests;

[TestFixture]
public class ServicesTest
{
    [Test]
    public void TestServices()
    {
        // Given
        MusicTheoryServices.Setup();
        
        // When
        var keys = ServicesManager.ServicesProvider.Services.Keys;
        
        // Then
        keys.Should().HaveCount(3);
    }
    
    [Test]
    public void TestServices2()
    {
        // Given
        MusicTheoryServices.Setup();
        
        // When + Then
        foreach (var serviceType in ServicesManager.ServicesProvider.Services.Keys)
        {
            object GetService() => ServicesManager.GetService(serviceType);
            Assert.DoesNotThrow(() => GetService());

            var service = GetService();
            service.Should().NotBeNull();
        }
    }
    
}