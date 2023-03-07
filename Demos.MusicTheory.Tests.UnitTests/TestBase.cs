using Demos.MusicTheory.ChromaticContext.Helpers;

namespace Demos.MusicTheory.Tests.UnitTests;

internal class TestBase
{
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