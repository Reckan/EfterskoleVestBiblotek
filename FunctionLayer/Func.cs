using DataClasses;
using DataModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FunctionLayer
{
    public class Func : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        Model Model { get; set; } = new();
        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public ReadOnlyObservableCollection<Book> Books
        {
            get
            {
                return Model.Book;
            }
        }

        public ReadOnlyObservableCollection<Customer> Customers
        {
            get
            {
                return Model.Customer;
            }
        }

        public ReadOnlyObservableCollection<BookRental> BookRentals
        {
            get
            {
                return Model.BookRental;
            }
        }
    }
}