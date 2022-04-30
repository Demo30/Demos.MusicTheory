using Demos.MusicTheory.ChromaticContext.Helpers;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests;

public class TestBase
{
    [TearDown]
    protected virtual void TearDown()
    {
        Services.ServicesManager.ResetServiceProvider();
    }
    protected TestBase()
    {
        InitializeStaticCaches();
    }

    /// <summary>
    /// These are to be considered as implementations inherent to the tested class.
    /// </summary>
    private static void InitializeStaticCaches()
    {
        BaseChromaticIndexMapper.InitializeMapper();
    }
}