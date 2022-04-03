using Demos.MusicTheory.ChromaticContext.Helpers;

namespace Demos.MusicTheory.Tests.IntegrationTests;

public class TestBase
{
    public TestBase()
    {
        InitializeStaticCaches();
    }

    /// <summary>
    /// These are to be considered as implementations inherent to the tested class.
    /// </summary>
    private void InitializeStaticCaches()
    {
        BaseChromaticIndexMapper.InitializeMapper();
    }
}