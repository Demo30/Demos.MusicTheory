using Demos.MusicTheory.Commons;
using System;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext;

public class ElementaryNote
{
    public NoteQuality Quality { get; }

    public NotationSymbols Modifier
    {
        get => _modifier;
        private init =>
            _modifier = RelevantNotationSymbols.Contains(value)
                ? value
                : throw new ArgumentException(
                    $"Provided notation symbol: \"{value}\" is not a valid symbol for a chromatic note.");
    }

    private readonly NotationSymbols _modifier = NotationSymbols.None;

    public static NotationSymbols[] RelevantNotationSymbols { get; } =
    {
        NotationSymbols.None,
        NotationSymbols.Sharp,
        NotationSymbols.Flat,
        NotationSymbols.DoubleSharp,
        NotationSymbols.DoubleFlat
    };

    public ElementaryNote(NoteQuality quality, NotationSymbols modifier)
    {
        Quality = quality;
        Modifier = modifier;
    }

    public override string ToString()
    {
        return $"{Quality}{ModifierToString(Modifier)}";
    }

    private static string ModifierToString(NotationSymbols modifier)
    {
        if (!RelevantNotationSymbols.Contains(modifier))
            throw new InvalidOperationException();

        return modifier switch
        {
            NotationSymbols.Sharp => "Sharp",
            NotationSymbols.Flat => "Flat",
            NotationSymbols.DoubleFlat => "Double flat",
            NotationSymbols.DoubleSharp => "Double sharp",
            _ => ""
        };
    }
}