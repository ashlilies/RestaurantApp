using RestaurantApp.Models;
using RestaurantApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            #region Create example items for testing
            // TEST: Create example items
            ItemService svc = ItemService.GetInstance();
            svc.AddItem(Category.Dishes, "Bolognese", "Yummy food", 10.95);
            svc.AddItem(Category.Dishes, "Carbonara", "Yummy food", 10.95);
            svc.AddItem(Category.Drinks, "Bubble Tea", "Yummy food", 10.95);
            #endregion

            //MainPage = new NavigationPage(new MainPage());
            // setting root to mode selector page
            MainPage = new NavigationPage(new ModeSelectorPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
