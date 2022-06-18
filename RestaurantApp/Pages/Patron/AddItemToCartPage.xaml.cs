using RestaurantApp.Models;
using RestaurantApp.Services;
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
    public partial class AddItemToCartPage : ContentPage {
        PatronTabbedViewModel viewModel = new PatronTabbedViewModel();
        private OrderService svc = OrderService.GetInstance();
        private Item itemToAdd;
        public AddItemToCartPage(Item item) {
            InitializeComponent();

            itemToAdd = item;

            addButton.Clicked += OnAdd;
            cancelButton.Clicked += OnCancel;

            addQuantityButton.Clicked += OnAddQuantity;
            removeQuantityButton.Clicked += OnRemoveQuantity;

            category.Text = item.Category.ToString();
            itemName.Text = item.Name;
            unitPrice.Text = item.UnitPrice.ToString();
            itemDescription.Text = item.Description;

            int currentQty = svc.CurrentOrder.GetCartQuantityOfItem(item);
            if (currentQty == 0)
                currentQty = 1;
            quantityEntry.Text = currentQty.ToString();
        }

        private void OnAddQuantity(object sender, EventArgs e) {
            int currentQty;
            if (!Int32.TryParse(quantityEntry.Text, out currentQty)) {
                currentQty = 1;
            } else {
                currentQty += 1;
            }

            quantityEntry.Text = currentQty.ToString();
        }

        private void OnRemoveQuantity(object sender, EventArgs e) {
            int currentQty;
            if (!Int32.TryParse(quantityEntry.Text, out currentQty)) {
                currentQty = 1;
            } else if (currentQty <= 1) {
                currentQty = 0;
            } else {
                currentQty -= 1;
            }

            quantityEntry.Text = currentQty.ToString();
        }

        private async void OnAdd(object sender, EventArgs e) {
            // TODO: Validate quantity, then call ViewModel to add.
            int quantity;

            if (!Int32.TryParse(quantityEntry.Text, out quantity)) {
                await DisplayAlert("Error", "Quantity must be an integer!", "OK");
                return;
            } else if (quantity < 0) {
                await DisplayAlert("Error", "Quantity must be at least 0!", "OK");
                return;
            }

            svc.CurrentOrder.UpdateItem(itemToAdd, quantity);
            await Navigation.PopAsync();
        }

        private void OnCancel(object sender, EventArgs e) {
            Navigation.PopAsync();
        }
    }
}