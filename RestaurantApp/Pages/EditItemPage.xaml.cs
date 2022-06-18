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
    public partial class EditItemPage : ContentPage {
        private ItemService svc = ItemService.GetInstance();
        private String oldName;
        public EditItemPage(Item itemToEdit) {
            InitializeComponent();

            categoryPicker.ItemsSource = Enum.GetValues(typeof(Category));
            saveButton.Clicked += OnEdit;
            deleteButton.Clicked += OnDelete;

            oldName = String.Copy(itemToEdit.Name);

            categoryPicker.SelectedIndex = (int) itemToEdit.Category;
            itemName.Text = itemToEdit.Name;
            unitPriceEntry.Text = itemToEdit.UnitPrice.ToString();
            itemDescription.Text = itemToEdit.Description;
        }

        private async void OnEdit(object sender, EventArgs e) {
            double unitPrice;

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

            Item newItem = svc.EditItem(oldName,
                                       itemName.Text,
                                       itemDescription.Text,
                                       unitPrice);
            if (newItem == null) {
                await DisplayAlert("Error", "An error has occurred (does another item with the same name exist?)", "OK");
                return;
            }

            await DisplayAlert("Success", String.Format(
                    "Edited item:\n" + "Category: {0}\n" + "Name: {1}\n" + "Description: {2}\n" + "Price: {3}",
                    newItem.Category, newItem.Name, newItem.Description, newItem.UnitPrice), "OK"
            );
            await Navigation.PopAsync();
        }

        private async void OnDelete(object sender, EventArgs e) {
            Item itemToDelete = svc.GetItemByName(oldName);
            if (await DisplayAlert("Warning", "Would you like to delete " + itemToDelete.Name + "?", "OK", "Cancel"))
                svc.DeleteItem(oldName);
            await Navigation.PopAsync();
        }
    }
}