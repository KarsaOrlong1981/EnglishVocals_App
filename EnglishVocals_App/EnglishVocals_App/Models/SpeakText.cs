using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EnglishVocals_App.Models
{
    public class SpeakText
    {
        public async Task SpeakEnglish(string text)
        {
            var locales = await TextToSpeech.GetLocalesAsync();

            // Grab the first locale
            var locale = locales.FirstOrDefault();

            var settings = new SpeechOptions()
            {
                Volume = .85f,
                Pitch = 1.0f,
                Locale = locale
            };

            await TextToSpeech.SpeakAsync(text, settings);
        }
        public async Task SpeakGerman(string text)
        {
            var settings = new SpeechOptions()
            {
                Volume = .85f,
                Pitch = 1.0f
            };
            await TextToSpeech.SpeakAsync(text, settings);
        }
    }
}
