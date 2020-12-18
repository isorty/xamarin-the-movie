using Plugin.Connectivity;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

using TheMovie.Helpers;
using TheMovie.Models;

namespace TheMovie.ViewModels
{
    public class GenresPageViewModel : BaseViewModel
    {        
        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set { SetProperty(ref isConnected, value); }
        }
        
        public ObservableRangeCollection<Genre> Genres { get; set; }

        public DelegateCommand LoadGenresCommand { get; }
        public DelegateCommand<Genre> ShowMoviesByGenreCommand { get; }

        private readonly INavigationService navigationService;
        public GenresPageViewModel(INavigationService navigationService)
        {
            Title = "Catalog";
            this.navigationService = navigationService;
            Genres = new ObservableRangeCollection<Genre>();

            LoadGenresCommand = new DelegateCommand(async () => await ExecuteLoadGenresCommand().ConfigureAwait(false));
            ShowMoviesByGenreCommand = new DelegateCommand<Genre>(async (Genre genre) => await ExecuteShowMoviesByGenreCommand(genre).ConfigureAwait(false));

            LoadGenresCommand.Execute();
        }        

        private async Task ExecuteLoadGenresCommand()
        {
            IsConnected = CrossConnectivity.Current.IsConnected;
            if (IsBusy || !IsConnected)
                return;
            IsBusy = true;
            try
            {
                Genres.Clear();
                await LoadGenresAsync().ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }               

        private async Task LoadGenresAsync()
        {
            try
            {
                var genres = await ApiService.GetGenres().ConfigureAwait(false);
                Genres.AddRange(genres);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task ExecuteShowMoviesByGenreCommand(Genre genre)
        {
            var parameters = new NavigationParameters
            {
                { nameof(genre), genre }
            };
            await navigationService.NavigateAsync("MoviesByGenrePage", parameters).ConfigureAwait(false);
        }
    }
}