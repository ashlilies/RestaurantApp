using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage {
        private ItemService svc = ItemService.GetInstance();
        public AddItemPage() {
            InitializeComponent();

            categoryPicker.ItemsSource = Enum.GetValues(typeof(Category));
            addButton.Clicked += OnAdd;
        }

        private async void OnAdd(object sender, EventArgs e) {
            double unitPrice;
            Category selectedItem;

            if (categoryPicker.SelectedItem == null) {
                //if (Category.TryParse(categoryPicker.SelectedItem.ToString(), out selectedItem) == false) {
                await DisplayAlert("Error", "Please set a valid category!", "OK");
                return;
            }

            Category.TryParse(categoryPicker.SelectedItem.ToString(), out selectedItem);

            if (Double.TryParse(unitPriceEntry.Text, out unitPrice) == false) {
                await DisplayAlert("Error", "Failed to add item: unit price is invalid!", "OK");
                return;
            }

            if (!(unitPrice > 0)) {
                await DisplayAlert("Error", "Failed to add item: unit price should be greater than 0!", "OK");
                return;
            }

            if (String.IsNullOrWhiteSpace(itemDescription.Text)) {
                await DisplayAlert("Error", "Description can't be empty!", "OK");
                return;
            }

            Item newItem = svc.AddItem(selectedItem,
                                       itemName.Text,
                                       itemDescription.Text,
                                       unitPrice);

            if (newItem != null)
                await DisplayAlert("Success", String.Format("Added item:\n" +
                                                            "Category: {0}\n" +
                                                            "Name: {1}\n" +
                                                            "Description: {2}\n" +
                                                            "Price: {3}",
                                                            newItem.Category, newItem.Name,
                                                            newItem.Description, newItem.UnitPrice), "OK");
            else {
                await DisplayAlert("Error", "An error has occurred (are you trying to add an item with a name that already exists?)", "OK");
                return;
            }

            await Navigation.PopAsync();
        }
    }
}