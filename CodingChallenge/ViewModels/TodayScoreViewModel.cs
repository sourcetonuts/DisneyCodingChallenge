using CodingChallenge.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge.ViewModels
{
    public class TodayScoreViewModel
        : ViewModelBase
    {
        ScoresService _serviceScores = new ScoresService();

        public TodayScoreViewModel()
        {
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

        List<ScoreViewModel> _apiscores;

        public async Task<List<ScoreViewModel>> LoadScores(int year, int month, int day)
        {
            IsBusy = true;
            Scores.Clear();
            await Task.Run(() =>
               _apiscores = _serviceScores.LoadScoresAsync(year, month, day)
            ).ConfigureAwait(true);

            _apiscores.ForEach(score => Scores.Add(score));
            SelectedGame = Scores.FirstOrDefault();
            IsBusy = false;
            return _apiscores;
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
