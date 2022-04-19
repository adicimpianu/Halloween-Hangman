using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for GamepageView.xaml
    /// </summary>
    public partial class GamepageView : UserControl
    {
        public GamepageView()
        {
            InitializeComponent();
        }

        public GamepageView(GameViewModel gameViewModel)
        {
            InitializeComponent();
            DataContext = gameViewModel;
        }
    }
}
