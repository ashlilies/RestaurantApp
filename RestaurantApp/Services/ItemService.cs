using RestaurantApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestaurantApp.Services {
    public class ItemService {

        #region Singleton
        private static ItemService instance = null;
        public static ItemService GetInstance() {
            if (instance == null)
                instance = new ItemService();
            return instance;
        } 

        /// <summary>
        /// Constructor has to be private to prevent accidentally creating singletons.
        /// </summary>
        private ItemService() {
            DishesCollection = new ObservableCollection<Item>();
            DrinksCollection = new ObservableCollection<Item>();
        }

        #endregion

        // Can use properties based off an inner list of all items; LINQ create observable collection based on inner list
        //public ObservableCollection<Item> ItemCollection { get; private set; }  // can't use LINQ to filter
        public ObservableCollection<Item> DishesCollection { get; private set; }
        public ObservableCollection<Item> DrinksCollection { get; private set; }

        /// <summary>
        /// Returns item object if successful, null otherwise.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="unitPrice"></param>
        /// <returns></returns>
        public Item AddItem(Category category, String name, String description, double unitPrice) {
            Item item;

            if (GetItemByName(name) != null)  // item already exists
                return null;

            if (unitPrice > 0) {
                item = new Item {
                    Category = category,
                    Name = name,
                    Description = description,
                    UnitPrice = unitPrice
                };

                switch (category) {
                case Category.Dishes:
                    DishesCollection.Add(item);
                    break;
                case Category.Drinks:
                    DrinksCollection.Add(item);
                    break;
                default:
                    // do not add if invalid category or null
                    break;
                }
                return item;
            }

            return null;
        }

        /// <summary>
        /// Returns null if old item name not found, returns Item object if successful otherwise.
        /// Category update not allowed.
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        /// <param name="description"></param>
        /// <param name="unitPrice"></param>
        /// <returns></returns>
        public Item EditItem(String oldName, String newName, String description, double unitPrice) {
            Item item = GetItemByName(oldName);
            if (item == null)
                return null;

            // Does new item name exist and it is not that name is unchanged?
            Item duplicate = GetItemByName(newName);
            if (duplicate != null && duplicate != item)
                return null;

            item.Name = newName;
            item.Description = description;
            item.UnitPrice = unitPrice;

            return item;
        }

        /// <summary>
        /// Removes an item from the list by name.
        /// Returns the success result as a boolean.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DeleteItem(String name) {
            Item item = GetItemByName(name);
            if (item != null) {
                switch (item.Category) {
                case Category.Dishes:
                    DishesCollection.Remove(item);
                    break;
                case Category.Drinks:
                    DrinksCollection.Remove(item);
                    break;
                default:
                    return false;
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns null if not found, returns Item object otherwise
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Item GetItemByName(String name) {
            foreach (Item item in DishesCollection) {
                if (item.Name == name)
                    return item;
            }

            foreach (Item item in DrinksCollection) {
                if (item.Name == name)
                    return item;
            }

            return null;
        }
    }
}
