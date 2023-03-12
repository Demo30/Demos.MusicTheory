using Demos.MusicTheory.Services;
using Demos.MusicTheory.Setup;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests;

[TestFixture]
internal class ServicesTest
{
    [TearDown]
    public void TearDown()
    {
        ServicesManager.ResetServiceProvider();
    }
    
    [Test]
    public void RegisteredServicesHaveExpectedCount()
    {
        // Given
        MusicTheorySetup.Setup();
        
        // When
        var keys = ServicesManager.ServicesProvider.Services.Keys;
        
        // Then
        keys.Should().HaveCount(8);
    }
    
    [Test]
    public void AllRegisteredServicesCanBeInstantiated()
    {
        // Given
        MusicTheorySetup.Setup();
        
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