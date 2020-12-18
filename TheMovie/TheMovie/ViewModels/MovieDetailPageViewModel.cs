using Mobile.Consts;
using Mobile.Extensions;
using Plugin.Share;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheMovie.Helpers;
using TheMovie.Models;

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

        public MovieDetailPageViewModel()
        {
            Images = new List<Xamarin.Forms.Image>();
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
    }
}