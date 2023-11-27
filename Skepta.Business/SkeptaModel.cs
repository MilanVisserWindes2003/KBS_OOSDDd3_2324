using Buisness.TTS;
using Skepta.Business.Util;
using System.ComponentModel;
using System.Windows.Input;

namespace Skepta.Business;

public class SkeptaModel : ObservableObject
{
    private DataAccess.DataAccess dataConnection;

    bool isLoggedin;
    //Stopwatch stopWatch = new Stopwatch();

    private int _textLength;
    private string _textDifficulty;
    private bool _isSpeechExercise;
    private string randomText = "";

    private TimeSpan elapsedTime { get; set; }
    private double wpm = 10000000;
    public double aantalWoorden;

    public TimeSpan ElapsedTime { 
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
        set { _textLength = value; NotifyPropertyChanged(nameof(TextLength)); }
    }

    public string TextDifficulty
    {
        get { return _textDifficulty; }
        set { _textDifficulty = value; NotifyPropertyChanged(nameof(TextDifficulty)); }
    }

    public bool IsSpeechExercise
    {
        get { return _isSpeechExercise; }
        set { _isSpeechExercise = value; }
    }

    public double WPM
    {
        get { return wpm; }
        set { wpm = value; NotifyPropertyChanged(nameof(WPM)); }
    }

    public SkeptaModel()
    {
        dataConnection = new DataAccess.DataAccess();
        TTSConverter = new TextToSpeechConverter();
    }

    public void aantalWoordenPerMinuut()
    {
        WPM = Math.Ceiling((aantalWoorden / ElapsedTime.TotalSeconds) * 60);
        
    }

    public TextToSpeechConverter TTSConverter { get; }

    public void textLengthSetter(int length)
    {
        this._textLength = length;
    }
    public void isSpeechExerciseSetter(bool isSpeechExercise)
    {
        this._isSpeechExercise = isSpeechExercise;
    }

    public void WordCounting()
    {
        if (string.IsNullOrWhiteSpace(RandomText))
            return;

        var amountOfWords = RandomText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        aantalWoorden = amountOfWords.Length;
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
        if (dataConnection.Register(username, password))
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

    public string ObtainRandomText()
    {
        List<string> teksten = dataConnection.ObTainTexts(TextDifficulty, TextLength);
        Random random = new Random();
        int randomIndex = random.Next(0, teksten.Count);
        this.randomText = teksten[randomIndex];
        WordCounting();

        return teksten[randomIndex];
    }

    public void AddTimerTick()
    {
        elapsedTime.Add(TimeSpan.FromSeconds(1));
    }

    public string GetTimerText()
    {
        return $"{elapsedTime}";
    }
}

