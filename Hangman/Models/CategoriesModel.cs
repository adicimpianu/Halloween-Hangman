using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class CategoriesModel
    {
        public CategoriesModel()
        {
            HorrorMovies = new List<string>();
            HorrorGames = new List<string>();
            HorrorMoviesKillers = new List<string>();
            AllCategories = new List<string>();
            AddToGames();
            AddToMovies();
            AddToKillers();
            AddToAllCategories();
        }
        public List<string> HorrorMovies { get; set; }
        public List<string> HorrorMoviesKillers { get; set; }
        public List<string> HorrorGames { get; set; }
        public List<string> AllCategories { get; set; }

        public void AddToMovies()
        {
            HorrorMovies.Add("The Grudge");
            HorrorMovies.Add("Halloween");
            HorrorMovies.Add("Friday The Thirteenth");
            HorrorMovies.Add("The Conjuring");
            HorrorMovies.Add("Insidious");
            HorrorMovies.Add("A Nightmare On Elm Street");
            HorrorMovies.Add("Scream");
            HorrorMovies.Add("Hereditary");
            HorrorMovies.Add("The Shining");
            HorrorMovies.Add("The Exorcist");
            HorrorMovies.Add("Texas Chainsaw Massacre");
            HorrorMovies.Add("Silent Hill");
            HorrorMovies.Add("SAW");
            HorrorMovies.Add("The Babadook");
            HorrorMovies.Add("A Quiet Place");
            HorrorMovies.Add("Suspiria");
            HorrorMovies.Add("It Follows");
            HorrorMovies.Add("Rec");
            HorrorMovies.Add("Alien");
            HorrorMovies.Add("The Silence Of The Lambs");
            HorrorMovies.Add("Rosemarys Baby");
            HorrorMovies.Add("The Witch");
            HorrorMovies.Add("IT");
            HorrorMovies.Add("Midsommar");
            HorrorMovies.Add("The Nun");
            HorrorMovies.Add("The Blair Witch Project");
            HorrorMovies.Add("The Ring");
            HorrorMovies.Add("Shutter");
            HorrorMovies.Add("Us");
            HorrorMovies.Add("Oculus");
        }

        public void AddToKillers()
        {
            HorrorMoviesKillers.Add("Sadako");
            HorrorMoviesKillers.Add("Kayako");
            HorrorMoviesKillers.Add("Michael Myers");
            HorrorMoviesKillers.Add("Pennywise");
            HorrorMoviesKillers.Add("Jigsaw");
            HorrorMoviesKillers.Add("Candyman");
            HorrorMoviesKillers.Add("Hannibal Lecter");
            HorrorMoviesKillers.Add("Leatherface");
            HorrorMoviesKillers.Add("Jason Voorhees");
            HorrorMoviesKillers.Add("Freddy Krueger");
            HorrorMoviesKillers.Add("Ghostface");
            HorrorMoviesKillers.Add("Pinhead");
            HorrorMoviesKillers.Add("Annabelle");
            HorrorMoviesKillers.Add("The Nun");
            HorrorMoviesKillers.Add("Babadook");
            HorrorMoviesKillers.Add("Jack Torrance");
            HorrorMoviesKillers.Add("Chucky");
            HorrorMoviesKillers.Add("Frankenstein");
            HorrorMoviesKillers.Add("Dracula");
            HorrorMoviesKillers.Add("Pamela Voorhees");
        }

        public void AddToGames()
        {
            HorrorGames.Add("Amnesia");
            HorrorGames.Add("Outlast");
            HorrorGames.Add("Silent Hill");
            HorrorGames.Add("Until Dawn");
            HorrorGames.Add("Resident Evil");
            HorrorGames.Add("Dead By Daylight");
            HorrorGames.Add("Phasmophobia");
            HorrorGames.Add("Slenderman");
            HorrorGames.Add("Visage");
            HorrorGames.Add("SOMA");
            HorrorGames.Add("Layers Of Fear");
            HorrorGames.Add("PT");
            HorrorGames.Add("The Evil Within ");
            HorrorGames.Add("Penumbra");
            HorrorGames.Add("Five Nights At Freddys");
        }

        public void AddToAllCategories()
        {
            foreach(var x in HorrorGames)
            {
                AllCategories.Add(x);
            }
            foreach(var x in HorrorMovies)
            {
                AllCategories.Add(x);
            }
            foreach(var x in HorrorMoviesKillers)
            {
                AllCategories.Add(x);
            }
        }

        public string GenerateRandomElement(List<string> category)
        {
            var random = new Random();
            int index = random.Next(category.Count);
            return category[index];
        }

        public string[] ConvertWordToList(string word)
        {
            string[] resultList;
            resultList = word.Split(' ');
            return resultList;
        }
    }
}
