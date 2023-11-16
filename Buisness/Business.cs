using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class Business
    {
       
        bool isLoggedin;
        DataAccess.DataAccess dataConnection;

        private int _textLength;
        private string _textDifficulty;
        private bool _isSpeechExercise;

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

        public bool CheckRegister(string username, string password, string password2)
        {
            if (password == password2 && username.Length <= 20 && username.Length > 0 && password.Length > 0 && password.Length <= 20)
            {
                if (dataConnection.UsernameExists(username) == false)
                {
                    dataConnection.Register(username, password);
                    return true;
                }
            }
            return false;
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
    }
}
