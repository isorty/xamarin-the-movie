using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMovie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await App.Database.CreateTable();
            base.OnAppearing();
        }
    }
}