using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMovie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesByGenrePage : ContentPage
    {
        public MoviesByGenrePage()
        {
            InitializeComponent();
            ItemsListView.ItemSelected += (sender, e) =>
            {
                // Manually deselect item
                ((ListView)sender).SelectedItem = null;
            };
        }
    }
}