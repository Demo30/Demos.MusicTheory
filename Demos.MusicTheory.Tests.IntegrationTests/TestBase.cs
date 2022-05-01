using Demos.MusicTheory.ChromaticContext.Helpers;
using Demos.MusicTheory.Services;
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

    protected void RegisterService<TService>()
    {
        ServicesManager.ServicesProvider.RegisterService<TService>(() => (TService)Activator.CreateInstance(typeof(TService))!);
    }

    /// <summary>
    /// These are to be considered as implementations inherent to the tested class.
    /// </summary>
    private static void InitializeStaticCaches()
    {
        BaseChromaticIndexMapper.InitializeMapper();
    }
}