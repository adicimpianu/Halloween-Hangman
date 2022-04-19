using Hangman.Commands;
using Hangman.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace Hangman.ViewModels
{
    public class GameViewModel : BaseVM
    {
        //----------------------CONSTRUCTOR-----------------------
        public GameViewModel(Players survivor, Action<Players, bool> action)
        {
            CurrentSurvivor = survivor;
            this.action = action;
            GamesWon = survivor.MatchesWon.ToString();
            GamesPlayed = survivor.MatchesPlayed.ToString();
        }

        //---------------------- VAR ---------------------------------------
        private ICommand newGamePressedCommand;
        private ICommand loadGamePressedCommand;

        private bool contains;
        private string gamesPlayed;
        private string gamesWon;
        private Players currentSurvivor;
        private string survivorUsername;
        private string survivorPicturePath;
        private Action<Players, bool> action;

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

        public string GamesPlayed
        {
            get
            {
                return gamesPlayed;

            }
            set
            {
                gamesPlayed = value;
                OnPropertyChanged("GamesPlayed");
            }
        }

        public string GamesWon
        {
            get
            {
                return gamesWon;
            }
            set
            {
                gamesWon = value;
                OnPropertyChanged("GamesWon");
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

        //-----------------------METHODS------------------
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
        public void newGamePressed(object obj)
        {
            if (CurrentSurvivor != null)
            {
                action.Invoke(CurrentSurvivor, true);
            }
        }

        public void loadGamePressed(object obj)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = false;
            var serializer = new XmlSerializer(typeof(List<GameModel>));

            var GamesList = new List<GameModel>();
            using (StreamReader reader = new StreamReader("C:/Users/cimpi/source/repos/Hangman/Hangman/Files/games.xml"))
            {
                var xmlReader = XmlReader.Create(reader, settings);
                GamesList = (List<GameModel>)serializer.Deserialize(xmlReader);
                reader.Close();
            }
            

            if (CurrentSurvivor != null)
            {
                contains = false;
                foreach (var game in GamesList)
                {
                    if (game.Username == CurrentSurvivor.Username)
                    {
                        contains = true;
                    }
                }

                if(contains == true)
                    action.Invoke(CurrentSurvivor, false);
            }
        }

        //------------------------COMMANDS---------------------

        public ICommand LoadGamePressedCommand
        {
            get
            {
                if (loadGamePressedCommand == null)
                {
                    loadGamePressedCommand = new RelayCommand(loadGamePressed);
                }
                return loadGamePressedCommand;
            }
        }

        public ICommand NewGamePressedCommand
        {
            get
            {
                if (newGamePressedCommand == null)
                {
                    newGamePressedCommand = new RelayCommand(newGamePressed);
                }
                return newGamePressedCommand;
            }
        }
    }
}
