using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace Hangman.Models
{
    public class FileModel
    {
        private readonly string filePath = "C:/Users/cimpi/source/repos/Hangman/Hangman/Files/Survivors.txt";
        
        public void SaveCurrentData(ObservableCollection<Players> playersList)
        {
            if (File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    foreach(Players player in playersList)
                    {
                        sw.WriteLine(player.Username + " " + player.PicturePath + " " + player.MatchesPlayed.ToString() + " " + player.MatchesWon.ToString());
                    }
                }
            }
        }

        public ObservableCollection<Players> LoadFileData()
        {
            ObservableCollection<Players> players = new ObservableCollection<Players>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        Players player = new Players();
                        string[] words = line.Split(' ');
                        player.Username = words[0];
                        player.PicturePath = words[1];
                        player.MatchesPlayed = int.Parse(words[2]);
                        player.MatchesWon = int.Parse(words[3]);
                        players.Add(player);
                    }
                }
            }
            return players;
        }

    }
}