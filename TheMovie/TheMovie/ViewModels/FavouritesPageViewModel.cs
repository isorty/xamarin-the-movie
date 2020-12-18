using Prism.Commands;
using Prism.Navigation;
using System;
using System.Diagnostics;

using System.Threading.Tasks;
using TheMovie.Helpers;
using TheMovie.Models;

namespace TheMovie.ViewModels
{
    public class FavouritesPageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ShortMovie> Movies { get; set; }

        public DelegateCommand LoadMoviesCommand { get; }
        public DelegateCommand<ShortMovie> ShowMovieDetailCommand { get; }

        private readonly INavigationService navigationService;

        public FavouritesPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Movies = new ObservableRangeCollection<ShortMovie>();

            LoadMoviesCommand = new DelegateCommand(async () => await ExecuteLoadMoviesCommand());
            ShowMovieDetailCommand = new DelegateCommand<ShortMovie>(async (ShortMovie movie) => await ExecuteShowMovieDetailCommand(movie).ConfigureAwait(false));

            LoadMoviesCommand.Execute();
        }

        private async Task ExecuteLoadMoviesCommand()
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
                await LoadMoviesAsync().ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadMoviesAsync()
        {
            try
            {
                var movies = await App.Database.GetItemsAsync();
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