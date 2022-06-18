using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RestaurantApp.Models {
    public class Item :INotifyPropertyChanged {
        public Category Category {  // not really used as handled in svc
            get { return _category; }
            set {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public String Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public String Description {
            get { return _description; }
            set {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public double UnitPrice {
            get { return _unitPrice; }
            set {
                _unitPrice = value;
                OnPropertyChanged(nameof(UnitPrice));
            }
        }

        private Category _category;
        private String _name;
        private String _description;
        private double _unitPrice;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String nameOfProperty) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nameOfProperty));
        }

        public override String ToString() {
            return String.Format("{0} ({1})", Name, UnitPrice);
        }
    }
    public enum Category {
        Dishes,
        Drinks
    }

}
