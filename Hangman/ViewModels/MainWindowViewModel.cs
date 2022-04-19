using Hangman.Commands;
using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.ViewModels
{
    public class MainWindowViewModel : BaseVM
    {

        private Players currentSurvivor;
        public LoginViewModel LoginVM { get; set; }
        public GameViewModel GameVM { get; set; }
        public HangmanViewModel HangmanVM { get; set; }

        private object _currentView;

        private void switchToGame(Players player)
        {
            currentSurvivor = player;
            GameVM = new GameViewModel(currentSurvivor,switchToHangman);
            CurrentView = GameVM;
        }
        private void switchToHangman(Players player, bool newload)
        {
            currentSurvivor = player;
            HangmanVM = new HangmanViewModel(currentSurvivor,newload);
            CurrentView = HangmanVM;
        }
        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        public MainWindowViewModel()
        {
            LoginVM = new LoginViewModel(switchToGame);
            CurrentView = LoginVM;
        }
    }
}
