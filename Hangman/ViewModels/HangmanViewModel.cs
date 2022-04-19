using Hangman.Commands;
using Hangman.Models;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Hangman.ViewModels
{
    public class HangmanViewModel : BaseVM
    {
        //------------------------CONSTRUCTOR---------------
        public HangmanViewModel(Players survivor,bool newOrLoad)
        {
            CurrentChaseList = new List<string>();
            CurrentLevelList = new List<string>();
            LevelList = new List<string>();
            fileManager = new FileModel();
            PlayersList = fileManager.LoadFileData();

            LevelList.Add("../Images/Levels/lvlOne.jpg");
            LevelList.Add("../Images/Levels/lvlTwo.jpg");
            LevelList.Add("../Images/Levels/lvlThree.jpg");
            LevelList.Add("../Images/Levels/lvlFour.jpg");
            LevelList.Add("../Images/Levels/lvlFive.jpg");

            CurrentLevelList.Add("../Images/Levels/startingLevel.jpg");
            CurrentLevelList.Add("../Images/Levels/firstLevel.jpg");
            CurrentLevelList.Add("../Images/Levels/secondLevel.jpg");
            CurrentLevelList.Add("../Images/Levels/thirdLevel.jpg");
            CurrentLevelList.Add("../Images/Levels/fourthLevel.jpg");
            CurrentLevelList.Add("../Images/Levels/fifthLevel.jpg");

            CurrentChaseList.Add("../Images/Levels/startingStage.jpg");
            CurrentChaseList.Add("../Images/Levels/firstStage.jpg");
            CurrentChaseList.Add("../Images/Levels/secondStage.jpg");
            CurrentChaseList.Add("../Images/Levels/thirdStage.jpg");
            CurrentChaseList.Add("../Images/Levels/fourthStage.jpg");
            CurrentChaseList.Add("../Images/Levels/fifthStage.jpg");
            
            char[] keys1 = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };
            char[] keys2 = { 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R' };
            char[] keys3 = { 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            Keyboard1 = new ObservableCollection<string>();
            Keyboard2 = new ObservableCollection<string>();
            Keyboard3 = new ObservableCollection<string>();

            CategoryManager = new CategoriesModel();

            foreach (var elem in keys1){
                Keyboard1.Add(elem.ToString());
            }
            foreach (var elem in keys2)
            {
                Keyboard2.Add(elem.ToString());
            }
            foreach (var elem in keys3)
            {
                Keyboard3.Add(elem.ToString());
            }

            CurrentSurvivor = survivor;

            if (newOrLoad)
            {
                isSave = false;
                categories = ECategories.ENone;
                CategoryButtonsVisibility = true;
                StartButtonVisibility = true;
                CurrentWord = "";
                CurrentWordArray = null;
                CurrentLevel = 0;
                LevelImage = CurrentLevelList[0];
                CurrentStageImage = CurrentChaseList[0];
                CurrentLevelImage = CurrentLevelList[0];
                MistakeStage = 0;
                timer = 30;
            }
            else
            {
                isSave = true;
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = false;
                var serializer = new XmlSerializer(typeof(List<GameModel>));

                GamesList = new List<GameModel>();
                using (StreamReader reader = new StreamReader("C:/Users/cimpi/source/repos/Hangman/Hangman/Files/games.xml"))
                {
                    var xmlReader = XmlReader.Create(reader, settings);
                    GamesList = (List<GameModel>)serializer.Deserialize(xmlReader);
                    reader.Close();
                }
                startLevelFromSave();
                CurrentWordArray = CategoryManager.ConvertWordToList(CurrentWord);
                StartButtonVisibility = true;
            }
        }

        public enum ECategories
        {
            ENone,
            EHorrorMovies,
            EHorrorGames,
            EHorrorVillains,
            EAllCategories
        }

        //---------------------VARIABLES---------------------------
        public ObservableCollection<string> Keyboard1 { get; set; }
        public ObservableCollection<string> Keyboard2 { get; set; }
        public ObservableCollection<string> Keyboard3 { get; set; }
        public ObservableCollection<Players> PlayersList { get; set; }

        private ICommand gamesSelectedCommand;
        private ICommand moviesSelectedCommand;
        private ICommand villainsSelectedCommand;
        private ICommand allSelectedCommand;
        private ICommand startPressedCommand;
        private ICommand keyboardButtonCommand;
        private ICommand saveButtonCommand;

        private FileModel fileManager;
        private Players currentSurvivor;
        private Timer _timer;
        private int timer;
        private string gameTimer;
        private string currentStageImage;
        private string currentLevelImage;
        private string levelImage;

        public List<GameModel> GamesList { get; set; }
        private int mistakeStage;
        private int currentLevel;
        private int currentStage;
        private string survivorUsername;
        private string survivorPicturePath;
        private bool isSave;
        private bool categoryButtonsVisibility;
        private bool startButtonVisibility;
        private string currentWord;
        private string categoryString;
        private string[] currentWordArray;
        private string[] shownWordArray;
        public List<string> LevelList;
        public List<string> CurrentChaseList;
        public List<string> CurrentLevelList;

        private ECategories categories;
        public CategoriesModel CategoryManager { get; set; }

        //----------------------PROPERTIES-------------------------------
        public Players CurrentSurvivor
        {
            get
            {
                return currentSurvivor;
            }
            set
            {
                currentSurvivor = value;
                OnPropertyChanged("CurrentSurvivor");
                setSurvivorUsername();
                setSurvivorPicturePath();
            }
        }

        public int CurrentStage
        {
            get
            {
                return currentStage;
            }
            set
            {
                currentStage = value;
                OnPropertyChanged("CurrentStage");
            }
        }

        public int CurrentLevel
        {
            get
            {
                return currentLevel;
            }
            set
            {
                currentLevel = value;
                OnPropertyChanged("CurrentLevel");
            }
        }

        public int MistakeStage
        {
            get
            {
                return mistakeStage;
            }
            set
            {
                mistakeStage = value;
                OnPropertyChanged("MistakeStage");
            }
        }

        public string LevelImage
        {
            get
            {
                return levelImage;
            }
            set
            {
                levelImage = value;
                OnPropertyChanged("LevelImage");
            }
        }
        public string CurrentStageImage
        {
            get
            {
                return currentStageImage;
            }
            set
            {
                currentStageImage = value;
                OnPropertyChanged("CurrentStageImage");
            }
        }

        public string CurrentLevelImage
        {
            get
            {
                return currentLevelImage;
            }
            set
            {
                currentLevelImage = value;
                OnPropertyChanged("CurrentLevelImage");
            }
        }

        public string GameTimer
        {
            get
            {
                return gameTimer;
            }
            set
            {
                gameTimer = value;
                OnPropertyChanged("GameTimer");
            }
        }

        public string[] ShownWordArray
        {
            get
            {
                return shownWordArray;
            }
            set
            {
                shownWordArray = value;
                OnPropertyChanged("ShownWordArray");
            }
        }

        public string[] CurrentWordArray
        {
            get
            {
                return currentWordArray;
            }
            set
            {
                currentWordArray = value;
                OnPropertyChanged("CurrentWordArray");
            }
        }

        public ECategories Category
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                OnPropertyChanged("Category");
            }
        }

        public bool StartButtonVisibility
        {
            get
            {
                return startButtonVisibility;
            }
            set
            {
                startButtonVisibility = value;
                OnPropertyChanged("StartButtonVisibility");
            }
        }
        public bool CategoryButtonsVisibility
        {
            get
            {
                return categoryButtonsVisibility;
            }
            set
            {
                categoryButtonsVisibility = value;
                OnPropertyChanged("CategoryButtonsVisibility");
            }
        }

        public string CategoryString
        {
            get
            {
                return categoryString;
            }
            set
            {
                categoryString = value;
                OnPropertyChanged("CategoryString");
            }
        }

        public string CurrentWord
        {
            get
            {
                return currentWord;
            }
            set
            {
                currentWord = value;
                OnPropertyChanged("CurrentWord");
            }
        }

        public string SurvivorUsername
        {
            get
            {
                return survivorUsername;
            }
            set
            {
                survivorUsername = value;
                OnPropertyChanged("SurvivorUsername");
            }
        }

        public string SurvivorPicturePath
        {
            get
            {
                return survivorPicturePath;
            }
            set
            {
                survivorPicturePath = value;
                OnPropertyChanged("SurvivorImagePath");
            }
        }

        public ICommand SaveButtonCommand
        {
            get
            {
                if(saveButtonCommand == null)
                {
                    saveButtonCommand = new RelayCommand(saveButtonPressed);
                }
                return saveButtonCommand;
            }
        }

        public ICommand GamesSelectedCommand
        {
            get
            {
                if (gamesSelectedCommand == null)
                {
                    gamesSelectedCommand = new RelayCommand(gamesSelected);
                }
                return gamesSelectedCommand;
            }
        }

        public ICommand VillainsSelectedCommand
        {
            get
            {
                if (villainsSelectedCommand == null)
                {
                    villainsSelectedCommand = new RelayCommand(villainsSelected);
                }
                return villainsSelectedCommand;
            }
        }

        public ICommand MoviesSelectedCommand
        {
            get
            {
                if (moviesSelectedCommand == null)
                {
                    moviesSelectedCommand = new RelayCommand(moviesSelected);
                }
                return moviesSelectedCommand;
            }
        }

        public ICommand AllSelectedCommand
        {
            get
            {
                if (allSelectedCommand == null)
                {
                    allSelectedCommand = new RelayCommand(allSelected);
                }
                return allSelectedCommand;
            }
        }

        public ICommand StartPressedCommand
        {
            get
            {
                if (startPressedCommand == null)
                {
                    startPressedCommand = new RelayCommand(startPressed);
                }
                return startPressedCommand;
            }
        }

        public ICommand KeyboardButtonCommand
        {
            get
            {
                if(keyboardButtonCommand == null)
                {
                    keyboardButtonCommand = new RelayCommand(getKeyPressed);
                }
                return keyboardButtonCommand;
            }
        }

        //-----------------------METHODS------------------
        public string[] convertArrayAfterKey(string key)
        {
            string[] arr = new string[ShownWordArray.Length];
            char keyChar = key[0];
            bool contains = false;

            for(int i = 0; i < ShownWordArray.Length; i++)
            {
                string emptyWord = ShownWordArray[i];
                StringBuilder sb = new StringBuilder(emptyWord);
                if (CurrentWordArray[i].Contains(char.ToUpper(keyChar)) || CurrentWordArray[i].Contains(char.ToLower(keyChar)))
                {
                    for(int j = 0; j < CurrentWordArray[i].Length; j++)
                    {
                        if (CurrentWordArray[i][j] == char.ToUpper(keyChar) || CurrentWordArray[i][j] == char.ToLower(keyChar))
                        {
                            sb[j] = keyChar;

                        }
                    }
                    emptyWord = sb.ToString();
                    arr[i] = emptyWord;
                    contains = true;
                }
                else
                {
                    arr[i] = ShownWordArray[i];
                }
            }
            if(contains == false)
            {
                MistakeStage++;
                CurrentLevelImage = CurrentLevelList[MistakeStage];
                CurrentStageImage = CurrentChaseList[MistakeStage];
            }
            return arr;
        }

        public void startTimer()
        {
            _timer = new Timer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += (sender, args) =>
            {
                timer -= 1;
                if (timer < 10)
                    GameTimer = "00:0" + timer.ToString();
                else
                    GameTimer = "00:" + timer.ToString();
            };

            _timer.Start();
        }
        public void getKeyPressed(object obj)
        {
            bool da=true;
            if (CurrentWord != "")
            {
                if(MistakeStage < 4) 
                {
                    ShownWordArray = convertArrayAfterKey(obj.ToString());
                    for (int i = 0; i < ShownWordArray.Length; i++)
                    {
                        if (ShownWordArray[i].ToLower() != CurrentWordArray[i].ToLower())
                        {
                            da = false;
                        }
                    }
                    if (da)
                    {
                        startLevel();
                    }
                }
                else
                {
                    startLevel();
                }
            }
        }

        public void startLevelFromSave()
        {
            foreach(var game in GamesList)
            {
                if(game.Username == currentSurvivor.Username)
                {
                    CurrentLevel = game.CurrentLevel;
                    CurrentWord = game.CurrentWord;
                    ShownWordArray = game.ShownWord;
                    timer = game.Timer;
                    StartButtonVisibility = false;
                    CategoryButtonsVisibility = false;
                    MistakeStage = game.MistakeStage;
                    LevelImage = LevelList[CurrentLevel-1];
                    CurrentLevelImage = CurrentLevelList[MistakeStage];
                    CurrentStageImage = CurrentChaseList[MistakeStage];
                    Category = game.Category;
                }
            }
        }

        public void startLevel()
        {
            CurrentLevel++;
            if(CurrentLevel > 5)
            {
                _timer.Stop();
                StartButtonVisibility = true;
                CategoryButtonsVisibility = true;
                CurrentLevel = 0;
                LevelImage = CurrentLevelList[0];
                CurrentStageImage = CurrentChaseList[0];
                CurrentLevelImage = CurrentLevelList[0];
                MistakeStage = 0;
                CurrentWord = "";
                Category = ECategories.ENone;
                CurrentSurvivor.MatchesWon++;
                foreach(var x in PlayersList)
                {
                    if (x.Username == currentSurvivor.Username)
                    {
                        x.MatchesWon = CurrentSurvivor.MatchesWon;
                    }
                }
                fileManager.SaveCurrentData(PlayersList);
            }
            else if(MistakeStage > 3)
            {
                _timer.Stop();
                StartButtonVisibility = true;
                CategoryButtonsVisibility = true;
                CurrentLevel = 0;
                LevelImage = CurrentLevelList[0];
                CurrentLevelImage = CurrentLevelList[MistakeStage+1];
                CurrentStageImage = CurrentChaseList[MistakeStage+1];
                MistakeStage = 0;
                Category = ECategories.ENone;
                CurrentWord = "";
                CategoryString = "";
            }
            else
            {
                timer = 30;
                MistakeStage = 0;
                LevelImage = LevelList[CurrentLevel - 1];
                CurrentStageImage = CurrentChaseList[MistakeStage];
                CurrentLevelImage = CurrentLevelList[MistakeStage];
                CurrentWord = generateWordByCategory();
                CurrentWordArray = CategoryManager.ConvertWordToList(CurrentWord);
                ShownWordArray = convertArrayToEmpty();
            }
        }

        public void saveButtonPressed(object obj)
        {
            isSave = true;
            GameModel game = new GameModel()
            {
                Category = categories,
                CurrentLevel = currentLevel,
                MistakeStage = mistakeStage,
                ShownWord = shownWordArray,
                CurrentWord = currentWord,
                Timer = timer,
                Username = currentSurvivor.Username
            };
            _timer.Stop();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = false;
            var serializer = new XmlSerializer(typeof(List<GameModel>));

            GamesList = new List<GameModel>();
            using (StreamReader reader = new StreamReader("C:/Users/cimpi/source/repos/Hangman/Hangman/Files/games.xml"))
            {
                var xmlReader = XmlReader.Create(reader, settings);
                GamesList = (List<GameModel>)serializer.Deserialize(xmlReader);
                reader.Close();
            }
    
            foreach(var games in GamesList.ToList())
            {
                if(games.Username == game.Username)
                {
                    GamesList.Remove(games);
                }
            }

            GamesList.Add(game);
            using (var writer = new StreamWriter("C:/Users/cimpi/source/repos/Hangman/Hangman/Files/games.xml"))
            {
                serializer.Serialize(writer, GamesList);
            }
            using (StreamReader reader = new StreamReader("C:/Users/cimpi/source/repos/Hangman/Hangman/Files/games.xml"))
            {
                var xmlReader = XmlReader.Create(reader, settings);
                GamesList = (List<GameModel>)serializer.Deserialize(xmlReader);
                reader.Close();
            }
        
            StartButtonVisibility = true;
        }

        public void setSurvivorUsername()
        {
            if (CurrentSurvivor != null)
            {
                SurvivorUsername = CurrentSurvivor.Username;
            }
        }

        public void setSurvivorPicturePath()
        {
            if (CurrentSurvivor != null)
            {
                SurvivorPicturePath = CurrentSurvivor.PicturePath;
            }
        }

        public void setCategoryString()
        {
            switch (Category)
            {
                case ECategories.EHorrorMovies:
                    CategoryString = "Horror Movies";
                    break;
                case ECategories.EHorrorGames:
                    CategoryString = "Horror Games";
                    break;
                case ECategories.EHorrorVillains:
                    CategoryString = "Horror Villains";
                    break;
                case ECategories.EAllCategories:
                    CategoryString = "All Categories";
                    break;
                default:
                    CategoryString = "None";
                    break;
            }
        }

        public void villainsSelected(object obj)
        {
            Category = ECategories.EHorrorVillains;
            CategoryButtonsVisibility = !CategoryButtonsVisibility;
            setCategoryString();
        }
        public void moviesSelected(object obj)
        {
            Category = ECategories.EHorrorMovies;
            CategoryButtonsVisibility = !CategoryButtonsVisibility;
            setCategoryString();
        }
        public void gamesSelected(object obj)
        {
            Category = ECategories.EHorrorGames;
            CategoryButtonsVisibility = !CategoryButtonsVisibility;
            setCategoryString();
        }
        public void allSelected(object obj)
        {
            CategoryButtonsVisibility = !CategoryButtonsVisibility;
            Category = ECategories.EAllCategories;
            setCategoryString();
        }

        public string generateWordByCategory()
        {
            string word = "";
            switch (categories)
            {
                case ECategories.EHorrorMovies:
                    word = CategoryManager.GenerateRandomElement(CategoryManager.HorrorMovies);
                    return word;
                case ECategories.EHorrorGames:
                    word = CategoryManager.GenerateRandomElement(CategoryManager.HorrorGames);
                    return word;
                case ECategories.EHorrorVillains:
                    word = CategoryManager.GenerateRandomElement(CategoryManager.HorrorMoviesKillers);
                    return word;
                case ECategories.EAllCategories:
                    word = CategoryManager.GenerateRandomElement(CategoryManager.AllCategories);
                    return word;
                default:
                    return word;
            }
        }

        public string[] convertArrayToEmpty()
        {
            string[] arr = new string[CurrentWordArray.Length];
            for (int j = 0; j < CurrentWordArray.Length; j++)
            {
                string emptyWord = CurrentWordArray[j];
                StringBuilder sb = new StringBuilder(emptyWord);
                for (int i = 0; i < sb.Length; i++)
                {
                    sb[i] = ' ';
                }
                emptyWord = sb.ToString();
                arr[j] = emptyWord;
            }
            return arr;
        }
        public void startPressed(object obj)
        {
            if (categories != ECategories.ENone)
            {
                StartButtonVisibility = false;
                if (isSave)
                {
                    startLevelFromSave();
                }
                else
                {
                    CurrentSurvivor.MatchesPlayed++;

                    foreach (var x in PlayersList)
                    {
                        if (x.Username == currentSurvivor.Username)
                        {
                            x.MatchesPlayed = CurrentSurvivor.MatchesPlayed;
                        }
                    }
                    fileManager.SaveCurrentData(PlayersList);
                    startLevel();
                }
                startTimer();
            }
            isSave = false;
        }
    }
}
