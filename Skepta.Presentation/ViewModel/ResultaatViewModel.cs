﻿using Business;
using GalaSoft.MvvmLight.Command;
using Skepta.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Skepta.Presentation.ViewModel.Commands;
<<<<<<< HEAD
using System.Threading;
using Skepta.DataAcces.HistoryFolder;
=======
using Skepta.Business.Result;
>>>>>>> 7de131a8126355bc2d1ae94eb30b5eb473da33e0

namespace Skepta.Presentation.ViewModel
{
    public class ResultaatViewModel : ViewModelBase
    {
        private SkeptaModel model;
        private ResultsLogic rsl;
        private double wpm;
        private char mistake;

<<<<<<< HEAD
        public string Typespeed { get; set; }
        public double wpmValue 
=======
        public double wpmValue
>>>>>>> 7de131a8126355bc2d1ae94eb30b5eb473da33e0
        {
            get
            {
                return wpm;
            }
            set
            {
                wpm = value;
                NotifyPropertyChanged(nameof(wpmValue));
            }
        }

        public char Mistake
        {
            get { return mistake; }
            set 
            { 
                mistake = value;
                NotifyPropertyChanged(nameof(Mistake));
            }
        }
        
        public ResultaatViewModel(SkeptaModel model) 
        { 
<<<<<<< HEAD
            this.model = model; 
             rsl = new ResultsLogic();
            
=======
            this.model = model;
            rsl = model.result;
>>>>>>> 7de131a8126355bc2d1ae94eb30b5eb473da33e0
            //this.model.aantalWoordenPerMinuut();
            // idk model.Text = model.GetTimerText();
        }

        public override void OpenPage()
        {
            rsl.aantalWoordenPerMinuut(model.RandomText, model.ElapsedTime);
            wpmValue = rsl.WPM;
<<<<<<< HEAD
            Typespeed = rsl.WPM.ToString();
            
=======
            Mistake = rsl.WorstMistake;
>>>>>>> 7de131a8126355bc2d1ae94eb30b5eb473da33e0
        }

        public ICommand back => new BaseCommand(BackCmd);

        private void BackCmd()
        {
            rsl.EmptyDictionairy();
            RequestPage = PageId.Exercise;
        }

        private void InsertHistory()
        {
            History history = new History();
            history.IsSpoken = model.IsSpeechExercise;
            history.TextId = model.ObtainTextId(model.TextDifficulty, model.TextLength.ToString(), model.RandomText);
            history.WorstMistake = "h";
            history.TypeSpeed = Typespeed;
            history.Username = model.Username;
            model.InsertHistoryData(history);
        }

    }
}
