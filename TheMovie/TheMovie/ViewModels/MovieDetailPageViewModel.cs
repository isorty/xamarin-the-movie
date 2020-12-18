using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TheMovie.Models;
using Xamarin.Forms;

namespace TheMovie.ViewModels
{
    public class MovieDetailPageViewModel : BaseViewModel, INavigatingAware
    {
        private ShortMovie _shortMovie;
        public ShortMovie _ShortMovie
        {
            get { return _shortMovie; }
            set { SetProperty(ref _shortMovie, value); }
        }

        public List<Xamarin.Forms.Image> Images { get; set; }

        private Movie movie;
        public Movie Movie
        {
            get { return movie; }
            set { SetProperty(ref movie, value); }
        }

        public DelegateCommand AddToFavouritesCommand { get; }

        public MovieDetailPageViewModel()
        {
            Images = new List<Xamarin.Forms.Image>();

            AddToFavouritesCommand = new DelegateCommand(async () => await ExecuteAddToFavouritesCommand());
        }   

        public async void OnNavigatingTo(INavigationParameters parameters)
        {
            _ShortMovie = parameters.GetValue<ShortMovie>("movie");
            Title = _ShortMovie.Title;
            await LoadMovieDetailAsync(_ShortMovie.Id).ConfigureAwait(false);
        }

        private async Task LoadMovieDetailAsync(int movieId)
        {
            var movieDetail = await ApiService.GetMovieById(movieId).ConfigureAwait(false);
            if (movieDetail != null)
            {
                if (movieDetail.Poster != null)
                {
                    var byteArray = Convert.FromBase64String(movieDetail.Poster);
                    Stream stream = new MemoryStream(byteArray);
                    Images.Add(new Xamarin.Forms.Image()
                    {
                        Source = Xamarin.Forms.ImageSource.FromStream(() => stream)
                    });
                }
                Movie = movieDetail;
                Movie.ImageSource = Images[0].Source;
            }
        }

        private async Task ExecuteAddToFavouritesCommand()
        {
            if (IsBusy)
            {
                Debug.WriteLine("was busy and returned");
                return;
            }
            IsBusy = true;
            try
            {
                var shortMovie = new ShortMovie()
                {
                    Id = Movie.Id,
                    Title = Movie.Title,
                    Rating = Movie.Rating,
                    Description = Movie.Description
                };
                await AddToFavouritesAsync(shortMovie).ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddToFavouritesAsync(ShortMovie movie)
        {
            bool isExist = true;
            try
            {
                var tmp = await App.Database.GetItemAsync(movie.Id);
                var result = await App.Database.DeleteItemAsync(movie);
                if (result > 0)
                    await App.Current.MainPage.DisplayAlert("Favourites", "Deleted from favourites", "Ok");
            }
            catch
            {
                isExist = false;
            }
            if (!isExist)
            {
                var result = await App.Database.SaveItemAsync(movie);
                if (result > 0)
                    await App.Current.MainPage.DisplayAlert("Favourites", "Added to favourites", "Ok");
            }
        }
    }
}