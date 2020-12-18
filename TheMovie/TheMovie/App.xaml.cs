using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism.DryIoc;
using Prism.Ioc;
using TheMovie.Views;
using TheMovie.SQLite;
using Xamarin.Forms;
using System.IO;
using System;

namespace TheMovie
{
    public partial class App : PrismApplication
    {
        public const string DATABASE_NAME = "movies.db";
        public static MovieAsyncRepository database;
        public static MovieAsyncRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new MovieAsyncRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<GenresPage>();
            containerRegistry.RegisterForNavigation<MoviesByGenrePage>();
            containerRegistry.RegisterForNavigation<MovieDetailPage>();
        }        

        protected override void OnStart()
        {
            base.OnStart();
        }
    }
}