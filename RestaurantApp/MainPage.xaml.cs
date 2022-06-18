// Admin page

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RestaurantApp.Models;
using RestaurantApp.Services;
using RestaurantApp.Pages;
using RestaurantApp.ViewModels;

namespace RestaurantApp {
    // TODO: Add hamburger menu
    public partial class MainPage : ContentPage {
        MainViewModel viewModel = new MainViewModel();
        public MainPage() {
            InitializeComponent();

            BindingContext = viewModel;

            addItemButton.Clicked += (s, e) => Navigation.PushAsync(new AddItemPage());
            viewDishesButton.Clicked += OnViewDishesButtonClick;
            viewDrinksButton.Clicked += OnViewDrinksButtonClick;

            itemsList.ItemSelected += OnItemSelected;

            OnViewDishesButtonClick(this, new EventArgs());  // default load
        }

        private void OnViewDishesButtonClick(object sender, EventArgs e) {
            itemsList.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(viewModel.AllDishes)));

            viewDishesButton.BackgroundColor = Color.LightGray;
            viewDrinksButton.BackgroundColor = Color.White;
        }

        private void OnViewDrinksButtonClick(object sender, EventArgs e) {
            itemsList.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(viewModel.AllDrinks)));

            viewDrinksButton.BackgroundColor = Color.LightGray;
            viewDishesButton.BackgroundColor = Color.White;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e) {
            Item item = (Item) e.SelectedItem;
            Navigation.PushAsync(new EditItemPage(item));
        }
    }
}
