﻿namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal interface IChromaticNoteFullyQualifiedProviderFromChromaticIndex
{
    public ChromaticNoteEnharmonicCluster GetEnharmonicNoteCluster(int chromaticIndex);
}