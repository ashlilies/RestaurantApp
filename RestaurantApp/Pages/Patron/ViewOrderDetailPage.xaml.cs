using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RestaurantApp.Models;

namespace RestaurantApp.Pages.Patron {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewOrderDetailPage : ContentPage {
        public ViewOrderDetailPage(Item item) {
            InitializeComponent();
        }
    }
}