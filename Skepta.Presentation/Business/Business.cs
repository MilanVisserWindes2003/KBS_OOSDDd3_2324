using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Business
{
    // DELETE THIS FILE IF NOT USED ANYMORE
    public class Business
    {
        bool isLoggedin;

        DataAccess.DataAccess? dataConnection;
        public DispatcherTimer? timer;

        private int _textLength;
        private string _textDifficulty;
        private bool _isSpeechExercise;
        private string randomText = "";

        private TimeSpan elapsedTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public string RandomText
        {
            get { return randomText; }
            set { randomText = value; }
        }

        public int TextLength
        {
            get { return _textLength; }
            set { _textLength = value; }
        }

        public string TextDifficulty
        {
            get { return _textDifficulty; }
            set { _textDifficulty = value; }
        }

        public bool IsSpeechExercise
        {
            get { return _isSpeechExercise; }
            set { _isSpeechExercise = value; }
        }


        public Business()
        {

        }

        protected virtual void PropertyChangedHandler(string property)
        {

        }
        public void textDifficultySetter(string difficulty)
        {
        }

        public void textLengthSetter(int length)
        {

        }
        public void isSpeechExerciseSetter(bool isSpeechExercise)
        {

        }

        public bool CheckLogin(string username, string password)
        {
            return false;
        }

        public (bool, string) CheckRegister(string username, string password, string password2)
        {
            return (false, "Er is iets fout gegaan. Probeer opnieuw.");
        }

        public void setSpeechExercise(string exerciseTag)
        {
        }

        //set text methode aan dataAccess vragen
        public string obtainRandomText(string niveau, int lengte)
        {
            return "";
        }

        public string GetPrintableCharacter(Key key, bool isShiftPressed)
        {
            return "";
        }

        public bool IsPrintableKey(Key key)
        {
            return true;

        }

        public void AddTimerTick()
        {

        }

        public string GetTimerText()
        {
            return $"{elapsedTime}";
        }
    }
}