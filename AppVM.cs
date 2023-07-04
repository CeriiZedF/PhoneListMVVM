
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PhoneListMVVM
{
    // view model class
    public class AppVM : INotifyPropertyChanged
    {
        private Phone? selectedPhone;

        public ObservableCollection<Phone> Phones { get; set; }
        public Phone? SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        private RelayCommands addCommand;
        public RelayCommands AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommands(obj =>
                  {
                      
                        Phone phone = new Phone(SelectedPhone.Title, SelectedPhone.Company, SelectedPhone.Price);
                        Phones.Insert(0, phone);
                        SelectedPhone = phone;
                      
                  }));
            }
        }
        private RelayCommands delCommand;
        public RelayCommands DelCommand
        {
            get
            {
                return delCommand ??
                  (delCommand = new RelayCommands(obj =>
                  {
                      if (SelectedPhone != null)
                      {
                          Phones.Remove(SelectedPhone);
                          SelectedPhone = new Phone();
                      }
                      
                  }));
            }
        }

        public AppVM()
        {
            Phones = new ObservableCollection<Phone>
            {
                new Phone { Title="iPhone 14", Company = "Apple", Price = 41499 },
                new Phone { Title="Samsung Galaxy S22", Company = "Samsung", Price = 50299 },
                new Phone { Title="Xiaomi 12", Company = "Xiaomi", Price = 25999 },
                new Phone { Title="Samsung Galaxy Fold 4", Company = "Samsung", Price = 79999 }
            };
            SelectedPhone = new Phone();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
