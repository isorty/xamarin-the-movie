using Prism.Commands;
using Prism.Navigation;
using System;
using System.Diagnostics;

using System.Threading.Tasks;
using TheMovie.Helpers;
using TheMovie.Models;

namespace TheMovie.ViewModels
{
    public class MoviesByGenrePageViewModel : BaseViewModel, INavigatingAware
    {
        private Genre _genre;
        public Genre _Genre
        {
            get { return _genre; }
            set { SetProperty(ref _genre, value); }
        }

        public int DescriptionLength { get; set; }

        public ObservableRangeCollection<ShortMovie> Movies { get; set; }

        public DelegateCommand LoadMoviesByGenreCommand { get; }
        public DelegateCommand<ShortMovie> ShowMovieDetailCommand { get; }

        private readonly INavigationService navigationService;

        public MoviesByGenrePageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Movies = new ObservableRangeCollection<ShortMovie>();

            LoadMoviesByGenreCommand = new DelegateCommand(async () => await ExecuteLoadMoviesByGenreCommand());
            ShowMovieDetailCommand = new DelegateCommand<ShortMovie>(async (ShortMovie movie) => await ExecuteShowMovieDetailCommand(movie).ConfigureAwait(false));          
        }

        public async void OnNavigatingTo(INavigationParameters parameters)
        {
            _Genre = parameters.GetValue<Genre>("genre");
            Title = _Genre.Name + " movies";
            await ExecuteLoadMoviesByGenreCommand();
        }

        private async Task ExecuteLoadMoviesByGenreCommand()
        {
            if (IsBusy)
            {
                Debug.WriteLine("was busy and returned");
                return;
            }
            IsBusy = true;
            try
            {
                Movies.Clear();
                await LoadMoviesByGenreAsync().ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadMoviesByGenreAsync()
        {
            try
            {
                var movies = await ApiService.GetMoviesByGenre(_Genre.Id).ConfigureAwait(false);
                Movies.AddRange(movies);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task ExecuteShowMovieDetailCommand(ShortMovie movie)
        {
            var parameters = new NavigationParameters
            {
                { nameof(movie), movie }
            };

            await navigationService.NavigateAsync("MovieDetailPage", parameters).ConfigureAwait(false);
        }
    }
}