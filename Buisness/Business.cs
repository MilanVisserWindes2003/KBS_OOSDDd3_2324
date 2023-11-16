﻿using System;
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

        public int textLength;
        public string textDifficulty;
        public bool isSpeechExercise;

        public Business() { dataConnection = new DataAccess.DataAccess(); }

        public void textDifficultySetter(string difficulty)
        {
            this.textDifficulty = difficulty;
        }

        public void textLengthSetter(int length)
        {
            this.textLength = length;
        }
        public void isSpeechExerciseSetter(bool isSpeechExercise)
        {
            this.isSpeechExercise = isSpeechExercise;
        }

        public bool CheckLogin(string username, string password)
        {
            if (dataConnection.UsernameExists(username))
            {
                string databasePassword = dataConnection.GetPassword(username);
                if (databasePassword == password)
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            };
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
    }
}
