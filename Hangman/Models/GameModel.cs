using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hangman.ViewModels.HangmanViewModel;

namespace Hangman.Models
{
    public class GameModel
    {
        public string Username { get; set; }
        public ECategories Category { get; set; }
        public int Timer { get; set; }
        public int MistakeStage { get; set; }
        public int CurrentLevel { get; set; }
        public string CurrentWord { get; set; }
        public string[] ShownWord { get; set; }

    }
}
