using CodingChallenge.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace CodingChallenge.ViewModels
{
    public class TodayScoreViewModel
        : ViewModelBase
    {
        public TodayScoreViewModel()
        {
            LoadScores();
        }

        ObservableCollection<ScoreViewModel> _scores = new ObservableCollection<ScoreViewModel>();
        public ObservableCollection<ScoreViewModel> Scores
        {
            get { return _scores; }
        }

        ScoreViewModel _selectedgame;
        public ScoreViewModel SelectedGame
        {
            get { return _selectedgame; }
            set
            {
                SetProperty(ref _selectedgame, value);
                UpdateSelectedStatus();
            }
        }

        bool _isbusy;
        public bool IsBusy
        {
            get { return _isbusy; }
            set { SetProperty(ref _isbusy, value); }
        }

        public void LoadScores(int year = 2016, int month = 5, int day = 20)
        {
            IsBusy = true;
            var serviceScores = new ScoresService();

            //Task.Run(() =>
            //{
            var scores = serviceScores.LoadScoresAsync(year, month, day);
            Scores.Clear();
            scores.ForEach(score => Scores.Add(score));
            //});

            SelectedGame = Scores.FirstOrDefault();
        }

        void UpdateSelectedStatus()
        {
            foreach (var game in Scores)
            {
                game.IsSelected = SelectedGame == game;
            }
        }
    }
}
