using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using RestaurantApp.ViewModels;

namespace RestaurantApp.Models {
    // An order contains a cart consisting of multiple items.
    // You can add or remove item objects from the cart, or access the Cart as a list of CartItems.
    // You can also get the quantity of a specific item - 0 if it is not in the cart.
    // It will be shown as one record in order history.
    public class Order : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void TriggerTotalPriceEvent() {
            OnPropertyChanged(nameof(TotalPrice));
        }

        public String Id { get; } = Guid.NewGuid().ToString();

        public DateTime ConfirmedTime { get; set; }
        public ObservableCollection<CartItem> Cart { get; private set; } = new ObservableCollection<CartItem>();

        /// <summary>
        /// Total price of ALL items in the cart.
        /// </summary>
        public double TotalPrice {
            get {
                double totalPrice = 0;
                foreach (CartItem ci in Cart)
                    totalPrice += ci.TotalPrice;
                return totalPrice;
            }
        }

        /// <summary>
        /// Updates an item and its respective quantity in the cart, removing an item if necessary.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public void UpdateItem(Item item, int quantity) {
            int currentQuantity = GetCartQuantityOfItem(item);
            if ((quantity - currentQuantity) > 0)  // add items
                AddItem(item, quantity - currentQuantity);
            else if ((quantity - currentQuantity) < 0)  // remove items
                RemoveItem(item, currentQuantity - quantity);
            //OnPropertyChanged(nameof(TotalPrice));
        }

        /// <summary>
        /// Adds an item to the current order.
        /// Returns success result as a bool.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public bool AddItem(Item item, int quantity = 1) {
            CartItem ci;

            if (!(quantity > 0))
                return false;

            if ((ci = GetCartItem(item)) != null) {
                ci.Quantity += quantity;
                TriggerTotalPriceEvent();
                return true;
            }

            ci = new CartItem(this, item, quantity);
            Cart.Add(ci);
            TriggerTotalPriceEvent();
            return true;
        }

        /// <summary>
        /// Removes a specified quantity, or 1, of an item from the Cart.
        /// Returns success result as a bool.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantityToRemove"></param>
        /// <returns></returns>
        public bool RemoveItem(Item item, int quantityToRemove = 1) {
            CartItem ci;

            if (!(quantityToRemove > 0))
                return false;

            if ((ci = GetCartItem(item)) == null)  // doesn't exist in cart
                return false;

            ci.Quantity -= quantityToRemove;
            if (ci.Quantity <= 0)
                Cart.Remove(ci);
            TriggerTotalPriceEvent();
            return true;
        }

        public int GetCartQuantityOfItem(Item item) {
            CartItem ci = GetCartItem(item);
            if (ci == null)
                return 0;
            return ci.Quantity;
        }

        /// <summary>
        /// Get respective CartItem by passing in an Item object. Returns null if doesn't exist.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private CartItem GetCartItem(Item item) {
            foreach (CartItem ci in Cart) {
                if (item == ci.Item)
                    return ci;
            }
            return null;
        }
    }

    public class CartItem : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName) {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public Item Item { get; }
        public int Quantity {
            get {
                return _quantity;
            }
            set {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        private int _quantity;

        /// <summary>
        /// Total price of a single item in the cart.
        /// </summary>
        public double TotalPrice {
            get {
                return Item.UnitPrice * Quantity;
            }
        }

        // The following 2 properties are for the Cart page bindings.
        public String ItemName {
            get {
                return Item.Name;
            }
        }

        public String ItemDescription {
            get {
                return Item.Description;
            }
        }

        private Order baseOrder;
        public CartItem(Order baseOrder, Item item, int quantity) {
            this.baseOrder = baseOrder;
            Item = item;
            Quantity = quantity;
        }
    }
}
