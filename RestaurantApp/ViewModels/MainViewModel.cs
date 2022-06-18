using System;
using System.Collections.Generic;
using System.Text;

using RestaurantApp.Services;
using RestaurantApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestaurantApp.ViewModels {
    // support filtering
    public class MainViewModel {
        private ItemService svc = ItemService.GetInstance();

        // Returns a reference to the appropriate ObservableCollection from the service.
        public ObservableCollection<Item> AllDishes {
            get {
                return svc.DishesCollection;
            }
        }

        public ObservableCollection<Item> AllDrinks {
            get {
                return svc.DrinksCollection;
            }
        }
    }
}
