using NUnit.Framework;
using System.Linq;
using TheMovie.UnitTest.Mocks;
using TheMovie.UnitTest.Mocks.Views;
using TheMovie.ViewModels;

namespace TheMovie.UnitTest.ViewModels
{
    [TestFixture]
    public class TestMainPageViewModel
    {        
        private GenresPageViewModel viewModel;
        private PrismApplicationMock app;        

        public TestMainPageViewModel()
        {            
            Xamarin.Forms.Mocks.MockForms.Init();
            app = new PrismApplicationMock();
            viewModel = new GenresPageViewModel(app.NavigationService);
        }        

        [Test]
        [Category("Unit Test")]
        public void LoadUpcomingMovies()
        {
            viewModel.LoadGenresCommand.Execute();            
            Assert.AreNotEqual(0, viewModel.Genres.Count());
        }

        [Test]
        [Category("Unit Test")]
        public void LoadUpcomingMoviesPagination()
        {
            const int minMoviesExpected = 40;
            viewModel.LoadGenresCommand.Execute();
            viewModel.ItemAppearingCommand.Execute(viewModel.Genres.Last());
            viewModel.ItemAppearingCommand.Execute(viewModel.Genres.Last());
            viewModel.ItemAppearingCommand.Execute(viewModel.Genres.Last());
            Assert.IsTrue(viewModel.Genres.Count() > minMoviesExpected);
        }

        [Test]
        [Category("Unit Test")]
        public void ShowSearchMovies()
        {            
            var nameView = new SearchMoviesPageMock().ToString();
            viewModel.ShowSearchMoviesCommand.Execute();
            var nameCurrentView = app.MainPage.Navigation.NavigationStack.FirstOrDefault().ToString();
            Assert.AreEqual(nameView, nameCurrentView);
        }

        [Test]
        [Category("Unit Test")]
        public void ShowMovieDetail()
        {            
            var nameView = new MovieDetailPageMock().ToString();
            var movie = viewModel.Genres.FirstOrDefault();
            viewModel.ShowMoviesByGenreCommand.Execute(movie);
            var nameCurrentView = app.MainPage.Navigation.NavigationStack.FirstOrDefault().ToString();
            Assert.AreEqual(nameView, nameCurrentView);
        }        
    }
}
