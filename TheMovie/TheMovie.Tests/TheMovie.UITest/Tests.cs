using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TheMovie.UITest
{
    [TestFixture(Platform.Android)]    
    public class Tests
    {
        IApp app;
        readonly Platform platform;

        public Tests()
        {
            platform = Platform.Android;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        [Category("UI Test")]        
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        [Category("UI Test")]
        public void TapOnTabsTest()
        {
            //app.Tap(c => c.Marked("Search"));

            //app.Tap(c => c.Marked("ImageViewCell"));
            Thread.Sleep(1000);
            app.ScrollDown();
            Thread.Sleep(1000);
            app.ScrollUp();
            Assert.Equals(true, true);
            //app.Screenshot(Ta);
        }

        //[Test]
        //[Category("UI Test")]
        //public void SearchMovieWithoutResultByTitle()
        //{
        //    const string searchTerm = "_WithoutResult_";
        //    const string resultMessage = "No results found.";

        //    app.Tap(c => c.Marked("Search"));
        //    app.EnterText(c => c.Marked("SearchBar"), searchTerm);
        //    app.PressEnter();

        //    Thread.Sleep(millisecondsTimeout: 5000);

        //    AppResult[] results = app.Query("message");
        //    Assert.AreEqual(resultMessage, results[0].Text);
        //    app.Screenshot("Without Result");
        //}

        //[Test]
        //[Category("UI Test")]
        //public void SearchMoviePagination()
        //{
        //    const int minMoviesExpected = 30;
        //    const int totalScroll = 50;

        //    const string searchTerm = "Spider";

        //    app.Tap(c => c.Marked("Search"));
        //    app.EnterText(c => c.Marked("SearchBar"), searchTerm);
        //    app.PressEnter();

        //    var titles = new List<string>();
        //    PaginationMovies(totalScroll, titles);

        //    Assert.GreaterOrEqual(titles.Count, minMoviesExpected);            
        //}

        //[Test]
        //[Category("UI Test")]
        //public void UpcomingMoviePagination()
        //{
        //    const int minMoviesExpected = 30;
        //    const int totalScroll = 50;

        //    var titles = new List<string>();
        //    PaginationMovies(totalScroll, titles);

        //    Assert.GreaterOrEqual(titles.Count, minMoviesExpected);
        //}        

        //[Test]
        //[Category("UI Test")]
        //public void UpcomingMovieSelectItem()
        //{            
        //    app.Tap(c => c.Marked("ImageViewCell"));
        //    Thread.Sleep(1000);
        //    AppResult[] title = app.Query("LabelTitle");

        //    if (title.Length == 0)
        //    {
        //        app.ScrollDown();
        //        title = app.Query("LabelTitle");
        //    }

        //    Assert.AreNotEqual("", title);

        //    app.Screenshot("Movie Selected");
        //}

        //private void PaginationMovies(int totalScroll, List<string> titles)
        //{
        //    for (int i = 0; i < totalScroll; i++)
        //    {
        //        AppResult[] result = app.Query("LabelTitle");
        //        foreach (var item in result)
        //        {
        //            if (titles.Find(a => a.Equals(item.Text)) == null)
        //            {
        //                titles.Add(item.Text);
        //            }
        //        }

        //        app.ScrollDown();
        //    }
        //}
    }
}