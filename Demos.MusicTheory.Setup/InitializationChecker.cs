using System;

namespace Demos.MusicTheory.Setup;

public static class InitializationChecker
{
    internal static void CheckLibraryInitialized()
    {
        if (!MusicTheorySetup.IsInitialized)
        {
            throw new InvalidOperationException("Initialization via Setup required before any usage.");
        }    
    }
}