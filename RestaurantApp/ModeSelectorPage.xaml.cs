using RestaurantApp.Pages.Patron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantApp {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModeSelectorPage : ContentPage {
        public ModeSelectorPage() {
            InitializeComponent();

            adminButton.Clicked += OnAdminMode;
            patronButton.Clicked += OnPatronMode;
        }

        private void OnAdminMode(object sender, EventArgs e) {
            Navigation.PushAsync(new MainPage());
        }
        private void OnPatronMode(object sender, EventArgs e) {
            Navigation.PushAsync(new PatronTabbedPage());
        }
    }
}