using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SpeechHelper
    {
        public void Play(string phrase)
        {
            SpeechSynthesizer speech = new SpeechSynthesizer();
            CultureInfo keyboardCulture = System.Windows.Forms.InputLanguage.CurrentInputLanguage.Culture;
            InstalledVoice neededVoice = speech.GetInstalledVoices(keyboardCulture).FirstOrDefault();
            if (neededVoice == null)
            {
                phrase = "Unsupported Language";
            }
            else if (!neededVoice.Enabled)
            {
                phrase = "Voice Disabled";
            }
            else
            {
                speech.SelectVoice(neededVoice.VoiceInfo.Name);
            }

            speech.Speak(phrase);
        }
    }
}
