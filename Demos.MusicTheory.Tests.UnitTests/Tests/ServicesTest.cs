﻿using Demos.MusicTheory.Services;
using Demos.MusicTheory.Setup;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests;

[TestFixture]
public class ServicesTest
{
    [TearDown]
    public void TearDown()
    {
        ServicesManager.ResetServiceProvider();
    }
    
    [Test]
    public void TestServices()
    {
        // Given
        MusicTheorySetup.Setup();
        
        // When
        var keys = ServicesManager.ServicesProvider.Services.Keys;
        
        // Then
        keys.Should().HaveCount(5); // TODO: this is kinda stupid, annotate with attribute maybe
    }
    
    [Test]
    public void TestServices2()
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