using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void OpenMovieDetailsTest()
        {
            var title = string.Empty;
            Thread.Sleep(1000);
            app.Tap(c => c.Marked("LabelTitle"));
            Thread.Sleep(1000);
            app.Tap(c => c.Marked("LabelTitle"));
            Thread.Sleep(1000);
            var results = app.Query("LabelTitle");
            if (results.Length == 0)
                app.ScrollDown();
            results = app.Query("LabelTitle");
            if (results != null && results.Length > 0)
                title = results[0].Text;
            app.Back();
            app.Back();
            Assert.AreEqual("The Mandalorian", title);
        }
    }
}
