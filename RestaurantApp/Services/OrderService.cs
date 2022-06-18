using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using RestaurantApp.Models;

namespace RestaurantApp.Services {
    // Singleton
    public class OrderService {
        #region Singleton
        private static OrderService instance;
        public static OrderService GetInstance() {
            if (instance == null)
                instance = new OrderService();
            return instance;
        }
        private OrderService() { }
        #endregion

        public Order CurrentOrder { get; private set; } = new Order();
        public ObservableCollection<Order> OrderHistory { get; } = new ObservableCollection<Order>();

        public ObservableCollection<CartItem> CartCollection {
            get {
                return CurrentOrder.Cart;
            }
        }

        /// <summary>
        /// Returns true if successful - that is, the order is not blank.
        /// </summary>
        /// <returns></returns>
        public bool ConfirmOrder() {
            if (CurrentOrder.TotalPrice > 0) {  // price > 0; hence there are items
                CurrentOrder.ConfirmedTime = DateTime.Now;
                OrderHistory.Add(CurrentOrder);
                CurrentOrder = new Order();
                return true;
            }
            return false;
        }
    }
}
