using Hangman.Commands;
using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class LoginViewModel : BaseVM
    {
        private ICommand nextPictureCommand;
        private ICommand previousPictureCommand;
        private ICommand addNewSurvivorVisibleCommand;
        private ICommand addNewSurvivorCommand;
        private ICommand removeSelectedSurvivorCommand;
        private ICommand removeSelectedSurvivorVisibleCommand;
        private ICommand playPressedCommand;
        public ObservableCollection<Players> PlayersList { get; set; }
        public List<string> ProfilePicturesList;
        public FileModel fileManager;

        private Players selectedUser;
        private string selectedUsername;
        private string selectedImagePath;
        private bool imageButtonsVisibility;
        private bool newSurvivorVisibility;
        private string newSurvivorUsername;
        private bool removeSurvivorVisibility;
        private Action<Players> action;
        //Constructor
        public LoginViewModel(Action<Players> action)
        {
            fileManager = new FileModel();
            this.action = action;
            ImageButtonsVisibility = false;
            NewSurvivorVisibility = false;
            RemoveSurvivorVisibility = false;
            NewSurvivorUsername = "";
            ProfilePicturesList = new List<string>();
            ProfilePicturesList.Add("../Images/ProfilePictures/Freddy.png");
            ProfilePicturesList.Add("../Images/ProfilePictures/Myers.jpg");
            ProfilePicturesList.Add("../Images/ProfilePictures/Ghostface.jpg");
            ProfilePicturesList.Add("../Images/ProfilePictures/IT.jpg");
            ProfilePicturesList.Add("../Images/ProfilePictures/Jason.jpg");
            ProfilePicturesList.Add("../Images/ProfilePictures/Pinhead.jpg");
            ProfilePicturesList.Add("../Images/ProfilePictures/Bughuul.jpg");
            PlayersList = fileManager.LoadFileData();
        }

        //Properties ------------------------------------------------------------

        public Players SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
                setSelectedUserName();
                setImageButtonVisibility();
                setSelectedImagePath();
            }
        }
        public string SelectedUsername
        {
            get
            {
                return selectedUsername;
            }
            set
            {
                selectedUsername = value;
                OnPropertyChanged("SelectedUsername");
            }
        }
        public string NewSurvivorUsername
        {
            get
            {
                return newSurvivorUsername;
            }
            set
            {
                newSurvivorUsername = value;
                OnPropertyChanged("NewSurvivorUsername");
            }
        }
        public string SelectedImagePath
        {
            get
            {
                return selectedImagePath;
            }
            set
            {
                selectedImagePath = value;
                OnPropertyChanged("SelectedImagePath");
            }
        }
        public bool ImageButtonsVisibility
        {
            get
            {
                return imageButtonsVisibility;
            }
            set
            {
                imageButtonsVisibility = value;
                OnPropertyChanged("ImageButtonsVisibility");
            }
        }
        public bool NewSurvivorVisibility
        {
            get
            {
                return newSurvivorVisibility;
            }
            set
            {
                newSurvivorVisibility = value;
                OnPropertyChanged("NewSurvivorVisibility");
            }
        }

        public bool RemoveSurvivorVisibility
        {
            get
            {
                return removeSurvivorVisibility;
            }
            set
            {
                removeSurvivorVisibility = value;
                OnPropertyChanged("RemoveSurvivorVisibility");
            }
        }


        //------------------------------------------------------------------------
        //Functions -------------------------------------------------------------
        public void setImageButtonVisibility()
        {
            if (SelectedUser != null)
            {
                ImageButtonsVisibility = true;
            }
        }

        public void setNewSurvivorVisibility(object obj)
        {
            if (NewSurvivorVisibility)
                NewSurvivorVisibility = false;
            else
                NewSurvivorVisibility = true;
        }

        public void setRemoveSurvivorVisibility(object obj)
        {
            if (SelectedUser != null)
                RemoveSurvivorVisibility = !RemoveSurvivorVisibility;
        }

        public void setSelectedUserName()
        {
            if (SelectedUser != null)
            {
                SelectedUsername = SelectedUser.Username;
            }
        }
        public void setSelectedImagePath()
        {
            if (SelectedUser != null)
            {
                SelectedImagePath = SelectedUser.PicturePath;
            }
        }

        public void setSelectedImageToNext(object obj)
        {
            if (ProfilePicturesList.Contains(SelectedUser.PicturePath))
            {
                if (ProfilePicturesList[ProfilePicturesList.IndexOf(SelectedUser.PicturePath)] != ProfilePicturesList.Last())
                {
                    SelectedUser.PicturePath = ProfilePicturesList[ProfilePicturesList.IndexOf(SelectedUser.PicturePath) + 1];
                    setSelectedImagePath();
                }
                else
                {
                    SelectedUser.PicturePath = ProfilePicturesList[0];
                    setSelectedImagePath();
                }
            }
            fileManager.SaveCurrentData(PlayersList);
        }

        public void setSelectedImageToPrevious(object obj)
        {
            if (ProfilePicturesList.Contains(SelectedUser.PicturePath))
            {
                if (ProfilePicturesList[ProfilePicturesList.IndexOf(SelectedUser.PicturePath)] != ProfilePicturesList.First())
                {
                    SelectedUser.PicturePath = ProfilePicturesList[ProfilePicturesList.IndexOf(SelectedUser.PicturePath) - 1];
                    setSelectedImagePath();
                }
                else
                {
                    SelectedUser.PicturePath = ProfilePicturesList.Last();
                    setSelectedImagePath();
                }
            }
            fileManager.SaveCurrentData(PlayersList);
        }

        public void addNewSurvivor(object obj)
        {
            Players player = new Players();
            player.Username = NewSurvivorUsername;
            player.PicturePath = "../Images/ProfilePictures/Myers.jpg";
            player.MatchesPlayed = 0;
            player.MatchesWon = 0;
            PlayersList.Add(player);
            fileManager.SaveCurrentData(PlayersList);
            NewSurvivorVisibility = !NewSurvivorVisibility;
        }

        public void removeSelectedSurvivor(object obj)
        {
            PlayersList.Remove(selectedUser);
            fileManager.SaveCurrentData(PlayersList);
            SelectedUser = PlayersList[0];
            RemoveSurvivorVisibility = !RemoveSurvivorVisibility;
        }

        public void playPressed(object obj)
        {
            if (SelectedUser != null) { 
                action.Invoke(SelectedUser);
            }
        }

        //-------------------------------------------------------------------------------
        //Commands ----------------------------------------------------------------------

        public ICommand NextPictureCommand
        {
            get
            {
                if (nextPictureCommand == null)
                {
                    nextPictureCommand = new RelayCommand(setSelectedImageToNext);
                }
                return nextPictureCommand;
            }
        }

        public ICommand PreviousPictureCommand
        {
            get
            {
                if (previousPictureCommand == null)
                {
                    previousPictureCommand = new RelayCommand(setSelectedImageToPrevious);
                }
                return previousPictureCommand;
            }
        }

        public ICommand AddNewSurvivorVisibleCommand
        {
            get
            {
                if (addNewSurvivorVisibleCommand == null)
                {
                    addNewSurvivorVisibleCommand = new RelayCommand(setNewSurvivorVisibility);
                }
                return addNewSurvivorVisibleCommand;
            }
        }

        public ICommand AddNewSurvivorCommand
        {
            get
            {
                if(addNewSurvivorCommand == null)
                {
                    addNewSurvivorCommand = new RelayCommand(addNewSurvivor);
                }
                return addNewSurvivorCommand;
            }
        }

        public ICommand RemoveSelectedSurvivorCommand
        {
            get
            {
                if(removeSelectedSurvivorCommand == null)
                {
                    removeSelectedSurvivorCommand = new RelayCommand(removeSelectedSurvivor);
                }
                return removeSelectedSurvivorCommand;
            }
        }

        public ICommand RemoveSelectedSurvivorVisibleCommand
        {
            get
            {
                if(removeSelectedSurvivorVisibleCommand == null)
                {
                    removeSelectedSurvivorVisibleCommand = new RelayCommand(setRemoveSurvivorVisibility);
                }
                return removeSelectedSurvivorVisibleCommand;
            }
        }

        public ICommand PlayPressedCommand
        {
            get
            {
                if(playPressedCommand == null)
                {
                    playPressedCommand = new RelayCommand(playPressed);
                }
                return playPressedCommand;
            }
        }
        //--------------------------------------------------------------------------------
    }
}
