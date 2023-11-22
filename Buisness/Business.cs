using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO.Ports;
using System.Diagnostics;

namespace Business
{
    public class Business
    {

        bool isLoggedin;
        Stopwatch stopWatch = new Stopwatch();
        DataAccess.DataAccess dataConnection;

        private int _textLength;
        private string _textDifficulty;
        private bool _isSpeechExercise;
        private string randomText = "";
        private double elapsedTime;

        public double ElapsedTime { 
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }
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


        public Business() { dataConnection = new DataAccess.DataAccess(); }

        public void textDifficultySetter(string difficulty)
        {
            this._textDifficulty = difficulty;
        }

        public void textLengthSetter(int length)
        {
            this._textLength = length;
        }
        public void isSpeechExerciseSetter(bool isSpeechExercise)
        {
            this._isSpeechExercise = isSpeechExercise;
        }

        public bool CheckLogin(string username, string password)
        {
            if (dataConnection.UsernameExists(username))
            {
                string databasePassword = dataConnection.GetPassword(username);

                // Check if the database password is not null and trim leading/trailing spaces
                if (!string.IsNullOrEmpty(databasePassword))
                {
                    databasePassword = databasePassword.Trim();

                    // Compare the trimmed database password with the entered password
                    if (databasePassword == password)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public (bool, string) CheckRegister(string username, string password, string password2)
        {
            if (dataConnection.UsernameExists(username))
            {
                return (false, "Gebruikersnaam is al in gebruik.");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                return (false, "Vul een gebruikersnaam in.");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return (false, "Vul een wachtwoord in.");
            }
            if (string.IsNullOrWhiteSpace(password2))
            {
                return (false, "Herhaal het wachtwoord.");
            }
            if (!(password == password2)) 
            {
                return (false, "Wachtwoorden komen niet overeen.");
            }
            if (!(username.Length <= 20)) 
            {
                return (false, "Gebruikersnaam mag maximaal 20 karaters zijn.");
            }
            if (!(password.Length <= 20))
            {
                return (false, "Wachtwoord mag maximaal 20 karaters zijn.");
            }
            if(dataConnection.Register(username, password))
            {
                return (true, "Je account is aangemaakt. Je kan nu inloggen.");
            }
            return (false, "Er is iets fout gegaan. Probeer opnieuw.");
        }

        public void setSpeechExercise(string exerciseTag)
        {
            if (exerciseTag == "Text") 
            { 
                _isSpeechExercise = false; 
            } 
            else 
            { 
                _isSpeechExercise = true; 
            }
        }
        //set text methode aan dataAccess vragen
        public string obtainRandomText(string niveau, int lengte)
        {
            List<string> teksten = dataConnection.ObTainTexts(niveau, lengte);
            Random random = new Random();
            int randomIndex = random.Next(0, teksten.Count);
            this.randomText = teksten[randomIndex]; 
            return teksten[randomIndex]; 
        }

                public string GetPrintableCharacter(Key key, bool isShiftPressed)
        {
            string keyString = key.ToString();

            if (key == Key.OemQuotes)
            {
                return isShiftPressed ? "\"" : "'";
            }
            else if (key == Key.OemQuestion)
            {
                return isShiftPressed ? "?" : "/";
            }
            else if (key == Key.OemPeriod)
            {
                return isShiftPressed ? ">" : ".";
            }
            else if (key == Key.OemComma)
            {
                return isShiftPressed ? "<" : ",";
            }
            else if (key == Key.D1)
            {
                return isShiftPressed ? "!" : "1";
            }

            return isShiftPressed ? keyString.ToUpper() : keyString.ToLower();
        }

        public bool IsPrintableKey(Key key)
        {
            return (key >= Key.A && key <= Key.Z) ||
                   (key >= Key.D0 && key <= Key.D9) ||
                   (key >= Key.NumPad0 && key <= Key.NumPad9) ||
                   key == Key.OemQuotes || // Double and single quotation marks
                   key == Key.OemQuestion || // Question mark
                   key == Key.OemPeriod || // Period (.)
                   key == Key.OemComma || // Comma (,)
                   key == Key.Oem1 || // Exclamation mark (!) - US keyboard layout
                   key == Key.Oem2 || // Slash (/) - specific to some keyboards
                   key == Key.Oem3 || // Grave accent (`) - specific to some keyboards
                   key == Key.OemPlus || // Plus sign (+)
                   key == Key.OemMinus || // Minus sign (-)
                   key == Key.OemOpenBrackets || // Opening square bracket ([)
                   key == Key.OemCloseBrackets; // Closing square bracket (])
        }
        public void waitToStartTimer()
        {
            Task.Delay(3);
            stopWatch.Start();
        }
        public void StopWatch()
        {
            stopWatch.Stop();
        }
        public void TimeElapsed()
        {
            elapsedTime = stopWatch.Elapsed.TotalSeconds;
        }
    }
}