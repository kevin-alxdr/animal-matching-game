namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;

            List<string> animalEmoji = [
                "🐸", "🐸",
                "🐷", "🐷",
                "🐢", "🐢",
                "🐶", "🐶",
                "🐀", "🐀",
                "😺", "😺",
                "🐋", "🐋",
                "🐲", "🐲",
                ];

            foreach(var button in AnimalButtons.Children.OfType<Button>()) // Find every button in the FlexLayout and repeat the satements between the { curly brackets } for each of them
            {
                int index = Random.Shared.Next(animalEmoji.Count); // Pick a random number between 0 and the number of emoji left in the list and call it "index"
                string nextEmoji = animalEmoji[index]; // Use the random number called "index" to get a random emoji from the list
                button.Text = nextEmoji; // Make the button display the selected emoji
                animalEmoji.RemoveAt(index); // Remove the chosen emoji from the list
            }

            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
        }

        int tenthsOfSecondsElapsed = 0;
        private bool TimerTick()
        {
            if (!this.IsLoaded) return false;

            tenthsOfSecondsElapsed++;

            TimeElapsed.Text = "Time elapsed: " +
                (tenthsOfSecondsElapsed / 10F).ToString("0.0s");

            if (PlayAgainButton.IsVisible)
            {
                tenthsOfSecondsElapsed = 0;
                return false;
            }

            return true;

        }

        Button lastClicked;
        bool findingMatch = false;
        int matchesFound;

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button buttonClicked)
            {
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && (findingMatch == false))
                {
                    buttonClicked.BackgroundColor = Colors.Red;
                    lastClicked = buttonClicked;
                    findingMatch = true;
                }
                else
                {
                    if ((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text)
                        && !string.IsNullOrWhiteSpace(buttonClicked.Text))
                    {
                        matchesFound++;
                        lastClicked.Text = " ";
                        buttonClicked.Text = " ";
                    }
                    lastClicked.BackgroundColor = Colors.LightBlue;
                    buttonClicked.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;
                }
            }

            if (matchesFound == 8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
            }
        }
    }
}
