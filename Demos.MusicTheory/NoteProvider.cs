using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory;

public class NoteProvider
{
    private readonly ChromaticNoteFullyQualifiedProviderFromNoteBySpan _bySpan;

    // Demonstrates a possibility of hiding internal dependencies behind a parameterless facade class
    public NoteProvider() : this((ChromaticNoteFullyQualifiedProviderFromNoteBySpan)MusicTheoryServices.ServiceProvider.GetService(typeof(ChromaticNoteFullyQualifiedProviderFromNoteBySpan)))
    {
        
    }

    public ChromaticNoteEnharmonicCluster GetEnharmonicNoteCluster(ChromaticNoteFullyQualified note, int chromaticIndexSpan, OneDimensionDirection direction) 
        => _bySpan.GetEnharmonicNoteCluster(note, chromaticIndexSpan, direction);
    
    internal NoteProvider(ChromaticNoteFullyQualifiedProviderFromNoteBySpan bySpan)
    {
        _bySpan = bySpan;
    }
}