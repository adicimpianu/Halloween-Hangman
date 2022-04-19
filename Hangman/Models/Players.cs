using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class Players
    {
        public string Username { get; set; }
        public string PicturePath { get; set; }

        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }

    }
}
