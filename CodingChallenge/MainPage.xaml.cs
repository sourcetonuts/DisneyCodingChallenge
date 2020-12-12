using CodingChallenge.ViewModels;
using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CodingChallenge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage 
        : Page
    {
        TodayScoreViewModel _viewmodel;
        public MainPage()
        {
            this.InitializeComponent();

            _viewmodel = new TodayScoreViewModel();
            gamedayPicker.Date = new DateTime(2016, 5, 20);
            gamedayPicker.DateChanged += GamedayPicker_DateChanged;

            this.DataContext = _viewmodel;

            Loaded += (s,e) => scoresListView.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }

        private void GamedayPicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            _viewmodel.LoadScores(args.NewDate.Value.Year, args.NewDate.Value.Month, args.NewDate.Value.Day);
            scoresListView.Focus( Windows.UI.Xaml.FocusState.Keyboard );
        }
    }
}
