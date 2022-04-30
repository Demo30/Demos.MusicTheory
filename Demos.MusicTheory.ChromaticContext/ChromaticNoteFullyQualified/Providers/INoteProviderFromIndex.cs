namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal interface INoteProviderFromIndex
{
    public NoteEnharmonics GetEnharmonics(int chromaticIndex);
}