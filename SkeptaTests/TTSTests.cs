using Buisness.TTS;
using Skepta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeptaTests
{
    public class TTSTests
    {
        private TextToSpeechConverter tts;
        [SetUp]
        public void Setup()
        {
            tts = new TextToSpeechConverter();
        }

        [Test]
        public void PlayTextTest()
        {
            Assert.True(tts.PlayMode == PlayMode.Stopped);
            tts.PlayText("");
            Assert.True(tts.PlayMode == PlayMode.Playing);

        }

        [Test]
        public void PauseAndResumeTextTest()
        {
            tts.PlayText("");
            Assert.True(tts.PlayMode == PlayMode.Playing);
            tts.Pause();
            Assert.True(tts.PlayMode == PlayMode.Paused);
            tts.Resume();
            Assert.True(tts.PlayMode == PlayMode.Playing);
        }

        [Test]
        public void StopTextTest()
        {
            tts.PlayText("");
            tts.Stop();
            Assert.True(tts.PlayMode == PlayMode.Stopped);
        }

        [TestCase("Nederlands")]
        [TestCase("Belgisch")]
        [Test]
        public void SetVoiceCorrectly(string language)
        {
            Assert.True(tts.Voice == "Nederlands");
            tts.SetVoice(language);
            Assert.True(tts.Voice == language);   
        }

        [TestCase("Spaans")]
        [TestCase("Duits")]
        [TestCase("MadeUpLanguage")]
        [Test]
        public void SetVoiceIncorrectly(string language)
        {
            string voice = tts.Voice;
            tts.SetVoice(language);
            Assert.False(tts.Voice == language);
            Assert.True(tts.Voice == voice);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [Test]
        public void SetVolumeCorrectly(int volume)
        {
            Assert.True(tts.Volume == 5);
            tts.SetVolume(volume);
            Assert.True(tts.Volume == volume);
        }

        [TestCase(-1)]
        [TestCase(11)]
        [Test]
        public void SetVolumeIncorrectly(int volume)
        {
            Assert.True(tts.Volume == 5);
            tts.SetVolume(volume);
            if (volume < 0)
            {
                Assert.True(tts.Volume == 0);
            }
            if (volume > 10)
            {
                Assert.True(tts.Volume == 10);
            }
        }
    }
}
