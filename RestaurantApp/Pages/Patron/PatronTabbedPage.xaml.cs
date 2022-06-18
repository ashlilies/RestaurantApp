using RestaurantApp.Models;
using RestaurantApp.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantApp.Pages.Patron {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatronTabbedPage : TabbedPage {
        PatronTabbedViewModel viewModel = new PatronTabbedViewModel();
        public PatronTabbedPage() {
            InitializeComponent();

            BindingContext = viewModel;

            viewDishesButton.Clicked += OnViewDishesButtonClick;
            viewDrinksButton.Clicked += OnViewDrinksButtonClick;
            confirmOrderButton.Clicked += OnConfirmOrderClicked;

            itemsList.ItemSelected += OnItemSelected;
            cartList.ItemSelected += OnCartItemSelected;  // TODO
            orderHistoryList.ItemSelected += OrderHistoryList_ItemSelected;
            //totalPriceLabel.SetBinding(Label.TextProperty, new Binding(nameof(viewModel.CartTotalPriceStr), stringFormat:"{0:C2}"));

            OnViewDishesButtonClick(this, new EventArgs());  // default load
        }

        private void OrderHistoryList_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            throw new NotImplementedException();
        }

        private void OnCartItemSelected(object sender, SelectedItemChangedEventArgs e) {
            Item item = ((CartItem) e.SelectedItem).Item;

            var addItemPage = new AddItemToCartPage(item);
            addItemPage.Disappearing += UpdateCartTotalPrice;

            Navigation.PushAsync(addItemPage);
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
            var addItemPage = new AddItemToCartPage(item);
            addItemPage.Disappearing += UpdateCartTotalPrice;

            Navigation.PushAsync(addItemPage);
        }

        private void UpdateCartTotalPrice(object sender, EventArgs e) {
                totalPriceLabel.SetBinding(Label.TextProperty, new Binding(nameof(viewModel.CartTotalPrice), stringFormat:"{0:C2}"));
        }

        private async void OnConfirmOrderClicked(object sender, EventArgs e) {
            if (viewModel.ConfirmOrder()) {
                await DisplayAlert("Success", "Your order has been confirmed!", "OK");
                //cartList.ItemsSource = null;
                cartList.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(viewModel.CartCollection)));
                //viewModel.UpdateCartTotalPrice();
                totalPriceLabel.SetBinding(Label.TextProperty, new Binding(nameof(viewModel.CartTotalPrice), stringFormat:"{0:C2}"));
                return;
            } else {
                await DisplayAlert("Error", "Please add some items to cart first", "OK");
                return;
            }
        }

        protected override void OnCurrentPageChanged() {
            base.OnCurrentPageChanged();
            //totalPriceLabel.Text = null;
            //totalPriceLabel.SetBinding(Label.TextProperty, new Binding(nameof(viewModel.CartTotalPrice)));
        }
    }
}