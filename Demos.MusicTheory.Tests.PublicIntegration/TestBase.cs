﻿using Demos.MusicTheory.Services;
using Demos.MusicTheory.Setup;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.PublicIntegration;

[TestFixture]
internal abstract class TestBase
{
    [OneTimeSetUp]
    public void Setup()
    {
        MusicTheorySetup.Setup();
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        ServicesManager.ResetServiceProvider();
    }
}