namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;

internal interface INoteProviderFromIndex
{
    public NoteEnharmonicsInternal GetEnharmonics(int chromaticIndex);
}