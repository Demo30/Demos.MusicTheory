using Demos.MusicTheory.Commons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext
{
    // IoC?? Spaghetti??


    /// <summary>
    /// Given a diatonic scale key signature, a collection of base characteristics is returned.
    /// </summary>
    public static class DiatonicScaleKeySignatureBaseCharacteristicsProvider
    {
        public static IEnumerable<BaseChromaticCharacteristic> GetBaseChromaticCharacteristic(KeySignatures key)
        {
            List<BaseChromaticCharacteristic> characteristics = GetFlatsAndSharps(key).ToList();
            var qualitiesWithoutModification = ((ChromaticNoteQuality[])Enum.GetValues(typeof(ChromaticNoteQuality)))
                .Where(quality => characteristics.Count(ch => ch.Quality.Equals(quality)) == 0)
                .Where(quality => quality != ChromaticNoteQuality.UNKNOWN);

            foreach(var quality in qualitiesWithoutModification)
            {
                characteristics.Add(new BaseChromaticCharacteristic(quality, NotationSymbols.None));
            }
            return characteristics.ToArray();
        }

        private static List<BaseChromaticCharacteristic> GetFlatsAndSharps(KeySignatures key)
        {
            List<BaseChromaticCharacteristic> baseCharacteristics = new List<BaseChromaticCharacteristic>();

            switch (key)
            {
                case KeySignatures.Simple:
                    break;
                case KeySignatures.Flats_1:
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.B, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_2:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats_1));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.E, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_3:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats_2));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.A, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_4:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats_3));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.D, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_5:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats_4));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.G, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_6:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Flats_5));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.C, NotationSymbols.Flat));
                    break;
                case KeySignatures.Sharps_1:
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.F, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_2:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps_1));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.C, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_3:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps_2));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.G, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_4:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps_3));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.D, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_5:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps_4));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.A, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_6:
                    baseCharacteristics.AddRange(GetFlatsAndSharps(KeySignatures.Sharps_5));
                    baseCharacteristics.Add(new BaseChromaticCharacteristic(ChromaticNoteQuality.E, NotationSymbols.Sharp));
                    break;
                default:
                    throw new Exception();
            }

            return baseCharacteristics;
        }
    }
}
