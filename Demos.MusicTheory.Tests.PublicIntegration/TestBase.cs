using Demos.MusicTheory.Setup;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.PublicIntegration;

internal abstract class TestBase
{
    [OneTimeSetUp]
    public void Setup()
    {
        MusicTheorySetup.Setup();
    }
}