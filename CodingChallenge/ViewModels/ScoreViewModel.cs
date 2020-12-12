namespace CodingChallenge.ViewModels
{
    public class ScoreViewModel
        : ViewModelBase
    {
        string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        string _subtitle;
        public string Subtitle
        {
            get { return _subtitle; }
            set { SetProperty(ref _subtitle, value); }
        }

        string _imageurl;
        public string ImageUrl
        {
            get { return _imageurl; }
            set { SetProperty(ref _imageurl, value); }
        }

        bool _isselected;
        public bool IsSelected
        {
            get { return _isselected; }
            set { SetProperty(ref _isselected, value); }
        }
    }
}
