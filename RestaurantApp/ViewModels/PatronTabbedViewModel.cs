using RestaurantApp.Models;
using RestaurantApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace RestaurantApp.ViewModels {
    // Singleton
    public class PatronTabbedViewModel {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private ItemService itemSvc = ItemService.GetInstance();
        private OrderService orderSvc = OrderService.GetInstance();

        // Returns a reference to the appropriate ObservableCollection from the service.
        public ObservableCollection<Item> AllDishes {
            get {
                return itemSvc.DishesCollection;
            }
        }

        public ObservableCollection<Item> AllDrinks {
            get {
                return itemSvc.DrinksCollection;
            }
        }

        public ObservableCollection<CartItem> CartCollection {
            get {
                return orderSvc.CartCollection;
            }
        }

        public ObservableCollection<Order> OrderHistoryCollection {
            get {
                return orderSvc.OrderHistory;
            }
        }

        public double CartTotalPrice {
            get {
                return orderSvc.CurrentOrder.TotalPrice;
            }
        }

        /// <summary>
        /// Returns success of confirming the order
        /// </summary>
        /// <returns></returns>
        public bool ConfirmOrder() {
            return orderSvc.ConfirmOrder();
        }
    }
}
