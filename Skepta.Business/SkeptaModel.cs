using Buisness.TTS;
using Skepta.Business.Util;
using Skepta.DataAcces.HistoryFolder;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;


namespace Skepta.Business;

public class SkeptaModel : ObservableObject
{
    private DataAccess.DataAccess dataConnection;
    private int _textLength;
    private string _textDifficulty;
    private bool _isSpeechExercise;
    private string randomText = "";
    private string username = string.Empty;
    private TimeSpan elapsedTime { get; set; }

    public SkeptaModel()
    {
        dataConnection = new DataAccess.DataAccess();
        TTSConverter = new TextToSpeechConverter();
        result = new ResultsLogic.ResultsLogic();
    }

    public string Username
    {
        get { return username; }
        set { username = value; NotifyPropertyChanged(nameof(Username)); }
    }
    public TimeSpan ElapsedTime
    {
        get { return elapsedTime; }
        set { elapsedTime = value; }
    }

    public string RandomText
    {
        get { return randomText; }
        set { randomText = value; NotifyPropertyChanged(nameof(RandomText)); }
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
        set { _isSpeechExercise = value; NotifyPropertyChanged(nameof(IsSpeechExercise)); }
    }

    public bool IsPersonalized { get; set; } = false;
    public ResultsLogic.ResultsLogic result { get; }
    public TextToSpeechConverter TTSConverter { get; }

    //Checks if the login credentials are valid
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

    //Calls the ObtainTextId from the dataAccess layer
    public string ObtainTextId(string level, string length, string content)
    {
        return dataConnection.ObtainTextId(level, length, content);
    }

    //Calls the InsertHistory from the dataAccess layer
    public void InsertHistoryData(History history)
    {
        dataConnection.InsertHistoryData(history);
    }

    //Calls the ObtainHistory from the dataAccess layer
    public History ObtainHistory(string username)
    {
        return dataConnection.ObtainHistory(username);
    }

    //Checks if the username, passwords are valid to register a new account
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

    //Obtains a text depending on the level, length and if the personalised text setting is on
    public string ObtainRandomText()
    {
        if (string.IsNullOrEmpty(TextDifficulty) || TextLength == 0)
        {
            return string.Empty;
        }

        List<string> teksten = dataConnection.GetPersonalizedTexts(TextDifficulty, TextLength.ToString(), Username);
        if (IsPersonalized == false || teksten == null ||teksten.Count == 0)
        {
            teksten = dataConnection.ObTainTexts(TextDifficulty, TextLength);
        }

        Random random = new Random();
        int randomIndex = random.Next(0, teksten.Count);
        this.randomText = teksten[randomIndex];
        return teksten[randomIndex];
    }
}

