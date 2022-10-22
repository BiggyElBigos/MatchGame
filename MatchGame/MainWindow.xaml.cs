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

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer= new DispatcherTimer();
        int tenthsofSecondElapsed;
        int matchesfound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();



        }



        private void SetUpGame()
        {
            List<string> AnimalEmoji = new List<string>()
            {
                "🐵","🐵",
                "🐸","🐸",
                "🦛","🦛",
                "🐘","🐘",
                "🐢","🐢",
                "🦀","🦀",
                "🐌","🐌",
                "💑","💑",

            };
            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if(textBlock.Name!= "timeTextBlock")
                { 
                int index=random.Next(AnimalEmoji.Count);
                string nextEmoji=AnimalEmoji[index];
                textBlock.Text=nextEmoji;
                AnimalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthsofSecondElapsed = 0;
            matchesfound=0;
        }

        bool findingMatch = false;
        TextBlock lastTextblockClicked;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch==false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextblockClicked = textBlock;
                findingMatch = true;
            }
            else if(textBlock.Text== lastTextblockClicked.Text)
            {
                matchesfound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextblockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsofSecondElapsed++;
            timeTextBlock.Text=(tenthsofSecondElapsed/10F).ToString("0.0s  ");
                if (matchesfound == 8)
                {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - jeszcze raz?";
                }

        }
        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesfound == 8)
            {
                SetUpGame();
            }


        }
    }
}
